import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SocketService {

  private hubConnection: signalR.HubConnection;
  constructor() { 
    this.hubConnection = new signalR.HubConnectionBuilder().withUrl(environment.rgbApiUrl).build();
  }

  startConnection() {
    this.hubConnection.start();
  }

  stopConnection() {
    this.hubConnection.stop();
  }

  sendMessage(method: string, message: string): Promise<void> {
    return this.hubConnection.invoke(method, message);
  }

  addMessageListener(callback: (message: any) => void): void {
    this.hubConnection.on('SendMessage', callback);
  }
}

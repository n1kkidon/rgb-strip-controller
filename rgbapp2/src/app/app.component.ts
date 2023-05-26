import { Component } from '@angular/core';

import { environment } from 'src/environments/environment';
import { RgbService } from './services/rgb.service';
import { SocketService } from './services/socket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  
  senderFunction = this.throttle(this.sendSelectedColor, 35);
  sendSelectedColor(){
    if(this.socketService.isConnected())
      this.socketService.sendMessage("ReceiveMessage", this.arrayColors[this.selectedColor]);
    console.log(this.selectedColor);
  }
  isOn = false;
  constructor(private rgbService: RgbService, private socketService: SocketService){
    socketService.addMessageListener(this.onMessage);
    this.rgbService.getLedStatus().subscribe(data => this.isOn = data as boolean);
    this.rgbService.getPermission().subscribe(data => {
      if(data as boolean){
        this.socketService.startConnection();
      }
    });
  }

  onMessage = (message: string) => {
    this.arrayColors[this.selectedColor] = message;
  }

  getEndpoint(){
    this.rgbService.getLedStatus().subscribe(data => console.log(data));
  }
  
  onOff(){
    if(this.isOn){
      this.socketService.sendMessage("TurnOff", "");
    }
    else this.socketService.sendMessage("TurnOn", "");
    this.isOn = !this.isOn;
  }

  title = 'rgbapp2';
  arrayColors: any = {
    color1: '#2883e9',
    color2: '#e920e9',
  };
  selectedColor: string = 'color1';


  throttle(func: (...args: any[]) => void, delay: number) {
    let timeoutId: any;
    let lastExecTime = 0;

    return (...args: any[]) => {
      const context = this;
      const currentTimestamp = new Date().getTime();

      const executeFunction = () => {
        func.apply(context, args);
        lastExecTime = currentTimestamp;
      };

      if (currentTimestamp >= lastExecTime + delay) {
        executeFunction();
      } else {
        clearTimeout(timeoutId);
        timeoutId = setTimeout(executeFunction, delay);
      }
    };
  }
}

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
  

  constructor(private rgbService: RgbService, private socketService: SocketService){
    socketService.addMessageListener(this.onMessage);
  }

  onMessage = (message: string) => {
    this.arrayColors[this.selectedColor] = message;
  }

  getEndpoint(){
    this.rgbService.getLedStatus().subscribe(data => console.log(data));
  }
  isOn = false;
  onOff(){
    this.isOn = !this.isOn;
    if(this.isOn){
      this.socketService.sendMessage("TurnOff", "");
    }
    else this.socketService.sendMessage("TurnOn", "");
  }

  startConnection(){
    this.socketService.startConnection();
  }
  stopConnection(){
    this.socketService.stopConnection();
  }

  title = 'rgbapp2';
  i = 10;
  a  = 0;
  color: any;
  arrayColors: any = {
    color1: '#2883e9',
    color2: '#e920e9',
    color3: 'rgb(255,245,0)',
    color4: 'rgb(236,64,64)',
    color5: 'rgba(45,208,45,1)'
  };
  selectedColor: string = 'color1';

  print(){
    this.socketService.sendMessage("ReceiveMessage", this.arrayColors[this.selectedColor]);
    console.log(this.selectedColor);
  }
}

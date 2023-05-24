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

  }

  getEndpoint(){
    this.rgbService.getLedStatus().subscribe(data => console.log(data));
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
    console.log(this.selectedColor);
  }
  hello(){
    this.a = this.a + this.i;
    this.arrayColors[this.selectedColor] = `rgba(${this.a}, ${this.a}, ${this.a}, 0.9)`;
  }
}

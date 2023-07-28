import { Component } from '@angular/core';
import { NgxPermissionsService } from 'ngx-permissions';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent{
  title = 'Homepage';

  constructor(private permService: NgxPermissionsService){
    this.permService.loadPermissions(['GUEST']);
  }
}

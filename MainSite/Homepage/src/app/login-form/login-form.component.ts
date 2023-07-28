import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss'],
})
export class LoginFormComponent {
  constructor(private userService: UserService){}

  hide = true;

  onSubmit(username: string, pass: string){
    this.userService.login(username, pass).subscribe((resp) => {
      console.log(resp);
    });
  }
}

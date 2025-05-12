import { Component } from '@angular/core';
import {AuthService} from '../../../core/services/auth.service';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {UserLogin} from '../../../core/models/userLogin';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  form: FormGroup = new FormGroup({
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    password: new FormControl<string>('', [Validators.required]),
  });

  constructor(private authService: AuthService, private router: Router) { }

  login(): void{
    if(this.form.valid){
      let userLogin: UserLogin = {
        email: this.form.controls['email'].value,
        password: this.form.controls['password'].value
      }

      this.authService.login(userLogin).subscribe({
        next:() => {
          console.log('Login successfull');
        },
        error: (err) => {
          console.error('Login failed', err);
        }
      });
      this.router.navigate(['/']);
    }
  }
}

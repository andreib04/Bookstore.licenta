import { Component } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {User} from '../../../../../../core/models/user';
import {UsersServiceService} from '../../../../../../core/services/users-service/users-service.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-add-user-page',
  templateUrl: './add-user-page.component.html',
  styleUrl: './add-user-page.component.css'
})
export class AddUserPageComponent {
  form: FormGroup;
  constructor(private userService: UsersServiceService, private router: Router){
    this.form = new FormGroup({
      firstName: new FormControl<string>('', [Validators.required]),
      lastName: new FormControl<string>('', [Validators.required]),
      role: new FormControl<string>('', [Validators.required]),
      email: new FormControl<string>('', [Validators.required, Validators.email]),
      password: new FormControl<string>('', [
        Validators.required,
        Validators.minLength(8),
      ]),
      confirmPassword: new FormControl<string>('', [Validators.required]),
    })
  }

  onSubmit(){
    if(this.form.valid){
      let user: User = {
        FirstName: this.form.controls['firstName'].value,
        LastName: this.form.controls['lastName'].value,
        Role: this.form.controls['role'].value,
        Email: this.form.controls['email'].value,
        Password: this.form.controls['password'].value,
      } as User;

      this.userService.postUser(user).subscribe(res => {
        console.log(res);
        console.log('Form submitted successfully!');
      });

      this.form.reset();

    }else{
      console.log(this.form.errors);
      console.log('Form is invalid!');
    }
  }

}

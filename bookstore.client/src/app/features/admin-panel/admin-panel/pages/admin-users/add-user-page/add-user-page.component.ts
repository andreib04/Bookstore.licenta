import { Component } from '@angular/core';
import {AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators} from '@angular/forms';
import {User} from '../../../../../../core/models/user';
import {UsersServiceService} from '../../../../../../core/services/users-service/users-service.service';
import {Router} from '@angular/router';
import {Location} from '@angular/common';

@Component({
  selector: 'app-add-user-page',
  templateUrl: './add-user-page.component.html',
  styleUrl: './add-user-page.component.css'
})
export class AddUserPageComponent {
  form: FormGroup;
  constructor(private userService: UsersServiceService, private router: Router, private location: Location){
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
    },
      { validators: this.matchingValidator }
    );
  }

  matchingValidator: ValidatorFn = (form: AbstractControl): ValidationErrors | null => {
    const password = form.get('password')?.value;
    const confirmPassword = form.get('confirmPassword')?.value;

    return password && confirmPassword && password !== confirmPassword ? {passwordmatcherror: true} : null;
  };

  onSubmit(){
    if(this.form.valid){
      let user: User = {
        firstName: this.form.controls['firstName'].value,
        lastName: this.form.controls['lastName'].value,
        role: this.form.controls['role'].value,
        email: this.form.controls['email'].value,
        password: this.form.controls['password'].value,
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

  backLocation(){
    this.location.back();
  }

}

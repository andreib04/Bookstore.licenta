import { Component } from '@angular/core';
import {AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators} from '@angular/forms';
import {User} from '../../../core/models/user';
import {UsersServiceService} from '../../../core/services/users-service/users-service.service';
import { Router} from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  form: FormGroup;


  constructor(private userService: UsersServiceService, private router: Router) {
    this.form = new FormGroup({
      firstName: new FormControl<string>('', [Validators.required]),
      lastName: new FormControl<string>('', [Validators.required]),
      email: new FormControl<string>('', [Validators.required, Validators.email]),
      password: new FormControl<string>('', [
        Validators.required,
        Validators.minLength(8),
        this.passwordValidator
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

  passwordValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
    const password = control.value;
    if(!password) return null;

    const hasUpper = /[A-Z]/.test(password);
    const hasNumber = /\d/.test(password);
    const isValid = hasUpper && hasNumber;

    return isValid ? null : {
      upper: !hasUpper,
      number: !hasNumber
    };
  };


  onSubmit(){
    if(this.form.valid){
      let user: User = {
        FirstName: this.form.controls['firstName'].value,
        LastName: this.form.controls['lastName'].value,
        Email: this.form.controls['email'].value,
        Password: this.form.controls['password'].value,
      } as User;

      this.userService.postUser(user).subscribe(res => {
        console.log(res);
        console.log('Form submitted successfully!');
      });

      this.router.navigate(['login']);
    }else{
      console.log(this.form.errors);
      console.log('Form is invalid!');
    }
  }

}

import { Component } from '@angular/core';
import {AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators} from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
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

  //TO CONTINUE
  form: FormGroup = new FormGroup({
    firstName: new FormControl<string>('', [Validators.required]),
    lastName: new FormControl<string>('', [Validators.required]),
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    password: new FormControl<string>('', [Validators.required, Validators.minLength(8), this.passwordValidator]),
  })

  get f(){
    return this.form.controls;
  }

}

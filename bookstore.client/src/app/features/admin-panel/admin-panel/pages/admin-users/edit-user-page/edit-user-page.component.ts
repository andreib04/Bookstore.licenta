import { Component } from '@angular/core';
import {UsersServiceService} from '../../../../../../core/services/users-service/users-service.service';
import {AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators} from '@angular/forms';
import {User} from '../../../../../../core/models/user';
import {ActivatedRoute, Router} from '@angular/router';
import {Location} from '@angular/common';

@Component({
  selector: 'app-edit-user-page',
  templateUrl: './edit-user-page.component.html',
  styleUrl: './edit-user-page.component.css'
})
export class EditUserPageComponent {
  user: User = {} as User;

  form: FormGroup;
  constructor(private userService: UsersServiceService,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              private location: Location) {
    this.user.id = +activatedRoute.snapshot.params['id'];

    this.userService.getOneUser(this.user.id).subscribe(res => {
      this.form.controls['firstName'].setValue(res.firstName);
      this.form.controls['lastName'].setValue(res.lastName);
      this.form.controls['role'].setValue(res.role);
      this.form.controls['email'].setValue(res.email);

      console.log(res);
    })

    this.form = new FormGroup({
      firstName: new FormControl<string>('', [Validators.required]),
      lastName: new FormControl<string>('', [Validators.required]),
      role: new FormControl<string>('', [Validators.required]),
      email: new FormControl<string>('', [Validators.required, Validators.email]),
      /*password: new FormControl<string>('', [Validators.required, Validators.minLength(8)]),
      confirmPassword: new FormControl<string>('', [Validators.required])*/
    },
      //{ validators: this.matchingValidator }
    );
  }

  matchingValidator: ValidatorFn = (form: AbstractControl): ValidationErrors | null => {
    const password = form.get('password')?.value;
    const confirmPassword = form.get('confirmPassword')?.value;

    return password && confirmPassword && password !== confirmPassword ? {passwordmatcherror: true} : null;
  };

  onSubmit(){
    if(this.form.valid){
      this.user = {
        id: this.user.id,
        firstName: this.form.controls['firstName'].value,
        lastName: this.form.controls['lastName'].value,
        role: this.form.controls['role'].value,
        /*password: this.form.controls['password'].value*/
      } as User;

      this.userService.updateUser(this.user.id, this.user).subscribe(res => {
        console.log(res);
        this.router.navigate(['admin/users']);
      })
    }
  }

  backLocation(){
    this.location.back();
  }
}

import { Component } from '@angular/core';
import {User} from '../../../../../core/models/user';
import {UsersServiceService} from '../../../../../core/services/users-service/users-service.service';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-admin-users',
  templateUrl: './admin-users.component.html',
  styleUrl: './admin-users.component.css'
})
export class AdminUsersComponent {
  users: User[] = [
    {
      Id: 2,
      FirstName: "abc",
      LastName: "aaa",
      Email: "abc@gmail.com",
      Password: '',
      Role: "Admin"
    }
  ];

  user: User = {} as User;

  constructor(private usersService: UsersServiceService, private activatedRoute: ActivatedRoute) {
    let id: number = +this.activatedRoute.snapshot.params['id'];

    this.usersService.getUsers().subscribe((data) =>{
      this.users = data;
      console.log('Users data: ', this.users);
    })
  }

  deleteUser(){
    this.usersService.deleteUser(this.user.Id).subscribe((data) =>{
      console.log('Deleted user: ', data);
      window.location.reload();
    })
  }
}

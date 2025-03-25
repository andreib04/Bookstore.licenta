import { Component } from '@angular/core';
import {User} from '../../../../../core/models/user';
import {UsersServiceService} from '../../../../../core/services/users-service/users-service.service';

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

  constructor(private usersService: UsersServiceService) {
    this.usersService.getUsers().subscribe((data) =>{
      this.users = data;
      console.log('Users data: ', this.users);
    })
  }
}

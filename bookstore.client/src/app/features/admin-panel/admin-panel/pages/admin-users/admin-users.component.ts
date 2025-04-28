import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {User} from '../../../../../core/models/user';
import {UsersServiceService} from '../../../../../core/services/users-service/users-service.service';

@Component({
  selector: 'app-admin-users',
  templateUrl: './admin-users.component.html',
  styleUrl: './admin-users.component.css'
})
export class AdminUsersComponent implements OnInit {
  //protected users!: User[];
  user: User = {} as User;

  allUsers: User[] = [];

  public userService: UsersServiceService
  constructor(private usersService: UsersServiceService, private cdr: ChangeDetectorRef) {
  this.userService = usersService;
  }

   ngOnInit(): void{
     this.getAllRegisteredUsers();
    }

    getAllRegisteredUsers(){
      this.userService
        .getUsers()
        .subscribe({
          next: (result: Array<User>) => {
            this.allUsers = result;
            this.cdr.detectChanges();
        }
      })
    }

  deleteUser(id: number){
    this.usersService.deleteUser(id).subscribe((data) =>{
      console.log('Deleted user: ', data);
      //this.allUsers = this.allUsers.find(u => u.id !== id)
    })
  }
}

import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {User} from '../../../../../core/models/user';
import {UsersServiceService} from '../../../../../core/services/users-service.service';

@Component({
  selector: 'app-admin-users',
  templateUrl: './admin-users.component.html',
  styleUrl: './admin-users.component.css'
})
export class AdminUsersComponent implements OnInit {
  //protected users!: User[];
  user: User = {} as User;

  allUsers: User[] = [];

  totalCount = 0;
  currentPage = 1;
  perPage = 10;

  public userService: UsersServiceService
  constructor(private usersService: UsersServiceService, private cdr: ChangeDetectorRef) {
  this.userService = usersService;
  }

   ngOnInit(): void{
     /*this.getAllRegisteredUsers();*/
     this.getAllUsers();
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

    getAllUsers(){
      const request = this.userService.getPaginatedUsers(this.currentPage, this.perPage);
      request.subscribe(res => {
        this.allUsers = res.items;
        this.totalCount = res.totalCount;
      })
    }

  deleteUser(id: number){
    this.usersService.deleteUser(id).subscribe((data) =>{
      console.log('Deleted user: ', data);
      //this.allUsers = this.allUsers.find(u => u.id !== id)
    })
  }

  changePage(page: number){
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    this.getAllUsers();
  }

  changePerPage(count: number){
    this.perPage = count;
    this.currentPage = 1;
    this.getAllUsers();
  }

  get totalPages(){
    return Math.ceil(this.totalCount / this.perPage);
  }
}

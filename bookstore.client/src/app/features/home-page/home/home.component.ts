import {Component, OnInit} from '@angular/core';
import {AuthService} from '../../../core/services/auth/auth.service';
import {User} from '../../../core/models/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{
  currentUser: User | null = null;

  constructor(public authService: AuthService) {}

  ngOnInit() {
    this.authService.getCurrentUser().subscribe((user) => {
      this.currentUser = user;
    })
  }
}

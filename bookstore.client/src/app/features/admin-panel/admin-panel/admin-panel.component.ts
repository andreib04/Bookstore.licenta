import {Component, OnInit} from '@angular/core';
import {Observable} from 'rxjs';
import {AuthService} from '../../../core/services/auth.service';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrl: './admin-panel.component.css'
})
export class AdminPanelComponent implements OnInit{
  isLoggedIn$!: Observable<boolean>;
  currentUser$!: Observable<any>;

  constructor(protected authService: AuthService) {
  }

  ngOnInit() {
    this.isLoggedIn$ = this.authService.isLoggedIn();
    this.currentUser$ = this.authService.getCurrentUser();
  }
}

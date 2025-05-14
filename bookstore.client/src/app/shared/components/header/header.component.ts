import {Component, OnInit} from '@angular/core';
import {debounceTime, Observable, Subject} from 'rxjs';

import {Router} from '@angular/router';
import {AuthService} from '../../../core/services/auth.service';
import {CartService} from '../../../core/services/cart.service';
import {SearchService} from '../../../core/services/search.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {
  isLoggedIn$!: Observable<boolean>;
  currentUser$!: Observable<any>;
  cartCount: number = 0;


  private searchSubject = new Subject<string>();

  constructor(protected authService: AuthService, private cartService: CartService) {}
  ngOnInit() {
    this.isLoggedIn$ = this.authService.isLoggedIn();
    this.currentUser$ = this.authService.getCurrentUser();

    this.cartService.cartCount$.subscribe(count => {
      this.cartCount = count;
    })
  }

}

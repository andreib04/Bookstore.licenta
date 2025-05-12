import {Component, OnInit} from '@angular/core';
import {Observable} from 'rxjs';
import {AuthService} from '../../../../core/services/auth.service';
import {CartService} from '../../../../core/services/cart.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {
  isLoggedIn$!: Observable<boolean>;
  currentUser$!: Observable<any>;
  cartCount: number = 0;

  constructor(protected authService: AuthService, private cartService: CartService) {}
  ngOnInit() {
    this.isLoggedIn$ = this.authService.isLoggedIn();
    this.currentUser$ = this.authService.getCurrentUser();

    this.cartService.cartCount$.subscribe(count => {
      this.cartCount = count;
    })
  }
}

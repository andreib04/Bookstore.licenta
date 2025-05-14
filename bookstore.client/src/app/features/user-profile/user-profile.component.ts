import {Component, OnInit} from '@angular/core';
import {AuthService} from '../../core/services/auth.service';
import {OrderService} from '../../core/services/order.service';
import {Order} from '../../core/models/Order';
import {User} from '../../core/models/user';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent implements OnInit{
  orders: Order[] = [];
  user: User | null = null;

  constructor(private authService: AuthService, private orderService: OrderService) {}

  ngOnInit() {
    this.loadOrders();
  }

  loadOrders() {
    this.authService.getCurrentUser().subscribe(user => {
      this.user = user;

      if(user) {
        this.orderService.getUserOrders(user.id).subscribe(orders => {
          this.orders = orders;
        });
      }
    });
  }
}

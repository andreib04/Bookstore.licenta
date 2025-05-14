import {Component, OnInit} from '@angular/core';
import {Order} from '../../../../../core/models/Order';
import {OrderService} from '../../../../../core/services/order.service';

@Component({
  selector: 'app-admin-orders',
  templateUrl: './admin-orders.component.html',
  styleUrl: './admin-orders.component.css'
})
export class AdminOrdersComponent implements OnInit{
  orders: Order[] = [];
  page = 1;
  perPage = 10;
  sortBy = 'createdAt';
  sortOrder = 'desc';

  constructor(private orderService: OrderService) {}

  ngOnInit() {
    this.getAllOrders();
  }

  getAllOrders(){
    this.orderService.getAllOrders(this.sortBy, this.sortOrder, this.page, this.perPage).subscribe(orders => {
      this.orders = orders;
    })
  }

  deleteOrder(id: number){
    this.orderService.deleteOrder(id).subscribe(res => {
      console.log("Order deleted: ", res);
    })
  }

  nextPage(){
    this.page++;
    this.getAllOrders();
  }

  prevPage(){
    if(this.page > 1){
      this.page--;
      this.getAllOrders();
    }
  }
}

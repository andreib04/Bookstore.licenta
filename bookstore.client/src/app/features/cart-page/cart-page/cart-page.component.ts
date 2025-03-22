import { Component } from '@angular/core';

@Component({
  selector: 'app-cart-page',
  templateUrl: './cart-page.component.html',
  styleUrl: './cart-page.component.css'
})
export class CartPageComponent {
    count: number = 1;

    increase(){
      this.count++;
    }

    decrease(){
      if(this.count > 1){
        this.count--;
      }
    }
}

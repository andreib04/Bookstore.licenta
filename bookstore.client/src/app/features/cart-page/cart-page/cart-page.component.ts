import {Component, OnInit} from '@angular/core';
import {CartItem} from '../../../core/models/cartItem';
import {CartService} from '../../../core/services/cart.service';
import {BooksServiceService} from '../../../core/services/books-service.service';
import {MagazinesServiceService} from '../../../core/services/magazines-service.service';
import {map} from 'rxjs/operators';
import {forkJoin, Observable} from 'rxjs';
import {AuthService} from '../../../core/services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-cart-page',
  templateUrl: './cart-page.component.html',
  styleUrl: './cart-page.component.css'
})
export class CartPageComponent implements OnInit{
  isLoggedIn$!: Observable<boolean>;
  cartItems: CartItem[] = [];
  totalPrice: number = 0;
  isLoading = false;

  constructor(private cartService: CartService,
              private bookService: BooksServiceService,
              private magazineService: MagazinesServiceService,
              private authService: AuthService,
              private router: Router) {
    this.isLoggedIn$ = this.authService.isLoggedIn();
  }

  ngOnInit() {
    this.loadCartItems();
  }

  loadCartItems() {
    this.isLoading = true;
    this.cartService.getCart().subscribe(items => {
      const itemObservables = items.map(item => {
        if (item.productType === 'Book') {
          return this.bookService.getOneBook(item.productId).pipe(
            map(book => ({
              ...item,
              title: book.title,
              description: book.description,
              price: book.price,
              image: book.image,
              subtotal: book.price * item.quantity
            }))
          );
        } else {
          return this.magazineService.getOneMagazine(item.productId).pipe(
            map(magazine => ({
              ...item,
              title: magazine.title,
              description: magazine.description,
              price: magazine.price,
              image: magazine.image,
              subtotal: magazine.price * item.quantity
            }))
          );
        }
      });

      forkJoin(itemObservables).subscribe(displayItems => {
        this.cartItems = displayItems;
        this.totalPrice = this.cartItems.reduce((sum, item) => sum + item.subtotal, 0);
      });

      this.isLoading = false;
    });
  }

  updateQuantity(item: CartItem, change: number){
    const newQuantity = item.quantity + change;

    if(newQuantity <= 0){
      this.removeItem(item);
      return;
    }

    this.cartService.updateQuantity(item.productId, item.productType, newQuantity)
      .subscribe(() => {
        item.quantity = newQuantity;
        item.subtotal = item.price * newQuantity;
        this.totalPrice = this.cartItems.reduce((sum, i) => sum + i.subtotal, 0);
      });
  }

  removeItem(item: CartItem){
    this.cartService.removeFromCart(item.productId, item.productType).subscribe(() => {
      this.cartItems = this.cartItems.filter(i =>
        !(i.productId === item.productId && i.productType === item.productType)
      );
      this.totalPrice = this.cartItems.reduce((sum, i) => sum + i.subtotal, 0);
    })
  }

  onCheckout(){
    this.isLoggedIn$.subscribe(isloggedIn => {
      if(isloggedIn){
        this.router.navigate(['/checkout']);
      }else{
        this.router.navigate(['/login']);
      }
    })
  }
}

import {Component, OnInit} from '@angular/core';
import {CartItem} from '../../core/models/cartItem';
import {forkJoin} from 'rxjs';
import {AuthService} from '../../core/services/auth.service';
import {FormArray, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {User} from '../../core/models/user';
import {OrderService} from '../../core/services/order.service';
import {CartService} from '../../core/services/cart.service';
import {Order} from '../../core/models/Order';
import {BooksServiceService} from '../../core/services/books-service.service';
import {MagazinesServiceService} from '../../core/services/magazines-service.service';
import {map} from 'rxjs/operators';
import {Router} from '@angular/router';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css'
})
export class CheckoutComponent implements OnInit {
  checkoutForm!: FormGroup;
  currentUser: User | null = null;
  cartItems: CartItem[] = [];
  totalPrice: number = 0;

  constructor(private authService: AuthService, private fb: FormBuilder, private orderService: OrderService, private cartService: CartService
  , private bookService: BooksServiceService, private magazineService: MagazinesServiceService, private router: Router) {
    this.checkoutForm = this.fb.group({
      userId: [null],
      items: this.fb.array([]),
      total: [0],
      status: ['pending'],
      address: ['', Validators.required],
      city: ['', Validators.required],
      postalCode: ['', Validators.required],
      country: ['', Validators.required],

      //card simulation
      cardNumber: ['', [Validators.required, Validators.pattern(/^\d{16}$/)]],
      cardHolder: ['', Validators.required],
      expiration: ['', [Validators.required, Validators.pattern(/^(0[1-9]|1[0-2])\/\d{2}$/)]],
      cvv: ['', [Validators.required, Validators.pattern(/^\d{3,4}$/)]]
    });
  }

  ngOnInit() {
    this.getCurrentUserId();
    this.loadCartItems();
  }

  onSubmit(){
    console.log('Form valid?', this.checkoutForm.valid);
    console.log('Form value:', this.checkoutForm.value);

    if(this.checkoutForm.valid){


      const order: Order = {
        userId: this.checkoutForm.get('userId')?.value,
        address: this.checkoutForm.get('address')?.value,
        city: this.checkoutForm.get('city')?.value,
        postalCode: this.checkoutForm.get('postalCode')?.value,
        country: this.checkoutForm.get('country')?.value,
        status: this.checkoutForm.get('status')?.value,
        total: this.checkoutForm.get('total')?.value,
        createdAt: new Date(),
        items: (this.checkoutForm.get('items') as FormArray).value
      } as Order

      console.log('Order to submit:', order);

      this.orderService.checkout(order).subscribe({
        next: (result) => {
          console.log('Order placed', result);
          alert('Order placed successfully.');
          this.cartService.clearCart();
          this.checkoutForm.reset();
          this.router.navigate(['/home']);
        },
        error: (err) => {
          console.error(err);
          alert('Something went wrong.')
        }
      })
    }else{
      console.log('form is invalid')
    }
  }

  getCurrentUserId(){
    this.authService.getCurrentUser().subscribe(user => {
      if(user) {
        this.currentUser = user;
        this.checkoutForm.patchValue({ userId: user.id })
        console.log('Patched userId:', user.id);
      }
    });
  }

  loadCartItems(){
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

        const itemsFormArray = this.checkoutForm.get('items') as FormArray;
        this.cartItems.forEach(item => {
          itemsFormArray.push(this.fb.group({
            productId: [item.productId],
            productType: [item.productType],
            quantity: [item.quantity],
            price: [item.price]
          }));
        });

        this.checkoutForm.patchValue({ total: this.totalPrice });
        console.log('Patched total:', this.totalPrice);
      });
    });
  }
}

import {Component, OnInit} from '@angular/core';
import {BooksServiceService} from '../../../core/services/books-service.service';
import {ActivatedRoute} from '@angular/router';
import {Book} from '../../../core/models/book';
import {CartItem} from '../../../core/models/cartItem';
import {CartService} from '../../../core/services/cart.service';

@Component({
  selector: 'app-book-page',
  templateUrl: './book-page.component.html',
  styleUrl: './book-page.component.css'
})
export class BookPageComponent implements OnInit {
  book!: Book;
  fallbackUrl = "https://images.unsplash.com/photo-1532012197267-da84d127e765?q=80&w=1287&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";

  constructor(private booksService: BooksServiceService, private route: ActivatedRoute, private cartService: CartService) {
  }
  ngOnInit() {
    this.getOnebook();
  }

  getOnebook(){
    let id: number = +this.route.snapshot.params['id'];

    this.booksService.getOneBook(id).subscribe({
      next: (book) => {
        this.book = book;
      },
      error: err => {
        console.log(err);
      }
    })
  }

  addToCart(book: Book){
    const item: CartItem = {
      productId: book.id,
      productType: 'Book',
      quantity: 1
    } as CartItem;

    this.cartService.addToCart(item).subscribe(() => {
      console.log("Product added to cart!")
    })
  }
}

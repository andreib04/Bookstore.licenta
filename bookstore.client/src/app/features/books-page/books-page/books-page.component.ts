import {Component, OnInit} from '@angular/core';
import {BooksServiceService} from '../../../core/services/books-service.service';
import {Book} from '../../../core/models/book';
import {CategoriesServiceService} from '../../../core/services/categories-service.service';
import {Category} from '../../../core/models/category';
import {CartService} from '../../../core/services/cart.service';
import {CartItem} from '../../../core/models/cartItem';

@Component({
  selector: 'app-books-page',
  templateUrl: './books-page.component.html',
  styleUrl: './books-page.component.css'
})
export class BooksPageComponent implements OnInit {
  allBooks: Book[] = [];
  allCategories: Category[] = [];
  fallbackUrl = "https://images.unsplash.com/photo-1532012197267-da84d127e765?q=80&w=1287&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";
  isLoading = false;

  totalCount = 0;
  currentPage = 1;
  perPage = 3;

  activeSort = {
    sortBy: 'price',
    sortOrder: 'asc'
  }

  categoryId?: number = undefined;

  constructor(private bookService: BooksServiceService,
              private categoriesService: CategoriesServiceService,
              private cartService: CartService){}

  ngOnInit() {
    this.getAllCategories();
    this.loadBooks();
  }

  loadBooks() {
    this.isLoading = true;

    const { sortBy, sortOrder } = this.activeSort;

    const request = this.categoryId
        ? this.bookService.getByCategory(this.categoryId, this.currentPage, this.perPage, sortBy, sortOrder)
        : this.bookService.getSortedPaginated(this.currentPage, this.perPage, sortBy, sortOrder);

      request.subscribe(res => {
        this.allBooks = res.items;
        this.totalCount = res.totalCount;
        this.isLoading = false;
      })
  }

  sortBooks(sortBy: string, sortOrder: string) {
    this.activeSort = { sortBy, sortOrder };
    this.currentPage = 1;
    this.loadBooks();
  }

  selectCategory(categoryId: number | undefined) {
    this.categoryId = categoryId;
    this.currentPage = 1;
    this.loadBooks();
  }

  changePage(page: number){
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    this.loadBooks();
  }

  changePerPage(count: number){
    this.perPage = count;
    this.currentPage = 1;
    this.loadBooks();
  }

  get totalPages(){
    return Math.ceil(this.totalCount / this.perPage);
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

  getAllCategories(){
    this.categoriesService.getCategory().subscribe({
      next: (category) => {
        this.allCategories = category;
        console.log(this.allCategories);
      },
      error: (error) => {
        console.log(error);
      }
    })
  }
}

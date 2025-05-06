import {Component, OnInit} from '@angular/core';
import {BooksServiceService} from '../../../core/services/books-service/books-service.service';
import {Book} from '../../../core/models/book';
import {CategoriesServiceService} from '../../../core/services/categories-service/categories-service.service';
import {Category} from '../../../core/models/category';
import {Observable} from 'rxjs';

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
  activeSort: {sortBy: string, sortOrder: string} = {sortBy: '', sortOrder: ''};
  totalCount = 0;
  currentPage = 1;
  perPage = 20;


  constructor(private bookService: BooksServiceService, private categoriesService: CategoriesServiceService){}

  ngOnInit() {
    this.loadBooks();
    this.getAllCategories();
  }

  loadBooks(){
    this.isLoading = true;
    this.bookService.getPaginatedBooks(this.currentPage, this.perPage).subscribe({
      next: (data) => {
        this.allBooks = data.books;
        this.totalCount = data.totalCount;
        this.isLoading = false;
      },
      error: (error) => {
        console.log(error);
      }
    })
  }

  getByCategory(categoryId: number){
    this.isLoading = true;
    this.bookService.getBookByCategory(categoryId).subscribe({
      next: (data) => {
        this.allBooks = data;
        this.isLoading = false;
      },
      error: (error) => {
        console.log(error);
      }
    })
  }

  changePage(page: number){
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

  sortBooks(sortBy: string, sortOrder: string){
    this.isLoading = true;
    this.activeSort = {sortBy, sortOrder};
    this.bookService.getSortedBooks(sortBy, sortOrder).subscribe({
      next: (data) => {
        this.allBooks = data;
        //this.loadBooks();
        this.isLoading = false;
      },
      error: (error) => {
        console.log(error);
      }
    })
  }
}

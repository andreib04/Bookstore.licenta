import {Component, OnInit} from '@angular/core';
import {BooksServiceService} from '../../../core/services/books-service/books-service.service';
import {Book} from '../../../core/models/book';
import {CategoriesServiceService} from '../../../core/services/categories-service/categories-service.service';
import {Category} from '../../../core/models/category';

@Component({
  selector: 'app-books-page',
  templateUrl: './books-page.component.html',
  styleUrl: './books-page.component.css'
})
export class BooksPageComponent implements OnInit {
  allBooks: Book[] = [];
  allCategories: Category[] = [];

  constructor(private bookService: BooksServiceService, private categoriesService: CategoriesServiceService){}

  ngOnInit() {
    this.getAllBooks();
    this.getAllCategories();
  }

  getAllBooks(){
    this.bookService.getBooks().subscribe({
      next: (books) => {
        this.allBooks = books;
        console.log(this.allBooks);
      },
      error: (error) => {
        console.log(error);
      }
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

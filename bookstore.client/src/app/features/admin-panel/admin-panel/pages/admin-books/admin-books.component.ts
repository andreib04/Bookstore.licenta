import {Component, OnInit} from '@angular/core';
import {BooksServiceService} from '../../../../../core/services/books-service/books-service.service';
import {Book} from '../../../../../core/models/book';

@Component({
  selector: 'app-admin-books',
  templateUrl: './admin-books.component.html',
  styleUrl: './admin-books.component.css'
})
export class AdminBooksComponent implements OnInit{

  allBooks: Book[] = [];
  book: Book = {} as Book;

  constructor(private bookService: BooksServiceService) {
  }

  ngOnInit() {
    this.getAllBooks();
  }

  getAllBooks(){
    this.bookService.getBooks().subscribe(res => {
      this.allBooks = res;
    })
  }

  deleteBook(){
    this.bookService.deleteBook(this.book.id).subscribe(res => {
      console.log('Deleted book', res);
    })
  }
}

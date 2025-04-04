import {Component, OnInit} from '@angular/core';
import {BooksServiceService} from '../../../../../core/services/books-service/books-service.service';
import {Book} from '../../../../../core/models/book';
import {Router} from '@angular/router';

@Component({
  selector: 'app-admin-books',
  templateUrl: './admin-books.component.html',
  styleUrl: './admin-books.component.css'
})
export class AdminBooksComponent implements OnInit{

  allBooks: Book[] = [];
  book: Book = {} as Book;

  constructor(private bookService: BooksServiceService, private router: Router) {
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

  goToEditPage(modelType: string, id:number){
    this.router.navigate([`/admin/edit/${modelType}/${id}`]);
  }
}

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

  deleteBook(id: number){
    this.bookService.deleteBook(id).subscribe(res => {
      this.allBooks = this.allBooks.filter(b => b.id !== id);
    })
  }

  goToEditPage(modelType: string, id:number){
    this.router.navigate([`/admin/edit/${modelType}/${id}`]);
  }
}

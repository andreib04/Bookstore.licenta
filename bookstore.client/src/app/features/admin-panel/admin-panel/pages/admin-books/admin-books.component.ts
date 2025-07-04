import {Component, OnInit} from '@angular/core';
import {BooksServiceService} from '../../../../../core/services/books-service.service';
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
  isLoading = false;

  totalCount = 0;
  currentPage = 1;
  perPage = 5;

  activeSort = {
    sortBy: 'price',
    sortOrder: 'asc'
  }

  constructor(private bookService: BooksServiceService, private router: Router) {
  }

  ngOnInit() {
    this.getAllBooks();
  }

  /*getAllBooks(){
    this.isLoading = true;
    this.bookService.getBooks().subscribe({
      next: (books) => {
        this.allBooks = books;
        console.log(this.allBooks);
        this.isLoading = false;
      },
      error: (error) => {
        console.log(error);
      }
    })
  }*/
  getAllBooks(){
    this.isLoading = true;
    const {sortBy, sortOrder} = this.activeSort;

    const request = this.bookService.getSortedPaginated(this.currentPage, this.perPage, sortBy, sortOrder);
    request.subscribe(res => {
      this.allBooks = res.items;
      this.totalCount = res.totalCount;
      this.isLoading = false;
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

  sortBooks(sortBy: string, sortOrder: string) {
    this.activeSort = { sortBy, sortOrder };
    this.currentPage = 1;
    this.getAllBooks();
  }

  changePage(page: number){
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    this.getAllBooks();
  }

  changePerPage(count: number){
    this.perPage = count;
    this.currentPage = 1;
    this.getAllBooks();
  }

  get totalPages(){
    return Math.ceil(this.totalCount / this.perPage);
  }
}

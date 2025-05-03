import {Component, OnInit} from '@angular/core';
import {AuthService} from '../../../core/services/auth/auth.service';
import {User} from '../../../core/models/user';
import {BooksServiceService} from '../../../core/services/books-service/books-service.service';
import {MagazinesServiceService} from '../../../core/services/magazines-service/magazines-service.service';
import {Book} from '../../../core/models/book';
import {Magazine} from '../../../core/models/magazine';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{
  currentUser: User | null = null;
  latestBooks: Book[] = [];
  latestMagazines: Magazine[] = [];
  book!: Book;
  magazine!: Magazine[];
  fallbackUrl = "https://images.unsplash.com/photo-1532012197267-da84d127e765?q=80&w=1287&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";

  constructor(public authService: AuthService,
              private booksService: BooksServiceService,
              private magazinesService: MagazinesServiceService) {}

  ngOnInit() {
    this.getCurrentUser();
    this.getLatestBooks();
    this.getLatestMagazines();
  }

  getCurrentUser() {
    this.authService.getCurrentUser().subscribe((user) => {
      this.currentUser = user;
    })
  }

  getLatestBooks(){
    this.booksService.getLatestBooks().subscribe(books => {
      this.latestBooks = books;
    })
  }

  getLatestMagazines(){
    this.magazinesService.getLatestMagazines().subscribe(magazines => {
      this.latestMagazines = magazines;
    })
  }
}

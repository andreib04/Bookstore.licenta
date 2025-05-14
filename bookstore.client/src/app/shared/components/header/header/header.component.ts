import {Component, OnInit} from '@angular/core';
import {debounceTime, Observable, Subject} from 'rxjs';
import {AuthService} from '../../../../core/services/auth.service';
import {CartService} from '../../../../core/services/cart.service';
import {SearchService} from '../../../../core/services/search.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {
  isLoggedIn$!: Observable<boolean>;
  currentUser$!: Observable<any>;
  cartCount: number = 0;

  searchTerm = '';
  suggestions: any[] = [];
  showSuggestions = false;

  private searchSubject = new Subject<string>();

  constructor(protected authService: AuthService, private cartService: CartService, private searchService: SearchService, private router: Router) {
    this.searchSubject.pipe(debounceTime(300)).subscribe((query) => {
      if(query.trim()){
        this.searchService.search(query).subscribe((results) => {
          this.suggestions = results;
        });
      }else{
        this.suggestions = [];
      }
    })
  }
  ngOnInit() {
    this.isLoggedIn$ = this.authService.isLoggedIn();
    this.currentUser$ = this.authService.getCurrentUser();

    this.cartService.cartCount$.subscribe(count => {
      this.cartCount = count;
    })
  }

  onInputChange(){
    this.searchSubject.next(this.searchTerm);
  }

  selectSuggestion(name: string){
    this.searchTerm = name;
    this.showSuggestions = false;
    this.router.navigate(['/search-results'], { queryParams: { q: name } });
  }

  hideSuggestions() {
    setTimeout(() => (this.showSuggestions = false), 200);
  }
}

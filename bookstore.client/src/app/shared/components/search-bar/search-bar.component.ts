import {Component, ElementRef, HostListener} from '@angular/core';
import {debounceTime, Subject} from 'rxjs';
import {SearchService} from '../../../core/services/search.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrl: './search-bar.component.css'
})
export class SearchBarComponent {
  searchQuery = '';
  suggestions: any[] = [];
  private searchChanged = new Subject<string>();

  constructor(private searchService: SearchService, private router: Router, private eRef: ElementRef) {
    this.searchChanged.pipe(debounceTime(300)).subscribe((query) => {
      if (query.trim().length > 1) {
        this.searchService.search(query).subscribe((results) => {
          this.suggestions = results;
        });
      } else {
        this.suggestions = [];
      }
    });
  }

  onSearchChange(){
    this.searchChanged.next(this.searchQuery);
  }

  submitSearch(){
    this.suggestions = [];
    this.router.navigate(['/search-results'], {
      queryParams: { query: this.searchQuery },
    })
  }
  selectSuggestion(item: any){
    this.searchQuery = item.title;
    this.suggestions = [];
    this.submitSearch();
  }



  @HostListener('document:click', ['$event'])
  handleClickOutside(event: Event){
    if(!this.eRef.nativeElement.contains(event.target)){
      this.suggestions = [];
    }
  }

}

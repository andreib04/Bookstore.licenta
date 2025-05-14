import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {SearchService} from '../../core/services/search.service';
import {Magazine} from '../../core/models/magazine';
import {CartItem} from '../../core/models/cartItem';
import {CartService} from '../../core/services/cart.service';

@Component({
  selector: 'app-search-results',
  templateUrl: './search-results.component.html',
  styleUrl: './search-results.component.css'
})
export class SearchResultsComponent implements OnInit{
  results: any[] = [];
  query: string = '';
  isLoading = false;
  fallBackUrl = "https://images.unsplash.com/photo-1596382940920-9f73b2d15901?q=80&w=1274&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";
  constructor(private route: ActivatedRoute, private searchService: SearchService, private router: Router, private cartService: CartService){}

  ngOnInit() {
    this.isLoading = true;
    this.route.queryParams.subscribe((params) => {
      this.query = params['query'];
      if(this.query){
        this.searchService.search(this.query).subscribe((results) => {
          this.results = results;
          this.isLoading = false;
        })
      }
    })
  }

  goToItem(item: any){
    const type = item.itemType.toLowerCase();
    const id = item.id;

    this.router.navigate([`/${type}/${id}`]);
  }

  addToCart(item: any){
    const type = item.itemType;

    const cartItem: CartItem = {
      productId: item.id,
      productType: type,
      quantity: 1
    } as CartItem;

    this.cartService.addToCart(item).subscribe(() => {
      console.log("Product added to cart!")
    })
  }
}



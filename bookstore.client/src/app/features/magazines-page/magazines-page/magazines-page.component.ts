import {Component, OnInit} from '@angular/core';
import {Magazine} from '../../../core/models/magazine';
import {Category} from '../../../core/models/category';
import {MagazinesServiceService} from '../../../core/services/magazines-service/magazines-service.service';
import {CategoriesServiceService} from '../../../core/services/categories-service/categories-service.service';

@Component({
  selector: 'app-magazines-page',
  templateUrl: './magazines-page.component.html',
  styleUrl: './magazines-page.component.css'
})
export class MagazinesPageComponent implements OnInit{
  allMagazines: Magazine[] = [];
  allCategories: Category[] = [];
  fallBackUrl = "https://images.unsplash.com/photo-1596382940920-9f73b2d15901?q=80&w=1274&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";
  isLoading = false;
  activeSort: {sortBy: string, sortOrder: string} = {sortBy: '', sortOrder: ''};
  totalCount = 0;
  currentPage = 1;
  perPage = 20;


  constructor(private magazineService: MagazinesServiceService, private categoriesService: CategoriesServiceService){}

  ngOnInit() {
    this.loadMagazines();
    this.getAllCategories();
  }

  loadMagazines(){
    this.isLoading = true;
    this.magazineService.getPaginatedMagazines(this.currentPage, this.perPage).subscribe({
      next: (data) => {
        this.allMagazines = data.magazines;
        this.totalCount = data.totalCount;
        this.isLoading = false;
      },
      error: (error) => {
        console.log(error);
      }
    })
  }

  changePage(page: number){
    this.currentPage = page;
    this.loadMagazines();
  }

  changePerPage(count: number){
    this.perPage = count;
    this.currentPage = 1;
    this.loadMagazines();
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

  sortMagazines(sortBy: string, sortOrder: string){
    this.isLoading = true;
    this.activeSort = {sortBy, sortOrder};
    this.magazineService.getSortedMagazines(sortBy, sortOrder).subscribe(
      data => {
        this.allMagazines = data;
        this.isLoading = false;
      },
      error => {
        console.error("Error sorting magazines", error);
    }
    )
  }
}

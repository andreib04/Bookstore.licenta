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


  totalCount = 0;
  currentPage = 1;
  perPage = 20;

  activeSort = {
    sortBy: 'price',
    sortOrder: 'asc'
  }

  categoryId?: number = undefined;

  constructor(private magazineService: MagazinesServiceService, private categoriesService: CategoriesServiceService){}

  ngOnInit() {
    this.getAllCategories();
    this.loadMagazines();
  }

  loadMagazines(){
    this.isLoading = true;

    const { sortBy, sortOrder } = this.activeSort;

    const request = this.categoryId
      ? this.magazineService.getByCategory(this.categoryId, this.currentPage, this.perPage, sortBy, sortOrder)
      : this.magazineService.getSortedPaginated(this.currentPage, this.perPage, sortBy, sortOrder);

    request.subscribe(res => {
      this.allMagazines = res.items;
      this.totalCount = res.totalCount;
      this.isLoading = false;
    })
  }

  sortMagazines(sortBy: string, sortOrder: string){
    this.activeSort = {sortBy, sortOrder};
    this.currentPage = 1;
    this.loadMagazines();
  }

  selectCategory(categoryId: number | undefined){
    this.categoryId = categoryId;
    this.currentPage = 1;
    this.loadMagazines();
  }

  changePage(page: number){
    if (page < 1 || page > this.totalPages) return;
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
}

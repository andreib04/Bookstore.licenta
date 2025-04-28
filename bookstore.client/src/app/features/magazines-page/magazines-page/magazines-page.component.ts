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

  constructor(private magazineService: MagazinesServiceService, private categoriesService: CategoriesServiceService){}

  ngOnInit() {
    this.getAllMagazines();
    this.getAllCategories();
  }

  getAllMagazines(){
    this.magazineService.getMagazines().subscribe({
      next: (mag) => {
        this.allMagazines = mag;
        console.log(this.allMagazines);
      },
      error: (error) => {
        console.log(error);
      }
    })
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

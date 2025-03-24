import { Component } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {CategoriesServiceService} from '../../../../../core/services/categories-service/categories-service.service';
import {Category} from '../../../../../core/models/category';

@Component({
  selector: 'app-admin-categories',
  templateUrl: './admin-categories.component.html',
  styleUrl: './admin-categories.component.css'
})
export class AdminCategoriesComponent {

  categories: Category[] = [];

  form: FormGroup = new FormGroup({
    categoryName: new FormControl<string>('', [Validators.required])
  });
  constructor(private categoryService: CategoriesServiceService) {
    this.categoryService.getCategory().subscribe(category => {
      console.log('Received categories: ',category);
      this.categories = category;
    })
  }

  onSubmit(){
    if(this.form.valid){
      let category: Category = {
        CategoryName: this.form.controls['categoryName'].value
      } as Category;

      this.categoryService.postCategory(category).subscribe(res => {
        console.log(res);
        console.log('Category added successfully.');
      });

    }else{
      console.log(this.form.errors);
      console.log('Form is invalid');
    }
  }
}

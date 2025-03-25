import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {CategoriesServiceService} from '../../../../../core/services/categories-service/categories-service.service';
import {Category} from '../../../../../core/models/category';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-admin-categories',
  templateUrl: './admin-categories.component.html',
  styleUrl: './admin-categories.component.css'
})
export class AdminCategoriesComponent implements OnInit {

  categories: Category[] = [];
  category: Category = {} as Category;

  form: FormGroup = new FormGroup({
    categoryName: new FormControl<string>('', [Validators.required])
  });

  constructor(private categoryService: CategoriesServiceService, private cdr: ChangeDetectorRef
  ,private activatedRoute: ActivatedRoute) {
    let id: number = +this.activatedRoute.snapshot.params['id'];
  }

  ngOnInit() {
    this.fetchItems();
  }

  fetchItems(){
    this.categoryService.getCategory().subscribe({
      next: (data) => {
        console.log('Categories data: ', data);
        this.categories = data;
        console.log('Category array: ', this.categories);
      },
      error: (error) => console.log('Error fetching data', error)
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
        this.fetchItems();
        this.form.reset();
      });

    }else{
      console.log(this.form.errors);
      console.log('Form is invalid');
    }
  }

  deleteCategory(){
    this.categoryService.deleteCategory(this.category.CategoryId).subscribe(res => {
      console.log('Deleted category', res);
      window.location.reload();
    })
  }
}

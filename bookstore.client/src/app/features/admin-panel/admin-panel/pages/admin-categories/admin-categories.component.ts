import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {CategoriesServiceService} from '../../../../../core/services/categories-service/categories-service.service';
import {Category} from '../../../../../core/models/category';

@Component({
  selector: 'app-admin-categories',
  templateUrl: './admin-categories.component.html',
  styleUrl: './admin-categories.component.css'
})
export class AdminCategoriesComponent implements OnInit {

  categories: Category[] = [];

  form: FormGroup = new FormGroup({
    categoryName: new FormControl<string>('', [Validators.required])
  });

  constructor(private categoryService: CategoriesServiceService, private cdr: ChangeDetectorRef) {
  }

  ngOnInit() {
    this.fetchItems();
  }

  fetchItems(){
    this.categoryService.getCategory().subscribe({
      next: (data) => {
        console.log('Categories data: ', data);
        this.categories = data;
        this.cdr.detectChanges();
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
}

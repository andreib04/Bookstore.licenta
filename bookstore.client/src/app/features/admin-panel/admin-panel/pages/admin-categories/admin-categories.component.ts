import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {CategoriesServiceService} from '../../../../../core/services/categories-service.service';
import {Category} from '../../../../../core/models/category';

@Component({
  selector: 'app-admin-categories',
  templateUrl: './admin-categories.component.html',
  styleUrl: './admin-categories.component.css'
})
export class AdminCategoriesComponent implements OnInit {

  totalCount = 0;
  currentPage = 1;
  perPage = 10;

  categories: Category[] = [
    {
      categoryId: 1,
      categoryName: "Romance"
    }
  ];
  category: Category = {} as Category;

  form: FormGroup = new FormGroup({
    categoryName: new FormControl<string>('', [Validators.required])
  });

  constructor(private categoryService: CategoriesServiceService, private cdr: ChangeDetectorRef,) {}

  ngOnInit() {
    /*this.fetchItems();*/
    this.getAllCategories();
  }

  getAllCategories(){
    const request = this.categoryService.getPaginatedCategory(this.currentPage, this.perPage);
    request.subscribe(res => {
      this.categories = res.items;
      this.totalCount = res.totalCount;
    })
  }

  fetchItems(){
    this.categoryService.getCategory().subscribe({
      next: (data) => {
        this.categories = data;
      },
      error: (error) => console.log('Error fetching data', error)
    })
  }

  onSubmit(){
    if(this.form.valid){
      let category: Category = {
        categoryName: this.form.controls['categoryName'].value
      } as Category;

      this.categoryService.postCategory(category).subscribe(res => {
        console.log('Category added successfully.');
        this.fetchItems();
        this.form.reset();
      });

    }else{
      console.log(this.form.errors);
      console.log('Form is invalid');
    }
  }

  editCategory(category: Category){
    if (!category) {
      console.error('Tried to edit a null category');
      return;
    }

    category.isEditing = true;
    category.tempName = category.categoryName;
  }

  saveCategory(category: Category){
    if(!category || !category.categoryId){
      console.error('Invalid category: ', category);
      return;
    }

    if(!category.tempName?.trim())
      return;

    const updatedCategory = { ...category, categoryName: category.tempName };

    this.categoryService.updateCategory(category.categoryId, updatedCategory).subscribe({
      next: (res) => {
        console.log('api response', res);

        if(!res){
          console.error('api returned null');
          return;
        }

        category.categoryName = res.categoryName;
        category.isEditing = false;
        console.log('Category updated successfully: ', res);

        window.location.reload();
      },
      error: (err) => {
        console.error('Error updating category: ', err);
      }
    })
  }

  cancelEdit(category: Category){
    category.isEditing = false;
  }

  deleteCategory(id: number){
    this.categoryService.deleteCategory(id).subscribe(res => {
      console.log('Deleted category', res);
      this.categories = this.categories.filter(c => c.categoryId !== id)
    })
  }

  changePage(page: number){
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    this.getAllCategories();
  }

  changePerPage(count: number){
    this.perPage = count;
    this.currentPage = 1;
    this.getAllCategories();
  }

  get totalPages(){
    return Math.ceil(this.totalCount / this.perPage);
  }
}

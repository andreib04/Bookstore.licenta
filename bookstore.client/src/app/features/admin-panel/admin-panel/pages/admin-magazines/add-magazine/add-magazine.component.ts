import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {MagazinesServiceService} from '../../../../../../core/services/magazines-service/magazines-service.service';
import {Location} from '@angular/common';
import {CategoriesServiceService} from '../../../../../../core/services/categories-service/categories-service.service';
import {Category} from '../../../../../../core/models/category';
import {Magazine} from '../../../../../../core/models/magazine';

@Component({
  selector: 'app-add-magazine',
  templateUrl: './add-magazine.component.html',
  styleUrl: './add-magazine.component.css'
})
export class AddMagazineComponent implements OnInit{
  categories: Category[] = [];
  allMagazines: Magazine[] = [];
  form: FormGroup;
  errorMessage: string = '';

  constructor(private magazineService: MagazinesServiceService, private location: Location, private categoriesService: CategoriesServiceService) {
    this.form = new FormGroup({
      title: new FormControl<string>('', [Validators.required]),
      publisher: new FormControl<string>('', [Validators.required]),
      description: new FormControl<string>('', [Validators.required]),
      price: new FormControl<number>(0, [Validators.required]),
      stock: new FormControl<number>(0, [Validators.required]),
      itemType: new FormControl<string>('', [Validators.required]),
      image: new FormControl<string>('', [Validators.required]),
     //releaseDate: new FormControl<string>(this.today, [Validators.required]),
      category: new FormGroup({
        categoryId: new FormControl<number | null>(null, Validators.required),
        categoryName: new FormControl('', Validators.required),
      })
    })
  }

  ngOnInit() {
    this.categoriesService.getCategory().subscribe(data => {
      this.categories = data;
    })
  }

  onFileChange(event: Event){
    const file = (event.target as HTMLInputElement)?.files?.[0];
    if(file) {

      const maxSize = 2 * 1024 * 1024;

      if(file.size > maxSize){
        this.errorMessage = 'File size exceeds 2 MB';
        this.form.get('image')?.setValue('');
        this.form.get('image')?.updateValueAndValidity();
        return;
      }else{
        this.errorMessage = '';
      }

      const reader = new FileReader();
      reader.onload = () => {
        const base64 = (reader.result as string).split(',')[1];

        this.form.patchValue({
          image: base64
        });

        this.form.get('image')?.updateValueAndValidity();
      };
      reader.readAsDataURL(file);
    }
  }

  onSubmit() {
    if(this.form.valid){
      const formValue = this.form.value;

      const magazine = {
        title: formValue.title,
        publisher: formValue.publisher,
        description: formValue.description,
        price: formValue.price,
        stock: formValue.stock,
        itemType: formValue.itemType,
        image: formValue.image,
        category: {
          categoryId: formValue.category.categoryId,
          categoryName: formValue.category.categoryName
        }
      } as Magazine;

      this.magazineService.postMagazine(magazine).subscribe(res => {
        console.log('New magazine: ', res);
        this.getAllMagazines();
        console.log('Form submitted successfully!');
        this.form.reset();
      })
    }
  }

  getAllMagazines(){
    this.magazineService.getMagazines().subscribe({
      next: (magazines) => {
        this.allMagazines = magazines;
        console.log(this.allMagazines);
      },
      error: (error) => {
        console.log(error);
      }
    })
  }

  onCategoryChange(categoryId: string){
    const selected = this.categories.find(c => c.categoryId === +categoryId);

    if(selected){
      this.form.get('category')?.setValue({
        categoryId: selected.categoryId,
        categoryName: selected.categoryName,
      });
    }
  }

  backLocation(){
    this.location.back();
  }
}

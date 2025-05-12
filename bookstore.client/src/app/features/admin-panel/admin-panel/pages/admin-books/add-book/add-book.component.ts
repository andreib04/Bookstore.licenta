import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {BooksServiceService} from '../../../../../../core/services/books-service.service';
import {Book} from '../../../../../../core/models/book';
import {Location} from '@angular/common';
import {Category} from '../../../../../../core/models/category';
import {CategoriesServiceService} from '../../../../../../core/services/categories-service.service';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrl: './add-book.component.css'
})
export class AddBookComponent implements OnInit{
  form!: FormGroup;
  allBooks: Book[] = [];
  categories: Category[] = [];
  errorMessage: string = '';

  constructor(private booksService: BooksServiceService, private location: Location, private categoriesService: CategoriesServiceService) {
  }

  ngOnInit() {
    this.categoriesService.getCategory().subscribe(data => {
      this.categories = data;
    })

    this.form = new FormGroup({
      title: new FormControl<string>('', [Validators.required]),
      author: new FormControl<string>('', [Validators.required]),
      description: new FormControl<string>('', [Validators.required]),
      price: new FormControl<number>(0, [Validators.required]),
      stock: new FormControl<number>(0, [Validators.required]),
      itemType: new FormControl<string>('Book', [Validators.required]),
      image: new FormControl<string>('', [Validators.required]),
      category: new FormGroup({
        categoryId: new FormControl<number | null>(null, Validators.required),
        categoryName: new FormControl('', Validators.required),
      })
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
        const base64 = (reader.result as string);

        this.form.patchValue({
          image: base64
        });

        this.form.get('image')?.updateValueAndValidity();
      };
      reader.readAsDataURL(file);
    }
  }

  onSubmit() {
    if(this.form.valid) {

      const formValue = this.form.value;

      const book: Book = {
        title: formValue.title,
        author: formValue.author,
        description: formValue.description,
        price: formValue.price,
        stock: formValue.stock,
        itemType: formValue.itemType,
        image: formValue.image,
        category: {
          categoryId: formValue.category.categoryId,
          categoryName: formValue.category.categoryName
        }
      } as Book;

      this.booksService.postBook(book).subscribe(res => {
        console.log('New book: ', res);
        this.getAllBooks();
        console.log('Form submitted successfully!');
        this.form.reset();
      })
    }else{
      console.log(this.form.errors);
      console.log('Form is invalid!');
    }
  }

  getAllBooks(){
    this.booksService.getBooks().subscribe({
      next: (books) => {
        this.allBooks = books;
        console.log(this.allBooks);
      },
      error: (error) => {
        console.log(error);
      }
    })
  }

  backLocation(){
    this.location.back();
  }

}

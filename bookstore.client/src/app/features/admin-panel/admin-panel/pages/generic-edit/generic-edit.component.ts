import {ChangeDetectorRef, Component, model, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {HttpClient} from '@angular/common/http';
import {CategoriesServiceService} from '../../../../../core/services/categories-service.service';
import {Location} from '@angular/common';

@Component({
  selector: 'app-generic-edit',
  templateUrl: './generic-edit.component.html',
  styleUrl: './generic-edit.component.css'
})
export class GenericEditComponent implements OnInit {
  modelType!: string;
  form!: FormGroup;
  baseURL = 'https://localhost:44305/api/';

  categories: any[] = [];

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private http: HttpClient,
    private cd: ChangeDetectorRef,
    private router: Router,
    private categoriesService: CategoriesServiceService,
    private location: Location
  ){
    this.form = this.fb.group({});
  }

  ngOnInit(){
    this.modelType = this.route.snapshot.paramMap.get('model')!;
    const id = this.route.snapshot.paramMap.get('id');
    console.log('Model Type:', this.modelType, 'ID:', id);
    this.loadCategories();
    this.loadModel(id);
  }

  loadModel(id: any){
    const apiURL = `${this.baseURL}${this.modelType}/${id}`;
    console.log('Fetching model', apiURL);

    this.http.get(apiURL).subscribe({
      next: (data:any) => {
        console.log('Model data', data);
        this.buildForm(data);
      },
      error: (error) => {
        console.error('Error fetching model', error);
      }
    })
  }

  buildForm(modelData: any){
    console.log('Building form with: ', modelData);

    let formControls: any = {};

    Object.keys(modelData).forEach((key) => {
      let value = modelData[key];
      let validators = [];

      if(key === 'category' && typeof value === 'object' && value !== null){
        value = value.id ?? null;
      }

      if(key === 'category'){
        return;
      }

      if(['title', 'description', 'itemType'].includes(key)){
        validators.push(Validators.required);
      }

      if(['price', 'stock'].includes(key)){
        validators.push(Validators.min(0));
      }

      formControls[key] = [value, validators];
    });

    this.form = this.fb.group(formControls);
    console.log('Generated form controls: ', this.form.controls);


    this.cd.detectChanges();
  }

  get formFields(){
    return this.form ? Object.keys(this.form.controls) : [];
  }

  loadCategories(){
    this.categoriesService.getCategory().subscribe((res) => {
      this.categories = res;
      console.log(this.categories);
    })
  }

  saveChanges(){
    this.http.put(`${this.baseURL}${this.modelType}/${this.form.value.id}`, this.form.value).subscribe((res) => {
      console.log('Updated successfully', res);

      this.router.navigate(['/admin/books']);
    })
  }

  isNumberField(fieldName: string): boolean{
    return ['price', 'stock'].includes(fieldName);
  }

  backLocation(){
    this.location.back();
  }
}

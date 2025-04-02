import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';
import {HttpClient} from '@angular/common/http';
import {Snapshot} from 'jest-editor-support';

@Component({
  selector: 'app-generic-edit',
  templateUrl: './generic-edit.component.html',
  styleUrl: './generic-edit.component.css'
})
export class GenericEditComponent implements OnInit {
  modelType!: string;
  form!: FormGroup;
  baseURL = 'https://localhost:44305/';

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private http: HttpClient
  ){}

  ngOnInit(){
    this.modelType = this.route.snapshot.paramMap.get('model')!;
    const id = this.route.snapshot.paramMap.get('id');
    this.loadModel(id);
  }

  loadModel(id: any){
    this.http.get(`${this.baseURL}${this.modelType}/${id}`).subscribe((data: any) =>{
      this.buildForm(data);
    })
  }

  buildForm(modelData: any){
    let formControls: any = {};

    Object.keys(modelData).forEach((key) => {
      let validators = [];

      if(key === 'title' || key === 'description' || key === 'itemType'){
        validators.push(Validators.required);
      }
      if(key === 'price' || key ==='stock'){
        validators.push(Validators.min(0));
      }

      formControls[key] = [modelData[key], validators];
    });

    this.form = this.fb.group(formControls);
  }

  saveChanges(){
    this.http.put(`${this.baseURL}${this.modelType}/${this.form.value.id}`, this.form.value).subscribe((res) => {
      console.log('Updated successfully', res);
    })
  }

  isNumberField(fieldName: string): boolean{
    return ['price', 'stock'].includes(fieldName);
  }
}

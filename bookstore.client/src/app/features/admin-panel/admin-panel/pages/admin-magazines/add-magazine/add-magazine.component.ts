import { Component } from '@angular/core';
import {FormGroup} from '@angular/forms';
import {MagazinesServiceService} from '../../../../../../core/services/magazines-service/magazines-service.service';
import {Location} from '@angular/common';

@Component({
  selector: 'app-add-magazine',
  templateUrl: './add-magazine.component.html',
  styleUrl: './add-magazine.component.css'
})
export class AddMagazineComponent {
  base64Image: string = '';
  form: FormGroup;

  constructor(private magazineService: MagazinesServiceService, private location: Location) {
    this.form = new FormGroup({});
  }

  onFileChange(event: Event){
    const file = (event.target as HTMLInputElement)?.files?.[0];
    if(file) {
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

  }

  backLocation(){
    this.location.back();
  }
}

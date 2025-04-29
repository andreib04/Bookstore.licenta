import {Component, OnInit} from '@angular/core';
import {MagazinesServiceService} from '../../../core/services/magazines-service/magazines-service.service';
import {ActivatedRoute} from '@angular/router';
import {Magazine} from '../../../core/models/magazine';

@Component({
  selector: 'app-magazine-page',
  templateUrl: './magazine-page.component.html',
  styleUrl: './magazine-page.component.css'
})
export class MagazinePageComponent implements OnInit{
  magazine!: Magazine;
  fallBackUrl = "https://images.unsplash.com/photo-1596382940920-9f73b2d15901?q=80&w=1274&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";

  constructor(private magazineService: MagazinesServiceService, private route: ActivatedRoute){}

  ngOnInit() {
    this.getAllMagazines();
  }

  getAllMagazines(){
    let id: number = +this.route.snapshot.params['id'];

    this.magazineService.getOneMagazine(id).subscribe({
      next: (magazine) => {
        this.magazine = magazine;
      },
      error: err => {
        console.log(err);
      }
    })
  }
}

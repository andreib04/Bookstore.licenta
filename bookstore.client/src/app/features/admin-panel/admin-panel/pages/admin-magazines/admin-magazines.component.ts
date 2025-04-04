import {Component, OnInit} from '@angular/core';
import {MagazinesServiceService} from '../../../../../core/services/magazines-service/magazines-service.service';
import {Magazine} from '../../../../../core/models/magazine';
import {Router} from '@angular/router';

@Component({
  selector: 'app-admin-magazines',
  templateUrl: './admin-magazines.component.html',
  styleUrl: './admin-magazines.component.css'
})
export class AdminMagazinesComponent implements OnInit{

  allMagazines: Magazine[] = [];
  magazine: Magazine = {} as Magazine;

  constructor(private magazineService: MagazinesServiceService, private router: Router) {
  }

  ngOnInit() {
    this.getAllMagazines();
  }

  getAllMagazines(){
    this.magazineService.getMagazines().subscribe(res => {
      this.allMagazines = res;
    })
  }

  deleteMagazine(){
    this.magazineService.deleteMagazine(this.magazine.id).subscribe(res => {
      console.log('Deleted magazine: ', res);
    })
  }

  goToEditPage(modelType: string, id:number){
    this.router.navigate([`/admin/edit/${modelType}/${id}`]);
  }

}

import {Component, OnInit} from '@angular/core';
import {MagazinesServiceService} from '../../../../../core/services/magazines-service.service';
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
  isLoading = false;

  totalCount = 0;
  currentPage = 1;
  perPage = 5;

  activeSort = {
    sortBy: 'price',
    sortOrder: 'asc'
  }

  constructor(private magazineService: MagazinesServiceService, private router: Router) {
  }

  ngOnInit() {
    this.getAllMagazines();
  }

  /*getAllMagazines(){
    this.magazineService.getMagazines().subscribe(res => {
      this.allMagazines = res;
    })
  }*/

  getAllMagazines(){
    this.isLoading = true;
    const {sortBy, sortOrder} = this.activeSort;

    const request = this.magazineService.getSortedPaginated(this.currentPage, this.perPage, sortBy, sortOrder);
    request.subscribe(res => {
      this.allMagazines = res.items;
      this.totalCount = res.totalCount;
      this.isLoading = false;
    })
  }

  deleteMagazine(id: number){
    this.magazineService.deleteMagazine(id).subscribe(res => {
      this.allMagazines = this.allMagazines.filter(m => m.id !== id);
    })
  }

  goToEditPage(modelType: string, id:number){
    this.router.navigate([`/admin/edit/${modelType}/${id}`]);
  }

  sortMagazines(sortBy: string, sortOrder: string) {
    this.activeSort = { sortBy, sortOrder };
    this.currentPage = 1;
    this.getAllMagazines();
  }

  changePage(page: number){
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    this.getAllMagazines();
  }

  changePerPage(count: number){
    this.perPage = count;
    this.currentPage = 1;
    this.getAllMagazines();
  }

  get totalPages(){
    return Math.ceil(this.totalCount / this.perPage);
  }
}

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AdminPanelComponent} from './admin-panel.component';
import {AdmindashboardComponent} from './pages/admin-dashboard/admindashboard.component';
import {AdminBooksComponent} from './pages/admin-books/admin-books.component';
import {AdminMagazinesComponent} from './pages/admin-magazines/admin-magazines.component';
import {AdminUsersComponent} from './pages/admin-users/admin-users.component';


const routes: Routes = [
  {
    path: '',
    component: AdminPanelComponent,
    children:[
      { path: 'dashboard', component: AdmindashboardComponent},
      {path: 'books', component: AdminBooksComponent},
      {path: 'magazines', component: AdminMagazinesComponent},
      {path: 'users', component: AdminUsersComponent},
      {path: '', redirectTo: 'dashboard', pathMatch: 'full'},
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminPanelRoutingModule { }

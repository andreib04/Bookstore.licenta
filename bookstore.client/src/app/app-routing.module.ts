import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from './features/login-page/login/login.component';
import {HomeComponent} from './features/home-page/home/home.component';
import {AuthGuard} from './core/guards/auth.guard';
import {AdminPanelComponent} from './features/admin-panel/admin-panel/admin-panel.component';
import {RoleGuard} from './core/guards/role-guard.guard';
import {BooksPageComponent} from './features/books-page/books-page/books-page.component';
import {MagazinesPageComponent} from './features/magazines-page/magazines-page/magazines-page.component';
import {CategoriesPageComponent} from './features/categories-page/categories-page/categories-page.component';

const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'home', component: HomeComponent},
  {path: 'books', component: BooksPageComponent},
  {path: 'magazines', component: MagazinesPageComponent},
  {path: 'categories', component: CategoriesPageComponent},

  {
    path: 'admin',
    component: AdminPanelComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {expectedRole: 'Admin'},
  },

  {path: '', redirectTo: 'home', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

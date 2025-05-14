 import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from './features/login-page/login/login.component';
import {HomeComponent} from './features/home-page/home/home.component';
import {AuthGuard} from './core/guards/auth.guard';
import {RoleGuard} from './core/guards/role-guard.guard';
import {BooksPageComponent} from './features/books-page/books-page/books-page.component';
import {MagazinesPageComponent} from './features/magazines-page/magazines-page/magazines-page.component';
import {RegisterComponent} from './features/register-page/register/register.component';
import {CartPageComponent} from './features/cart-page/cart-page/cart-page.component';
 import {BookPageComponent} from './features/books-page/book-page/book-page.component';
 import {MagazinePageComponent} from './features/magazines-page/magazine-page/magazine-page.component';
 import {SearchResultsComponent} from './features/search-results/search-results.component';
 import {CheckoutComponent} from './features/checkout/checkout.component';
 import {UserProfileComponent} from './features/user-profile/user-profile.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },

  { path: 'home', component: HomeComponent },
  { path: 'books', component: BooksPageComponent },
        { path: 'book/:id', component: BookPageComponent },

  {path: 'magazines', component: MagazinesPageComponent },
        { path: 'magazine/:id', component: MagazinePageComponent },

  { path: 'cart', component:CartPageComponent },
  { path: 'checkout', component: CheckoutComponent },
  { path: 'search-results', component: SearchResultsComponent },
  { path: 'profile', component:UserProfileComponent },

  {
    path: 'admin',
    loadChildren: () => import('./features/admin-panel/admin-panel/admin-panel.module').then(m => m.AdminPanelModule),
    canActivate: [AuthGuard, RoleGuard],
    data: {expectedRole: 'Admin'},
  },

  {path: '', redirectTo: 'home', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    scrollPositionRestoration: 'top',
    anchorScrolling: 'enabled',
  })],
  exports: [RouterModule]
})
export class AppRoutingModule { }

import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './features/login-page/login/login.component';
import {ReactiveFormsModule} from '@angular/forms';
import { HomeComponent } from './features/home-page/home/home.component';
import { AdminPanelComponent } from './features/admin-panel/admin-panel/admin-panel.component';
import {AuthInterceptor} from './core/interceptors/auth.interceptor';
import { HeaderComponent } from './shared/components/header/header/header.component';
import { SearchBarComponent } from './shared/components/search-bar/search-bar/search-bar.component';
import { BooksPageComponent } from './features/books-page/books-page/books-page.component';
import { MagazinesPageComponent } from './features/magazines-page/magazines-page/magazines-page.component';
import { CategoriesPageComponent } from './features/categories-page/categories-page/categories-page.component';
import { RegisterComponent } from './features/register-page/register/register.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    AdminPanelComponent,
    HeaderComponent,
    SearchBarComponent,
    BooksPageComponent,
    MagazinesPageComponent,
    CategoriesPageComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, ReactiveFormsModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './features/login-page/login/login.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HomeComponent } from './features/home-page/home/home.component';
import {AuthInterceptor} from './core/interceptors/auth.interceptor';
import { HeaderComponent } from './shared/components/header/header/header.component';
import { SearchBarComponent } from './shared/components/search-bar/search-bar/search-bar.component';
import { BooksPageComponent } from './features/books-page/books-page/books-page.component';
import { MagazinesPageComponent } from './features/magazines-page/magazines-page/magazines-page.component';
import { RegisterComponent } from './features/register-page/register/register.component';
import { CartPageComponent } from './features/cart-page/cart-page/cart-page.component';
import { FooterComponent } from './shared/components/footer/footer/footer.component';
import {CommonModule} from '@angular/common';
import {AuthService} from './core/services/auth.service';
import { BookPageComponent } from './features/books-page/book-page/book-page.component';
import { MagazinePageComponent } from './features/magazines-page/magazine-page/magazine-page.component';
import { SearchResultsComponent } from './features/search-results/search-results.component';
import { CheckoutComponent } from './features/checkout/checkout.component';
import { UserProfileComponent } from './features/user-profile/user-profile.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    HeaderComponent,
    SearchBarComponent,
    BooksPageComponent,
    MagazinesPageComponent,
    RegisterComponent,
    CartPageComponent,
    FooterComponent,
    BookPageComponent,
    MagazinePageComponent,
    SearchResultsComponent,
    CheckoutComponent,
    UserProfileComponent,
  ],
    imports: [
        BrowserModule, HttpClientModule,
        AppRoutingModule, ReactiveFormsModule,
        CommonModule, FormsModule
    ],
  providers: [
    AuthService,
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
  ],
  exports: [

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

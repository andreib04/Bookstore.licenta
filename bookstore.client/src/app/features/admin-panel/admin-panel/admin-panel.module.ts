import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminPanelRoutingModule } from './admin-panel-routing.module';

import {AdminPanelComponent} from './admin-panel.component';
import { AdmindashboardComponent } from './pages/admin-dashboard/admindashboard.component';
import { AdminBooksComponent } from './pages/admin-books/admin-books.component';
import { AdminMagazinesComponent } from './pages/admin-magazines/admin-magazines.component';
import { AdminUsersComponent } from './pages/admin-users/admin-users.component';
import { AddUserPageComponent } from './pages/admin-users/add-user-page/add-user-page.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { AdminCategoriesComponent } from './pages/admin-categories/admin-categories.component';
import { EditUserPageComponent } from './pages/admin-users/edit-user-page/edit-user-page.component';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {AuthInterceptor} from '../../../core/interceptors/auth.interceptor';
import { GenericEditComponent } from './pages/generic-edit/generic-edit.component';
import { AddBookComponent } from './pages/admin-books/add-book/add-book.component';
import { AddMagazineComponent } from './pages/admin-magazines/add-magazine/add-magazine.component';

@NgModule({
  declarations: [
    AdminPanelComponent,
    AdmindashboardComponent,
    AdminBooksComponent,
    AdminMagazinesComponent,
    AdminUsersComponent,
    AddUserPageComponent,
    AdminCategoriesComponent,
    EditUserPageComponent,
    GenericEditComponent,
    AddBookComponent,
    AddMagazineComponent
  ],
    imports: [
        CommonModule,
        AdminPanelRoutingModule,
        FormsModule,
        ReactiveFormsModule,
    ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
  ]
})
export class AdminPanelModule { }

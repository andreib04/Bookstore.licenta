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



@NgModule({
  declarations: [
    AdminPanelComponent,
    AdmindashboardComponent,
    AdminBooksComponent,
    AdminMagazinesComponent,
    AdminUsersComponent,
    AddUserPageComponent,
    AdminCategoriesComponent,
    EditUserPageComponent
  ],
    imports: [
        CommonModule,
        AdminPanelRoutingModule,
        FormsModule,
        ReactiveFormsModule,
    ]
})
export class AdminPanelModule { }

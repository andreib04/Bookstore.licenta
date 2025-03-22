import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminPanelRoutingModule } from './admin-panel-routing.module';

import {AdminPanelComponent} from './admin-panel.component';
import { AdmindashboardComponent } from './pages/admin-dashboard/admindashboard.component';
import { AdminBooksComponent } from './pages/admin-books/admin-books.component';
import { AdminMagazinesComponent } from './pages/admin-magazines/admin-magazines.component';
import { AdminUsersComponent } from './pages/admin-users/admin-users.component';



@NgModule({
  declarations: [
    AdminPanelComponent,
    AdmindashboardComponent,
    AdminBooksComponent,
    AdminMagazinesComponent,
    AdminUsersComponent
  ],
  imports: [
    CommonModule,
    AdminPanelRoutingModule,
  ]
})
export class AdminPanelModule { }

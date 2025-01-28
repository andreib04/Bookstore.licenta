import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from './features/login-page/login/login.component';
import {HomeComponent} from './features/home-page/home/home.component';
import {AuthGuard} from './core/guards/auth.guard';
import {AdminPanelComponent} from './features/admin-panel/admin-panel/admin-panel.component';
import {RoleGuard} from './core/guards/role-guard.guard';

const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'home', component: HomeComponent, canActivate: [AuthGuard]},

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

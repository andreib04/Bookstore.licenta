import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import {AuthService} from '../services/auth.service';
import { map } from 'rxjs/operators';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class RoleGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: any): Observable<boolean> {
    const expectedRole = route.data.expectedRole;
    return this.authService.getCurrentUser().pipe(
      map((user) => {
        if(user && user.role === expectedRole){
          return true;
        }
        else{
          this.router.navigate(['/']);
          return false;
        }
      })
    )
  }
}

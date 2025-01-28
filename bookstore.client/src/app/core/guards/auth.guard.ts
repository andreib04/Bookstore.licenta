import {Injectable} from '@angular/core';
import {CanActivate, Router} from '@angular/router';
import {AuthService} from '../services/auth/auth.service';
import {map} from 'rxjs/operators';
import {Observable} from 'rxjs';
import {log} from '@angular-devkit/build-angular/src/builders/ssr-dev-server';

@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): Observable<boolean> {
    return this.authService.isLoggedIn().pipe(
      map((loggedIn) => {
        if(!loggedIn){
          this.router.navigate(['/login']);
          return false;
        }
        return true;
      })
    )
  }
}

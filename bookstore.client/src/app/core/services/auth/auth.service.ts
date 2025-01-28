import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable, tap} from 'rxjs';
import {User} from '../../models/user';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {Token} from '../../models/token';
import {UserLogin} from '../../models/userLogin';
import {environment} from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private loggedIn = new BehaviorSubject<boolean>(this.hasToken());
  private currentUser = new BehaviorSubject<User | null>(this.getUserFromStorage());
  constructor(private http: HttpClient, private router: Router) { }

  baseURL = "https://localhost:44305/";
  apiPATH = "api/login";

  login(userLogin: UserLogin): Observable<string> {
    return this.http.post(`${this.baseURL}${this.apiPATH}`, userLogin, { responseType: 'text' }).pipe(
      tap((token: string) => {
        localStorage.setItem('access_token', token);
        this.loggedIn.next(true);

        const user = this.decodeToken(token);
        localStorage.setItem('current_user', JSON.stringify(user));
        this.currentUser.next(user);

        this.router.navigate(['/']);
      })
    );
  }

  logout(): void{
    localStorage.removeItem('access_token');
    localStorage.removeItem('current_user');
    this.loggedIn.next(false);
    this.currentUser.next(null);
    this.router.navigate(['/login']);
  }

  isLoggedIn(): Observable<boolean> {
    return this.loggedIn.asObservable();
  }

  getCurrentUser(): Observable<User | null>{
    return this.currentUser.asObservable();
  }

  private hasToken(): boolean{
    return !!localStorage.getItem('access_token');
  }

  private getUserFromStorage(): User | null{
    const userJson = localStorage.getItem('current_user');
    return userJson ? JSON.parse(userJson) : null;
  }

  private decodeToken(token: string): User{
    const payload = JSON.parse(atob(token.split('.')[1]));
    return {
      Id: payload.sub,
      FirstName: payload.given_name,
      LastName: payload.family_name,
      Email: payload.email,
      Password: '',
      Role: payload.role,
    };
  }
}

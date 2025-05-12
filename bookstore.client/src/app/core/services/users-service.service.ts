import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {User} from '../models/user';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UsersServiceService {

  baseURL = "https://localhost:44305/";
  apiPATH = "api/User";
  constructor(private http: HttpClient) { }

   getUsers(): Observable<User[]>{
    return this.http.get<User[]>(`${this.baseURL}${this.apiPATH}`);
  }

  checkEmailExists(email: string): Observable<boolean> {
    return this.http.get<{ exists: boolean }>(`${this.baseURL}${this.apiPATH}/check-email`, {
      params: { email }
    }).pipe(
      map(response => {
        return response.exists;
      })
    );
  }

  getOneUser(id: number): Observable<User> {
    return this.http.get<User>(`${this.baseURL}${this.apiPATH}/${id}`);
  }

  postUser(user: User): Observable<User> {
    return this.http.post<User>(`${this.baseURL}${this.apiPATH}`, user);
  }

  updateUser(id: number, user: User): Observable<User> {
    return this.http.put<User>(`${this.baseURL}${this.apiPATH}/${id}`, user);
  }

  deleteUser(id: number): Observable<User>{
    return this.http.delete<User>(`${this.baseURL}${this.apiPATH}/${id}`);
  }
}

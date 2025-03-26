import { Injectable } from '@angular/core';
import {environment} from '../../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Magazine} from '../../models/magazine';

@Injectable({
  providedIn: 'root'
})
export class MagazinesServiceService {

  baseURL = "https://localhost:44305/";
  apiPATH = "api/Magazine";
  constructor(private http: HttpClient) { }

  getMagazines(): Observable<Magazine[]> {
    return this.http.get<Magazine[]>(`${this.baseURL}${this.apiPATH}`);
  }

  getOneMagazine(id: number): Observable<Magazine> {
    return this.http.get<Magazine>(`${this.baseURL}${this.apiPATH}/${id}`);
  }

  postMagazine(magazine: Magazine): Observable<Magazine> {
    return this.http.post<Magazine>(`${this.baseURL}${this.apiPATH}`, magazine);
  }

  updateMagazine(id: number, magazine: Magazine): Observable<Magazine> {
    return this.http.put<Magazine>(`${this.baseURL}${this.apiPATH}/${id}`, magazine);
  }

  deleteMagazine(id: number): Observable<Magazine>{
    return this.http.delete<Magazine>(`${this.baseURL}${this.apiPATH}/${id}`);
  }
}

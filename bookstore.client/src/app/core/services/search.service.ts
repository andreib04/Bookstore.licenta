import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  baseURL = "https://localhost:44305/";
  apiPATH = "api/search";
  constructor(private http: HttpClient) {}

  search(query: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseURL}${this.apiPATH}/${query}`);
  }
}

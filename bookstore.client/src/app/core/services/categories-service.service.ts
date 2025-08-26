import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Category} from '../models/category';
import {Observable} from 'rxjs';
import {PaginatedCategoryRes} from '../models/paginatedCategoryRes';

@Injectable({
  providedIn: 'root'
})
export class CategoriesServiceService {

  baseURL = "https://localhost:44305/";
  apiPATH = "api/Category";
  constructor(private http: HttpClient) { }

  getCategory(): Observable<Category[]>{
    return this.http.get<Category[]>(`${this.baseURL}${this.apiPATH}`);
  }

  getPaginatedCategory(page: number, perPage: number): Observable<PaginatedCategoryRes>{
    return this.http.get<PaginatedCategoryRes>(`${this.baseURL}${this.apiPATH}/paginated`, {
      params: {
        page,
        perPage
      }
    });
  }

  getOneCategory(id: number): Observable<Category> {
    return this.http.get<Category>(`${this.baseURL}${this.apiPATH}/${id}`);
  }

  postCategory(category: Category): Observable<Category> {
    return this.http.post<Category>(`${this.baseURL}${this.apiPATH}`, category);
  }

  updateCategory(id: number, category: Category): Observable<Category> {
    return this.http.put<Category>(`${this.baseURL}${this.apiPATH}/${id}`, category);
  }

  deleteCategory(id: number): Observable<Category>{
    return this.http.delete<Category>(`${this.baseURL}${this.apiPATH}/${id}`);
  }
}

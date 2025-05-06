import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Book} from '../../models/book';
import {PaginatedBookRes} from '../../models/paginatedBookRes';

@Injectable({
  providedIn: 'root'
})
export class BooksServiceService {

  baseURL = "https://localhost:44305/";
  apiPATH = "api/book";
  constructor(private http: HttpClient) { }

  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(`${this.baseURL}${this.apiPATH}`);
  }

  getOneBook(id: number): Observable<Book> {
    return this.http.get<Book>(`${this.baseURL}${this.apiPATH}/${id}`);
  }

  getLatestBooks(count: number = 3): Observable<Book[]>{
    return this.http.get<Book[]>(`${this.baseURL}${this.apiPATH}/latest/${count}`);
  }

  getBookByCategory(categoryId: number): Observable<Book[]> {
    return this.http.get<Book[]>(`${this.baseURL}${this.apiPATH}/byCategory?categoryId=${categoryId}`);
  }

  getSortedBooks(sortBy: string, sortOrder: string): Observable<Book[]>{
    return this.http.get<Book[]>(`${this.baseURL}${this.apiPATH}/sorted?sortBy=${sortBy}&sortOrder=${sortOrder}`);
  }

  getPaginatedBooks(page: number, perPage: number): Observable<PaginatedBookRes>{
    return this.http.get<PaginatedBookRes>(`${this.baseURL}${this.apiPATH}/paginated?page=${page}&perPage=${perPage}`);
  }

  postBook(book: Book): Observable<Book> {
    return this.http.post<Book>(`${this.baseURL}${this.apiPATH}`, book);
  }

  updateBook(id:number, book: Book): Observable<Book> {
    return this.http.put<Book>(`${this.baseURL}${this.apiPATH}/${id}`, book);
  }

  deleteBook(id: number): Observable<Book> {
    return this.http.delete<Book>(`${this.baseURL}${this.apiPATH}/${id}`);
  }
}

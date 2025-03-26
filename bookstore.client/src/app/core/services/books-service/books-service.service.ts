import { Injectable } from '@angular/core';
import {environment} from '../../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Book} from '../../models/book';

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

import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Order} from '../models/Order';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  baseURL = "https://localhost:44305/";
  apiPATH = "api/orders";
  constructor(private http: HttpClient) {}

  placeOrder(order: Order): Observable<Order>{
    return this.http.post<Order>(`${this.baseURL}${this.apiPATH}`, order);
  }

  checkout(order: Order):Observable<Order>{
    return this.http.post<Order>(`${this.baseURL}${this.apiPATH}/checkout`, order);
  }

  getUserOrders(userId: number): Observable<Order[]>{
    return this.http.get<Order[]>(`${this.baseURL}${this.apiPATH}/user/${userId}`);
  }

  getAllOrders(sortBy = 'createdAt', sortOrder= 'desc', page = 1, perPage = 10): Observable<Order[]>{
    const params = new HttpParams()
      .set('sortBy', sortBy)
      .set('sortOrder', sortOrder)
      .set('page', page.toString())
      .set('perPage', perPage.toString())

    return this.http.get<Order[]>(`${this.baseURL}${this.apiPATH}/admin`, {params});
  }



  deleteOrder(id: number): Observable<Order>{
    return this.http.delete<Order>(`${this.baseURL}${this.apiPATH}/${id}`)
  }
}

import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {AuthService} from './auth.service';
import {CartItem} from '../models/cartItem';
import {BehaviorSubject, Observable, of, tap} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  baseURL = "https://localhost:44305/";
  apiPATH = "api/cart";

  private cartKey = 'localCart';
  private isLoggedIn = false;
  private cartCount = new BehaviorSubject<number>(0);
  cartCount$ = this.cartCount.asObservable();

  constructor(private http: HttpClient, private auth: AuthService) {
    this.auth.isLoggedIn$.subscribe(logged => {
      this.isLoggedIn = logged;
      if(logged) {
        this.syncCartToServer();
      }
      else {
        this.updateCartCount();
      }
    })

    this.updateCartCount();
  }

  private getLocalCart(): CartItem[] {
    return JSON.parse(localStorage.getItem(this.cartKey) || '[]');
  }

  private saveLocalCart(cart: CartItem[]) {
    localStorage.setItem(this.cartKey, JSON.stringify(cart));
    this.updateCartCount();
  }

  private updateCartCount(){
    if(this.isLoggedIn){
      this.http.get<CartItem[]>(`${this.baseURL}${this.apiPATH}`).subscribe(items => {
        const total = items.reduce((sum, item) => sum + item.quantity, 0);
        this.cartCount.next(total);
      });
    }else{
      const cart = this.getLocalCart();
      const total = cart.reduce((sum, item) => sum + item.quantity, 0);
      this.cartCount.next(total);
    }
  }

  addToCart(item: CartItem): Observable<CartItem> {
    if(this.isLoggedIn){
      return this.http.post<CartItem>(`${this.baseURL}${this.apiPATH}/add`, item)
        .pipe(
          tap(() => this.updateCartCount()),
        );
    }else{
      const cart = this.getLocalCart();
      const existing = cart.find(c => c.productId === item.productId && c.productType === item.productType);

      if (existing) {
        existing.quantity += item.quantity;
      }
      else {
        cart.push(item);
      }

      this.saveLocalCart(cart);
      this.updateCartCount();

      return of(item);
    }
  }

  updateQuantity(productId: number, productType: string, quantity: number): Observable<CartItem> {
    if(this.isLoggedIn){
      return this.http.put<CartItem>(
        `${this.baseURL}${this.apiPATH}/update-quantity?productId=${productId}&productType=${productType}&quantity=${quantity}`, {}
      ).pipe(
        tap(() => this.updateCartCount())
      );
    }else{
      const cart = this.getLocalCart();
      const item = cart.find(c => c.productId === productId && c.productType === productType);

      if(item) {
        item.quantity = quantity;
        this.saveLocalCart(cart);
        this.updateCartCount();
      }
      return of(item!);
    }
  }

  removeFromCart(productId: number, productType: string): Observable<CartItem>{
    if(this.isLoggedIn){
      return this.http.delete<CartItem>(
        `${this.baseURL}${this.apiPATH}/remove?productId=${productId}&productType=${productType}`
      ).pipe(
        tap(() => this.updateCartCount())
      );
    }else{
      let cart = this.getLocalCart();
      const removedItem = cart.find(c => c.productId === productId && c.productType === productType);
      cart = cart.filter(c => !(c.productId === productId && c.productType === productType));
      this.saveLocalCart(cart);
      this.updateCartCount();
      return of(removedItem!);
    }
  }

  getCart(): Observable<CartItem[]> {
    if(this.isLoggedIn){
      return this.http.get<CartItem[]>(`${this.baseURL}${this.apiPATH}`)
    }else{
      return of(this.getLocalCart());
    }
  }

  private syncCartToServer() {
    const localCart = this.getLocalCart();
    if (localCart.length > 0) {
      this.http.post(`${this.baseURL}${this.apiPATH}/sync`, localCart).subscribe(() => {
        localStorage.removeItem(this.cartKey);
      });
    }
  }
}

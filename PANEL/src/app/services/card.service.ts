import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';

interface ICard {
  cardNumber: string,
  expiryDate: string,
  cvv: string,
  expenseLimit: number,
  cardHolderType: string
}

const httpOptions = {
  headers : new HttpHeaders({'Content-Type' : 'application/json'})
}

@Injectable({
  providedIn: 'root'
})
export class CardService {
  private AUTH_API = environment.AUTH_API
  
  constructor(private http: HttpClient) { }

  add(params:any){
    return this.http.post(this.AUTH_API + 'Card', params, httpOptions)
  }

  get(){
    return this.http.get(this.AUTH_API + 'Card/ByDealerId', httpOptions)
  }

  getById(id:number){
    return this.http.get(this.AUTH_API + `Card/${id}`, httpOptions)
  }

  update(id:number, params:any){
    return this.http.put(`${this.AUTH_API}Card/${id}`, params, httpOptions)
  }

  delete(id: number){
    return this.http.delete(`${this.AUTH_API}Card/${id}`, httpOptions)
  }
}

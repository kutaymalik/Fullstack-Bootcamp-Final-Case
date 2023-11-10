import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

const httpOptions = {
  headers : new HttpHeaders({'Content-Type' : 'application/json'})
}

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private AUTH_API = environment.AUTH_API

  constructor(private http: HttpClient) { }

  create(params:any){
    return this.http.post(this.AUTH_API + 'Order', params, httpOptions)
  }

  getDealer(){
    return this.http.get(this.AUTH_API + 'Order/ByDealerId', httpOptions)
  }

  getAll(){
    return this.http.get(this.AUTH_API + `Order`, httpOptions)
  }

  getById(id:number){
    return this.http.get(`${this.AUTH_API}Order/${id}`, httpOptions)
  }
 
  confirm(id:number){
    return this.http.put(`${this.AUTH_API}Order/Confirm/${id}`, httpOptions)
  }

  cancel(id:number){
    return this.http.put(`${this.AUTH_API}Order/Cancel/${id}`, httpOptions)
  }

  delete(id:number){
    return this.http.delete(`${this.AUTH_API}Order/${id}`, httpOptions)
  }
}

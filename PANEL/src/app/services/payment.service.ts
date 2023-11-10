import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

const httpOptions = {
  headers : new HttpHeaders({'Content-Type' : 'application/json'})
}

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  private AUTH_API = environment.AUTH_API

  constructor(private http: HttpClient) { }

  create(params:any){
    return this.http.post(this.AUTH_API + 'Payment', params, httpOptions)
  }

  getDealer(){
    return this.http.get(this.AUTH_API + 'Payment/ByDealerId', httpOptions)
  }

  getByOrderId(id:number){
    return this.http.get(`${this.AUTH_API}Payment/ByOrderId/${id}`, httpOptions)
  }

  getAll(){
    return this.http.get(this.AUTH_API + `Payment`, httpOptions)
  }
}

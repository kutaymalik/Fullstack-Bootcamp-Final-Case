import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';

const httpOptions = {
  headers : new HttpHeaders({'Content-Type' : 'application/json'})
}

@Injectable({
  providedIn: 'root'
})
export class AddressService {
  private AUTH_API = environment.AUTH_API

  constructor(private http: HttpClient) { }

  add(params:any){
    return this.http.post(this.AUTH_API + 'Address', params, httpOptions)
  }

  get(){
    return this.http.get(this.AUTH_API + 'Address/ByDealerId', httpOptions)
  }

  getById(id:number){
    return this.http.get(this.AUTH_API + `Address/${id}`, httpOptions)
  }

  update(id:number, params:any){
    return this.http.put(`${this.AUTH_API}Address/${id}`, params, httpOptions)
  }

  delete(id: number){
    return this.http.delete(`${this.AUTH_API}Address/${id}`, httpOptions)
  }
}

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

const httpOptions = {
  headers : new HttpHeaders({'Content-Type' : 'application/json'})
}

@Injectable({
  providedIn: 'root'
})
export class DealerService {
  private AUTH_API = environment.AUTH_API

  constructor(private http: HttpClient) { }

  getAll(){
    return this.http.get(this.AUTH_API + `Dealer`, httpOptions)
  }

  getByDealer(){
    return this.http.get(this.AUTH_API + `Dealer/bySessionId`, httpOptions)
  }

  getByAdmin(id: number){
    return this.http.get(this.AUTH_API + `Dealer/${id}`, httpOptions)
  }

  update(id:number, params:any){
    return this.http.put(`${this.AUTH_API}Dealer/${id}`, params, httpOptions)
  }

  delete(id: number){
    return this.http.delete(`${this.AUTH_API}Dealer/${id}`, httpOptions)
  }
}

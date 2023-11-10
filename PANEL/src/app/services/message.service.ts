import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';

const httpOptions = {
  headers : new HttpHeaders({'Content-Type' : 'application/json'})
}


@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private AUTH_API = environment.AUTH_API

  constructor(private http: HttpClient) { }
  getDealer(id:number){
    return this.http.get(`${this.AUTH_API}Message/ByDealerId?dealerid=${id}`, httpOptions)
  }
}

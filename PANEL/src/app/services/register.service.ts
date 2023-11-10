import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

const httpOptions = {
  headers : new HttpHeaders({'Content-Type' : 'application/json'})
}

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  private AUTH_API = environment.AUTH_API

  constructor(private http: HttpClient) { }

  register(params:any){
    return this.http.post(this.AUTH_API + 'Dealer', params, httpOptions)
  }
}

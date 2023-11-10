import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

const httpOptions = {
  headers : new HttpHeaders({'Content-Type' : 'application/json'})
}

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  private AUTH_API = environment.AUTH_API

  constructor(private http: HttpClient) { }

  create(params:string){
    const body = JSON.stringify({ timePeriod: params }); 
    return this.http.post(this.AUTH_API + 'Report', body, httpOptions)
  }

  createDealer(tp:string, id:number){
    const body = JSON.stringify({ timePeriod: tp , dealerId: id}); 
    return this.http.post(this.AUTH_API + 'Report/ByDealerId', body, httpOptions)
  }
}

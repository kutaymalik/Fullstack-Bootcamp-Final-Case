import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor(private https: HttpClient) { }

  clean(){
    window.sessionStorage.clear();
  }

  public saveUser(tokenResponse:any):void{
    window.sessionStorage.removeItem('auth')
    window.sessionStorage.setItem('auth', JSON.stringify(tokenResponse))
  }

  public getUser():any{
    const response = window.sessionStorage.getItem('auth')
    if(response){
      const tokenResponse = JSON.parse(response);
      return tokenResponse.response;
    }
  }

  public saveTokens(tokendata:any){
    const stringifyResponse: any = JSON.stringify(tokendata)
    if(stringifyResponse){
      const tokenResponse = JSON.parse(stringifyResponse);
      window.sessionStorage.removeItem('role');
      window.sessionStorage.removeItem('token');
      window.sessionStorage.removeItem('refreshToken');
      window.sessionStorage.setItem('token' ,tokenResponse.response.token)
      window.sessionStorage.setItem('refreshToken' ,tokenResponse.response.refreshToken)
      window.sessionStorage.setItem('role' ,tokenResponse.response.role);
      window.sessionStorage.setItem('id' ,tokenResponse.response.dealerId);
    }
  }

  public getToken():string {
    const stringifyResponse:any = window.sessionStorage.getItem('auth');
    if(stringifyResponse){
      const tokenResponse = JSON.parse(stringifyResponse);
      console.log(tokenResponse.response.token);
      return tokenResponse.response.token;
    }
    else return "";
  }

  clearToken(): void {
    window.sessionStorage.removeItem('auth');
  }

}

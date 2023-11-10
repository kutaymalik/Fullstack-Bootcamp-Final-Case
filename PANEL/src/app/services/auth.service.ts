import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StorageService } from './storage.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

const httpOptions = {
  headers : new HttpHeaders({'Content-Type' : 'application/json'})
}

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private AUTH_API = environment.AUTH_API
  constructor(
    private https: HttpClient, 
    private storage: StorageService,
    private toastr: ToastrService,) { }

  register(){
    console.log('register');
  }
  login(email:any, password:any):Observable<any>{
    return this.https.post(this.AUTH_API + 'Token', {
      email,
      password
    }, httpOptions);
  }

  

  generateRefreshToken(refreshToken: any):Observable<any>{
    const params = new HttpParams().set('token', refreshToken);

    const httpsOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
        // İhtiyaca göre diğer başlıkları burada ekleyebilirsiniz
      }),
      params: params // HttpParams ekleyin
    };

    return this.https.get(`${this.AUTH_API}Token/refreshToken`, httpsOptions);
  }

  logOut(){
    this.toastr.info('Successfully logged out.');
    this.storage.clean();
    window.location.href = '/#/login'
  }

  isLoggin(){
    let user = this.storage.getUser();
    if(user){
      return true
    } else{
      return false
    }
  }

  fetchExample():Observable<any>{
    return this.https.get(this.AUTH_API + 'Card/ByDealerId', httpOptions)
  }
}
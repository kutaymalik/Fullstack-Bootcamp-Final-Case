import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpHandler, HttpRequest, HTTP_INTERCEPTORS, HttpEvent } from '@angular/common/http';
import { StorageService } from '../services/storage.service';
import { Observable, BehaviorSubject, throwError, catchError, switchMap } from "rxjs";
import { AuthService } from "../services/auth.service";


@Injectable()
export class TokenInterceptor implements HttpInterceptor{
    constructor(
        private storage: StorageService,
        private auth: AuthService){
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let authreq = req;
        authreq = this.AddTokenHeader(req);
        return next.handle(authreq).pipe(
            catchError(err => {
                
                if(err.status === 401){
                    //this.auth.logOut();
                    return this.HandleRefreshToken(req, next);
                }

                console.log(err);
                return throwError(err);
            })
        )
    }

    HandleRefreshToken(req: HttpRequest<any>, next: HttpHandler){
        return this.auth.generateRefreshToken(window.sessionStorage.getItem('refreshToken')).pipe(
            switchMap((data:any) => {
                this.storage.saveTokens(data)
                return next.handle(this.AddTokenHeader(req))
            }),
            catchError(err => {
                this.auth.logOut();
                return throwError(err);
            })
        )
    }

    AddTokenHeader(request: HttpRequest<any>){
        return request.clone({headers: request.headers.set('Authorization', 'bearer ' + window.sessionStorage.getItem('token'))})
    }
}



export const httpInterceptorProviders = [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true }
]
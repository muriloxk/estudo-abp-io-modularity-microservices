import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth-service.component';
import { Constants } from './Constants';
import { Injectable } from '@angular/core';
import { CoreModule } from './core.module';

@Injectable()
export class AuthInterceptorService implements HttpInterceptor {
    

    constructor(private authService: AuthService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        
        if(req.url.startsWith(Constants.uriWebGateway)) {
          this.authService.getAccessToken().then(token => {
            const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
            const authReq = req.clone({ headers });
            return next.handle(authReq).toPromise();
          })
        }
        
        return next.handle(req);
    }
}
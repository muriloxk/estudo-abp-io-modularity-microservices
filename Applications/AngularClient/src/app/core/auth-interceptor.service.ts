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
            const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`)
                                             .set('__tenant', `de76993a-f5c5-dd94-9a26-39f48895efe5`);
            const authReq = req.clone({ headers });
            return next.handle(authReq).toPromise();
          })
        }

        // if(req.url.startsWith(Constants.uriWebGateway)) {

        //     const authReq = req.clone({ headers });
        //     console.log("__TENANT");
        //     return next.handle(authReq);
        // }
        
        return next.handle(req);
    }
}
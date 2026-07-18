import { Injectable,Provider } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { HttpInterceptor, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../services/authentication.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  
  constructor(
    private authenticationService: AuthenticationService
  ) {}

  // middleware method that attaches the JWT to authenticate requests
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    var isAuthAPI: boolean;

    // jwt not provided to login/register method
    if (request.url.endsWith('login') ||
        request.url.endsWith('register')) {
      isAuthAPI = true;
    }
    else {
      isAuthAPI = false;
    }

    // get token for requests
    if (this.authenticationService.isLoggedIn() && !isAuthAPI) {
      let token = this.authenticationService.getToken();
      const authReq = request.clone({
        // add JWT bearer token to request authorization
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
      return next.handle(authReq);
    }
    return next.handle(request);
  }
}

export const authInterceptorProvider: Provider = 
  { provide: HTTP_INTERCEPTORS, 
    useClass: JwtInterceptor, multi: true
  };
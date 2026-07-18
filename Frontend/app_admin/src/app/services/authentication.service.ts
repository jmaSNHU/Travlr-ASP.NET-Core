import { Inject, Injectable } from '@angular/core';
import { BROWSER_STORAGE } from '../storage';
import { User } from '../models/user';
import { AuthResponse } from '../models/auth-response';
import { TripDataService } from '../services/trip-data.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  // setup storage and service access
  constructor(
    @Inject(BROWSER_STORAGE) private storage: Storage,
    private tripDataService: TripDataService
  ) { }

  // variable to handle Authentication Response
  authResp: AuthResponse = new AuthResponse();

  // get token from local storage provider
  // JWT key will be named 'travlr-token'
  public getToken() : string {
    let out: any;
    out = this.storage.getItem('travlr-token');

    // return empty string if we don't have a token
    if (!out) {
      return '';
    }

    return out;
  }

  // save token to local storage provider
  public saveToken(token: string) : void {
    this.storage.setItem('travlr-token', token);
  }

  // logout of application and remove JWT from local storage
  public logout() : void {
    this.storage.removeItem('travlr-token');
  }

  // boolean to determine if user is still logged in
  // and token is still valid
  // will need to reauthenticate if current token is expired
  public isLoggedIn() : boolean {
    const token: string = this.getToken();
    if (token) {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload.exp > (Date.now() / 1000);
    } else {
      return false;
    }
  }

  public isRegistered() : boolean {
    return this.authResp.isSuccess;
  }

  // retrieve current user
  // should only be called after calling isLoggedIn()
  public getCurrentUser(): User {
    const token: string = this.getToken();
    const { email, name } = JSON.parse(atob(token.split('.')[1]));
    return { email, name } as User;
  }

  // login method that uses the login method in TripDateService
  // because that method returns an observable, we subscribe to the
  // result and only process when the Observable condition is satisfied
  public login(user: User, passwd: string) : void {
    this.tripDataService.login(user, passwd)
      .subscribe({
        next: (value: any) => {
          if (value) {
            console.log(value);
            this.authResp = value;
            this.saveToken(this.authResp.token);
          }
        },
        error: (error: any) => {
          console.log('Error: ' + error);
        }
      });
  }

  // register method that uses TripDataServicer's register method
  // because that method returns an observable, we subscribe to the
  // result and only process when the Observable condition is satisfied
  public register(user: User, passwd: string) : void {
    this.tripDataService.register(user, passwd)
      .subscribe({
        next: (value: any) => {
          if (value) {
            console.log(value);
            this.authResp = value;
            this.saveToken(this.authResp.token);
          }
        },
        error: (error: any) => {
          console.log('Error: ' + error);
        }
      });
  }
}

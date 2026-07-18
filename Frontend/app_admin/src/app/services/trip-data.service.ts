import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs'; // handles async calls
import { Trip } from '../models/trip'; // Trip data interface for API
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user';
import { AuthResponse } from '../models/auth-response';
import { BROWSER_STORAGE } from '../storage';


@Injectable({
  providedIn: 'root'
})
export class TripDataService {

  constructor(
    private http: HttpClient,
    @Inject(BROWSER_STORAGE) private storage: Storage
  ) { }
  baseUrl = 'https://localhost:7277/api/auth';
  url = 'https://localhost:7277/api/trips';

  // call to /login endpoint, returns JWT
  login(user: User, passwd: string) : Observable<AuthResponse> {
    // console.log('Inside TripDataService::login');
    return this.handleAuthAPICall('login', user, passwd);
  }

  // call to /register endpoint, creates user and returns JWT
  register(user: User, passwd: string) : Observable<AuthResponse> {
    // console.log('Inside TripDateService::register');
    return this.handleAuthAPICall('register', user, passwd);
  }

  // helper method to handle login and register methods
  handleAuthAPICall(endpoint: string, user: User, passwd: string) :
  Observable<AuthResponse> {
    // console.log('Inside TripDateService::handleAuthAPICall');
    let formData = {
      name: user.name,
      email: user.email,
      password: passwd
    };

    return this.http.post<AuthResponse>(this.baseUrl + '/' + endpoint, formData);
  }

  getTrips() : Observable<Trip[]> {
    // debug
    // console.log('Inside TripDataService::getTrips');
    
    return this.http.get<Trip[]>(this.url);
  }

  addTrip(formData: Trip) : Observable<Trip> {
    // debug
    // console.log('Inside TripDataService::addTrip');

    return this.http.post<Trip>(this.url, formData);
  }

  getTrip(tripCode: string) : Observable<Trip[]> {
    // debug
    // console.log('Inside TripDataService::getTrip');

    return this.http.get<Trip[]>(this.url + '/' + tripCode);
  }

  updateTrip(formData: Trip) : Observable<Trip> {
    // debug
    // console.log('Inside TripDateService::updateTrip');

    return this.http.put<Trip>(this.url + '/' + formData.code, formData);
  }
}

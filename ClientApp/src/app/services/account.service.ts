import { Injectable } from '@angular/core';
import {BehaviorSubject} from "rxjs/BehaviorSubject";
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Injectable(
//  providedIn: 'root'
)
export class AccountService {



  // Observable navItem source
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  // Observable navItem stream
  authNavStatus$ = this._authNavStatusSource.asObservable();

  private loggedIn = false;

  constructor(private http: HttpClient) {
    this.loggedIn = !!localStorage.getItem('auth_token');
    // ?? not sure if this the best way to broadcast the status but seems to resolve issue on page refresh where auth status is lost in
    // header component resulting in authed user nav links disappearing despite the fact user is still logged in
    this._authNavStatusSource.next(this.loggedIn);
  }

  login(userName, password) {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    headers.append('Content-Type', 'application/x-www-form-urlencoded');

    console.log(localStorage);

    return this.http
      .post(
        'api/accounts/login',
        {email: userName, password: password},
        { headers , responseType: "text"})
      .map(res =>{
        localStorage.setItem('auth_token', res);
        this.loggedIn = true;
        this._authNavStatusSource.next(true);
        return true;
      });
  }
  logout() {
    localStorage.removeItem('auth_token');
    this.loggedIn = false;
    this._authNavStatusSource.next(false);
  }

  isLoggedIn() {
    return this.loggedIn;
  }

}

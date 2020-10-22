import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs/Observable";
import {User} from "../models/user.model";

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'Bearer ' + localStorage.getItem("auth_token")
  })
};

@Injectable()
export class UserService {
  userEndpoint = "api/user";

  constructor(private http: HttpClient) { }

  getUserWithDiagnoses():Observable<User>{
    return this.http.get<User>(this.userEndpoint + "/diagnoses");
  }


  getUserwithSymptoms():Observable<User>{
    let test = localStorage.getItem("auth_token");
    return this.http.get<User>(this.userEndpoint + "/symptoms", httpOptions);
  }
}

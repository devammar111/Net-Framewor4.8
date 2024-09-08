import { Injectable } from '@angular/core';
import { Headers, Response, HttpModule } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map'
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ChangePasswordService {
  private token: string;
  private redirectUrl: string = '/';
  private loginUrl: string = '/signin';
  private isloggedIn: boolean = false;
  //private loggedInUser: User;

  constructor(private http: HttpClient, private router: Router) {
    // set token if saved in local storage
    var currentUser = JSON.parse(localStorage.getItem('currentUser'));
    this.token = currentUser && currentUser.token;
  }

  confirmPasswordReset(model: any): Observable<boolean> {
    let url = "Account/ResetPassword";
    return this.http
      .post(url, model)
      .map((response: any) => {
        if (response === true) {
          return response;
        } else {
          return false;
        }
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
}

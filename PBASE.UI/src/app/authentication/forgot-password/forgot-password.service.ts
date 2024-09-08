import { Injectable } from '@angular/core';
import { Headers, Response, HttpModule } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map'
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ForgotPasswordService {
  private token: string;
  private redirectUrl: string = '/';

  constructor(private http: HttpClient, private router: Router) {
    // set token if saved in local storage
    var currentUser = JSON.parse(localStorage.getItem('currentUser'));
    this.token = currentUser && currentUser.token;
  }

  resetPassword(emailAddress: string) {
    let url = 'account/forgotpassword';
    var body = {
      Email: emailAddress
    };
    return this.http
      .post(url, body)
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

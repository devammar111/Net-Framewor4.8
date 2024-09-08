import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs/Rx';
import { Headers, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/map';
import { Router } from '@angular/router';
import { Login } from './login';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class LoginService {
  private token: string;
  private resResult: any;
  private isloggedIn: boolean = false;
  options: RequestOptions;
  headers: Headers;
  constructor(private router: Router, private http: HttpClient, private toastr: ToastrService,) {
    // set token if saved in local storage
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    this.token = currentUser && currentUser.token;
  }

  login(login: Login): Observable<boolean> {
    let that = this;
    let url = "token";
    let body = "username=" + login.UserName + "&password=" + encodeURIComponent(login.Password) + "&grant_type=password" + "&Scope=" + login.IPAddress;
    return this.http
      .post(url, body)
      .map((response: any) => {
        if (response.closeAlertMessage) {
          that.toastr.warning(response.closeAlertMessage, "", {
            timeOut: 0,
            extendedTimeOut: 0,
            enableHtml: true,
          });
          return false;
        }
        let token = response && response.access_token;
        if (token) {
          // set token property
          this.token = token;
          this.isloggedIn = true;
          // store username and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser', JSON.stringify({
            username: response.userName,
            token: token,
            tokenLife: response.expires_in / 3600,
            tokenTime: new Date().getTime(),
            isReadonly: response.isReadonly
          }));
          // return true to indicate successful login
          return true;
        } else {
          // return false to indicate failed login
          return false;
        }
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }

  logout(): void {
    this.signOut()
      .subscribe((res) => {
        this.reset();
        this.router.navigate(['authentication/login']);
      });
  }

  signOut() {
    return this.http.get("Account/Signout")
      .map((res) => res);
  }

  private _listners = new Subject<any>();

  listen(): Observable<any> {
    return this._listners.asObservable();
  }

  filter(filterBy: string) {
    this._listners.next(filterBy);
  }

  reset(): void {
    this.token = null;
    this.isloggedIn = false;
    localStorage.removeItem('currentUser');
  }
}

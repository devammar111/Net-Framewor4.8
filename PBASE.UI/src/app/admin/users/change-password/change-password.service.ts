import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
@Injectable()
export class ChangePasswordService {
    apiUrl: string = "Account/";// Web API URL
  constructor(private http: HttpClient) {
    }

    //update Password
    updatePassword(password: any): Observable<any> {
    return this.http
      .put(this.apiUrl + "changepassword/" + password.Id, password)
      .map((res) => res)
      .catch(this.handleError);
  }
  private handleError(error: any) {
    return Observable.throw(error || 'Server error');
  }

    //update Profile
    updateProfile(user: any): Observable<any> {
      return this.http
        .put("Account/profile/" + user.Id, user)
        .map((res) => res)
        .catch(this.handleError);
    }
} 

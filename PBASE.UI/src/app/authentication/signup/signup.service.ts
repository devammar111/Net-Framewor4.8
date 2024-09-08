import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import { SignUp } from './signup';

@Injectable()
export class SignupService {
  constructor(private http: HttpClient) {
  }
  
  addUser(user: SignUp): Observable<any> {
    return this.http
      .post('Account/Register/', user)
      .flatMap((res: any) => {
        return Observable.of(res);
      });
  }
} 

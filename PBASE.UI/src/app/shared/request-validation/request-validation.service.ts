import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';

@Injectable()
export class RequestValidationService {
  apiUrl: string = "requestvalidation";// Web API URL  
  constructor(private http: HttpClient) {
  }

  getToken(): Observable<any> {
    return this.http
      .get(this.apiUrl)
      .flatMap((res: any) => {
        return Observable.of(res);
      })
  }

  private handleError(error: any) {
    console.error(error);
    return Observable.throw(error.json().error || 'Server error');
  }
} 

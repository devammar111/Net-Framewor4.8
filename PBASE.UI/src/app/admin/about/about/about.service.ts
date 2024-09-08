import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';


@Injectable({
  providedIn: 'root',
})
export class AboutService {
  constructor(private http: HttpClient) {
  }

  getData(): Observable<any> {
    return this
      .http
      .get('lookupForm/userIdle')
      .flatMap((res: any) => {
        return Observable.of(res);
      })
  }
}

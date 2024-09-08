import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
@Injectable()
export class LookupFormService {
  apiUrl: string = "LookupForm/";// Web API URL
  constructor(private http: HttpClient) {
  }

  //get Lookup
  getLookup(id: number, lookupTypeId: number): Observable<any> {
    return this.http.get(this.apiUrl + id + "/" + lookupTypeId)
      .map((res) => res)
      .catch(this.handleError);
  }

  //save Lookup
  updateLookup(lookup: any): Observable<any> {
    let bodyString = JSON.stringify(lookup); // Stringify payload
    return this.http.put(this.apiUrl + lookup['LookupId'], bodyString)
      .map((res) => res)
      .catch(this.handleError);
  }

  addLookup(lookup: any): Observable<any> {
    let bodyString = JSON.stringify(lookup); // Stringify payload
    return this.http.post(this.apiUrl, bodyString)
      .map((res) => res)
      .catch(this.handleError);
  }

  //delete Lookup
  deleteLookup(lookupId: number): Observable<any> {
    return this.http.delete(this.apiUrl + lookupId)
      .map((res) => res)
      .catch(this.handleError);
  }

  private handleError(error: any) {
    console.error(error);
    return Observable.throw(error.json().error || 'Server error');
  }
} 

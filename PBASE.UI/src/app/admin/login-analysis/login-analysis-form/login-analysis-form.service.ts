import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
@Injectable()
export class LoginAnalysisFormService {
  constructor(private http: HttpClient) {
    }
  //get Data by id
  getData(id: number, startDate: string, endDate: string): Observable<any> {
    return this
      .http
      .get('loginAnalysis/' + id + '/' + startDate + '/' + endDate)
      .flatMap((res: any) => {
        return Observable.of(res);
      })
  }
  //add user
  addLoginAnalysis(data: any): Observable<any> {
    return this
      .http
      .post('loginAnalysis/', data)
      .flatMap((res: any) => {
        return Observable.of(res);
      });
    }

    updateLoginAnalysis(loginAnalysis: any): Observable<any> {
        let bodyString = JSON.stringify(loginAnalysis); // Stringify payload
      return this.http.put("loginAnalysis/" + loginAnalysis['Id'], bodyString)
            .map((res) => res)
            .catch(this.handleError);
    }

    //Delete Policy
    deleteLoginAnalysis(client: any): Observable<any> {
        let bodyString = JSON.stringify(client); // Stringify payload
      return this.http.delete("loginAnalysis" + client['PreEmploymentId'])
            .map((res) => res)
            .catch(this.handleError);
    }

    private handleError(error: any) {
        console.error(error);
        return Observable.throw(error.error || 'Server error');
    } 
} 

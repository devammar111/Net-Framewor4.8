import { Component, OnInit} from '@angular/core';
import { HttpClient, HttpBackend } from '@angular/common/http';

@Component({
    selector: 'app-ip-info',
  templateUrl: './ip-info.component.html',
})
export class IpInfoComponent implements OnInit {
  data: string = '';
  private httpClient: HttpClient;

  constructor(handler: HttpBackend) {
    this.httpClient = new HttpClient(handler);
  }

  ngOnInit() {
    let that = this;
    that.httpClient
      .get('http://api.ipify.org/?format=json')
      .subscribe((res: any) => {
        that.data = res;
      });
  }
}

import { Component, OnInit, ViewChild } from '@angular/core';
import { HttpService } from 'src/app/shared/http.service';
import { AboutService } from './about.service';
import { formatDate } from '@angular/common';
import { BreadcrumbService } from 'src/app/shared/breadcrumb/breadcrumb.service';
import { Breadcrumb } from 'src/app/shared/breadcrumb/breadcrumb.model';


@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {
  yearNow = '';
  appInfo: any = {};
  data: any = {};
  constructor(
    private httpService: HttpService,
    private breadcrumbService: BreadcrumbService
  ) {
    this.yearNow = formatDate(new Date(), 'yyyy', 'en-US');}

  ngOnInit() {
    let that = this;
    that.appInfo = that.httpService.appInfo();
    that.setBreadcrumbs();
  }
  setBreadcrumbs() {
    let that = this;
    let breadcrumbs: Breadcrumb[] = [];
    breadcrumbs.push({ label: 'About', url: '/about', params: {} });

    that.breadcrumbService.set(breadcrumbs);
  }
}

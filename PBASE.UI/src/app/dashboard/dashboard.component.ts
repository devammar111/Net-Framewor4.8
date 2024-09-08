import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import * as HighCharts from 'highcharts';
//service
import { BreadcrumbService } from '../shared/breadcrumb/breadcrumb.service';
import { DashboardService } from './dashboard.service';
//custom
import { Breadcrumb } from '../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus } from '../shared/all-routes';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit {
  dashboardSection: Menu = pBMenus.find(x => x.id == "Dashboard");
  @ViewChild('chartContainer') ChartContainer: ElementRef;
  data: any = {};
  constructor(
    private breadcrumbService: BreadcrumbService,
    private dashboardService: DashboardService) { }

  ngOnInit() {
    let that = this;
    that.setBreadcrumbs();
  }

  setBreadcrumbs() {
    let that = this;
    let breadcrumbs: Breadcrumb[] = [];
    breadcrumbs.push({ label: that.dashboardSection.grid.routeInfo.label, url: '', params: {} });
    that.breadcrumbService.set(breadcrumbs);
  }
}

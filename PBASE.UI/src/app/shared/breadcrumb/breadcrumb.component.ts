import { Component, Input, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { BreadcrumbService } from './breadcrumb.service';
import { Breadcrumb } from './breadcrumb.model';

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html'
})
export class BreadcrumbComponent implements OnInit {
  breadcrumbs: Breadcrumb[];
  constructor(
    private breadcrumbService: BreadcrumbService,
    private titleService: Title) {
    this.breadcrumbService.breadcrumbItem
      .subscribe((data: Breadcrumb[]) => {
        if (data.length > 0) {
          this.breadcrumbs = data;
          let crumb: Breadcrumb = this.breadcrumbs[data.length - 1];
          this.titleService.setTitle(crumb.label);
        }
      });
  }

  ngOnInit() {
    this.breadcrumbs = [];
    this.titleService.setTitle('Please wait...');
  }
}

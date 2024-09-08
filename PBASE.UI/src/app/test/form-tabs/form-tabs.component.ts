import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Tab } from '../../shared/tabs/tab';
import { HttpService } from 'src/app/shared/http.service';


@Component({
  selector: 'app-form-tabs',
  templateUrl: './form-tabs.component.html',
  styles: []
})
export class FormTabsComponent implements OnInit {
    formTabs: Tab[] = [];
    queryParams: any = {};

  constructor(
    private route: ActivatedRoute,
    private httpService: HttpService  ) { }

    ngOnInit() {
        let that = this;
      let tab1Info = that.httpService.getAppInfo(-9961);
      let tab2Info = that.httpService.getAppInfo(-9960);
      let tab3Info = that.httpService.getAppInfo(-9959);
      if (tab1Info.show) {
        that.formTabs.push(new Tab(1, "Test Tab 1", ['/tests', 'tab', 'edit-test'], that.route.snapshot.queryParams));
      }
      if (tab2Info.show) {
        that.formTabs.push(new Tab(1, "Test Tab 2", ['/tests', 'tab', 'testssub'], that.route.snapshot.queryParams));
      }
      if (tab3Info.show) {
        that.formTabs.push(new Tab(1, "Test Tab 3", ['/tests', 'tab', 'testnote'], that.route.snapshot.queryParams));
      }
    }
}

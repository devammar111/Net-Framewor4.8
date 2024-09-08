import { Component, OnInit } from "@angular/core"
import { Tab } from '../../../shared/tabs/tab';
import { HttpService } from "src/app/shared/http.service";

@Component({
  selector: 'app-system-settings-tabs',
  templateUrl: './system-settings-tabs.component.html',
  styleUrls: ['./system-settings-tabs.component.scss']
})
export class SystemSettingsTabsComponent implements OnInit {
  systemSettingsTabs: Tab[] = [];
  systemSetting_ObjectTabInfo: any = {};
  dashboardTab: any = {};

  constructor(
    private httpService: HttpService
  ) {
  }

  iniTabs() {
    let that = this;
    that.getPermissions();
    var isDashboardObjectsShow = (that.dashboardTab.show == true) || (that.dashboardTab.isReadonly == true);
    var isObjectsShow = (that.systemSetting_ObjectTabInfo.show == true) || (that.systemSetting_ObjectTabInfo.isReadonly == true);
    that.systemSettingsTabs =
      [
      new Tab(1, "Objects", ['/', 'system-settings', 'options'], {}, isObjectsShow),
        new Tab(2, "Dashboard Objects", ['/', 'system-settings', 'dashboard'], {}, isDashboardObjectsShow),
        new Tab(3, "Groups", ['/', 'system-settings', 'groups'])
      ];
  }

  getPermissions() {
    let that = this;
    that.dashboardTab = that.httpService.getAppInfo(-9947);
    that.systemSetting_ObjectTabInfo = that.httpService.getAppInfo(-9980);
  }

  ngOnInit() {
    let that = this;
    that.iniTabs();
  }
}



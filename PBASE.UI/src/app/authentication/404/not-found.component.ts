import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpService } from '../../shared/http.service';
import { ROUTES } from "../../shared/sidebar/menu-items";

@Component({
  selector: 'app-not-found',
  templateUrl: './not-found.component.html'
})

export class NotfoundComponent {
  ordering: any = [];
  menu: any = [];
  permissions: any;
  constructor(private router: Router,
    private httpService: HttpService) {
  }

  ngOnInit() {
    let that = this;
    var count = 0;
    let menus = that.httpService.menus();
    let filteredMenus = menus.filter(x => x.AccessTypeId != null);
    that.permissions = filteredMenus.map(x => x.Id);
    for (var i = 0; i < ROUTES.length; i++) {
      if (ROUTES[i].path == '') {
        for (var j = 0; j < ROUTES[i].permissions.length; j++) {
          that.ordering[count] = ROUTES[i].permissions[j];
          that.menu.push(ROUTES[i].submenu[j]);
          count++;
        }
      }
      else {
        that.ordering[count] = ROUTES[i].permissions[0];
        that.menu.push(ROUTES[i]);
        count++;
      }
    }
  }

  back() {
    let that = this;
    let activemenu: any;
    for (var i = 0; i < that.ordering.length; i++) {
      let activePermission = that.permissions.find(x => x == that.ordering[i]);
      if (activePermission != undefined) {
        activemenu = that.menu.find(m => m.permissions[0] == activePermission);
        if (activemenu) {
          break;
        }
      }
    }
    if (activemenu) {
      that.router.navigate([activemenu.path]);
    }
    else {
      that.router.navigate(['/profile']);
    }
  }
}

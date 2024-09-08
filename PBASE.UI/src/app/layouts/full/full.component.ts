import { Component, OnInit, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { formatDate } from '@angular/common';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { LoginService } from '../../authentication/login/login.service';
import { UserIdleService } from 'angular-user-idle';
import { FullService } from './full.service';
import { HttpService } from '../../shared/http.service';
import { ROUTES } from "../../shared/sidebar/menu-items";
declare var $: any;

@Component({
  selector: 'app-full-layout',
  templateUrl: './full.component.html',
  styleUrls: ['./full.component.scss']
})
export class FullComponent implements OnInit {
  yearNow = '';
  cancelClicked: boolean;
  confirmClicked: boolean;
  public config: PerfectScrollbarConfigInterface = {};
  public isCollapsed = false;
  public innerWidth: any;
  public defaultSidebar: any;
  public showSettings = false;
  public showMobileMenu = false;
  public expandLogo = false;
  appInfo: any = {};
  userName: string = '';
  userProfileImageHandler: string = '';  
  options = {
    theme: 'light',
    dir: 'ltr',
    layout: 'vertical',
    sidebartype: 'full',
    sidebarpos: 'fixed',
    headerpos: 'fixed',
    boxed: 'full',
    navbarbg: 'skin6',
    sidebarbg: 'skin1',
    logobg: 'skin1'
  };
  user: any = {};
  ordering: any = [];
  menu: any = [];
  permissions: any;
  subscriptions: any = [];
  constructor(public router: Router,
    private loginService: LoginService,
    private userIdle: UserIdleService,
    private httpService: HttpService) {
    this.yearNow = formatDate(new Date(), 'yyyy', 'en-US');
  }

  Logo() {
    this.expandLogo = !this.expandLogo;
  }

  ngOnInit() {
    let that = this;

    const match = navigator.userAgent.search(/(?:Edge|MSIE|Trident\/.*; rv:)/);
    if (match !== -1) {
      //window.alert("IE");
      console.log("IE");

      var head: HTMLHeadElement = document.getElementsByTagName('head')[0];
      var link: HTMLLinkElement = document.createElement('link');
      link.rel = 'shortcut icon';
      link.href = 'favicon.ico';
      head.appendChild(link);
    }
   
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
    that.checkTokenLife();
    that.defaultSidebar = that.options.sidebartype;
    that.handleSidebar();
    that.userIdle.startWatching();
    that.subscriptions.push((this.userIdle.onTimerStart().subscribe()));
    that.subscriptions.push((this.userIdle.onTimeout().subscribe(() => {
      that.logout();
    })));
    that.userProfileImageHandler = that.httpService.userProfileImageHandler();
    that.userName = that.httpService.userName();
    that.appInfo = that.httpService.appInfo();
    if (that.router.url === '/') {
      that.goTohome();
    }
  }

  logout() {
    this.loginService.logout();
    this.userIdle.resetTimer();
    this.userIdle.stopTimer();
    this.userIdle.stopWatching();
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.handleSidebar();
  }

  handleSidebar() {
    this.innerWidth = window.innerWidth;
    switch (this.defaultSidebar) {
      case 'full':
      case 'iconbar':
        if (this.innerWidth < 1170) {
          this.options.sidebartype = 'mini-sidebar';
        } else {
          this.options.sidebartype = this.defaultSidebar;
        }
        break;

      case 'overlay':
        if (this.innerWidth < 767) {
          this.options.sidebartype = 'mini-sidebar';
        } else {
          this.options.sidebartype = this.defaultSidebar;
        }
        break;

      default:
    }
  }

  toggleSidebarType() {
    switch (this.options.sidebartype) {
      case 'full':
      case 'iconbar':
        this.options.sidebartype = 'mini-sidebar';
        break;

      case 'overlay':
        this.showMobileMenu = !this.showMobileMenu;
        break;

      case 'mini-sidebar':
        if (this.defaultSidebar === 'mini-sidebar') {
          this.options.sidebartype = 'full';
        } else {
          this.options.sidebartype = this.defaultSidebar;
        }
        break;

      default:
    }
  }

  checkTokenLife() {
    let that = this;
    that.user = JSON.parse(localStorage.getItem('currentUser'));
    if (that.user) {
      let tokenTime = ((new Date().getTime() - that.user.tokenTime) / 1000) / 3600;
      if (tokenTime > that.user.tokenLife) {
        that.loginService.logout();
      }
    }
    else {
      that.loginService.logout();
    }
  }

  goTohome(){
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

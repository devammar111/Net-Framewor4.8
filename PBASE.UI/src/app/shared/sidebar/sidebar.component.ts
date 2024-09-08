import { Component, AfterViewInit, OnInit } from '@angular/core';
import { ROUTES } from './menu-items';
import { RouteInfo } from './sidebar.metadata';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LoginService } from '../../authentication/login/login.service';
import { formatDate } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { NgxPermissionsService } from 'ngx-permissions';
import { HttpService } from '../../shared/http.service';

declare var $: any;

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html'
})
export class SidebarComponent implements OnInit {
  yearNow = '';
  showMenu = '';
  showSubMenu = '';
  submitted = false;
  public url: string = "";
  public sidebarnavItems: any[];
  // this is for the open close
  addExpandClass(element: any) {
    if (element === this.showMenu) {
      this.showMenu = '0';
    } else {
      this.showMenu = element;
    }
  }
  addActiveClass(element: any) {
    if (element === this.showSubMenu) {
      this.showSubMenu = '0';
    } else {
      this.showSubMenu = element;
    }
  }

  constructor(
    private modalService: NgbModal,
    private router: Router,
    private loginService: LoginService,
    private toastr: ToastrService,
    private httpService: HttpService,
    private ngxPermissionsService: NgxPermissionsService,
    private route: ActivatedRoute) {
    this.yearNow = formatDate(new Date(), 'yyyy', 'en-US');
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    this.submitted = currentUser && (currentUser.applicationSubmitted === 'True');
  }
  // End open close
  ngOnInit() {
    this.sidebarnavItems = ROUTES.filter(sidebarnavItem => sidebarnavItem.submitted == this.submitted);
    this.url = this.router.url;
    let menus = ROUTES.filter(sidebarnavItem => sidebarnavItem.path === '');
    menus = menus.filter(sidebarnavItem => sidebarnavItem.submenu.find(submenu => this.url.indexOf(submenu.path) === 0));
    if (menus.length > 0) {
      this.addExpandClass(menus[menus.length - 1].title);
    }
  }

  logout(): void {
      this.loginService.logout();
  }


}

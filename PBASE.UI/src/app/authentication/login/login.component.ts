import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginService } from './login.service';
import { ToastrService } from 'ngx-toastr';
import { CookieService } from 'ngx-cookie-service';
import { Title } from '@angular/platform-browser';
import { NgxPermissionsService } from 'ngx-permissions';
import { SnotifyService, SnotifyPosition } from 'ng-snotify';
import { HttpService } from '../../shared/http.service';
import { ROUTES } from "../../shared/sidebar/menu-items";
import { Login } from './login';
import { HttpClient, HttpBackend } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  model: Login = new Login();
  returnUrl: string;
  IpAddress: string;
  isLogin: boolean | number = false;
  isLoading: boolean = false;
  error: any;
  ordering: any = [];
  menu: any = [];
  private httpClient: HttpClient;
  constructor(private fb: FormBuilder,
    private router: Router,
    private loginService: LoginService,
    private cookieService: CookieService,
    private toastr: ToastrService,
    private titleService: Title,
    private ngxPermissionsService: NgxPermissionsService,
    private route: ActivatedRoute,
    private httpService: HttpService,
    handler: HttpBackend,
    private snotifyService: SnotifyService) {
    this.httpClient = new HttpClient(handler);
  }

  ngOnInit() {
    let that = this;
    that.setTitle();
    that.getIP();
    that.returnUrl = that.route.snapshot.queryParams['returnUrl'] || '/';
    let count = 0;
    for (let i = 0; i < ROUTES.length; i++) {
      if (ROUTES[i].path == '') {
        for (let j = 0; j < ROUTES[i].permissions.length; j++) {
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
    that.form = that.fb.group({
      uname: [null, Validators.compose([Validators.required])],
      password: [null, Validators.compose([Validators.required])]
    });
  }

  getIP() {
    let that = this;
    that.httpClient
      .get('http://api.ipify.org/?format=json')
      .subscribe((res: any) => {
        if (res) {
          that.IpAddress = res.ip;
        }
      });
  }

  onSubmit() {
    let that = this;
    that.isLoading = true;
    that.model.UserName = that.form.controls['uname'].value;
    that.model.Password = that.form.controls['password'].value;
    that.model.IPAddress = that.IpAddress;
    that.loginService
      .login(that.model)
      .subscribe((data: boolean) => {
        if (data) {
          that.httpService
            .get('user/')
            .subscribe((response: any) => {
              //Display Alerts
                setTimeout(function () {
                    that.showAlerts(response.alerts);
                }, 1200);
              that.updateUser(response);
              let menus = response.Menus.filter(x => x.AccessTypeId != null);
              let permissions: any[] = menus.map(x => x.Id);
              that.ngxPermissionsService.loadPermissions(permissions);
              let activemenu: any;
              for (var i = 0; i < that.ordering.length; i++) {
                let activePermission = permissions.find(x => x == that.ordering[i]);
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
            },
            err => {
              that.isLogin = false;
              that.isLoading = false;
            });
        }
        else {
          that.isLogin = false;
          that.isLoading = false;
        }
      }, (err: any) => {
        that.isLogin = false;
        that.isLoading = false;
        if (err.status == 400 && err.error) {
          that.toastr.error(err.error.error_description ? err.error.error_description : 'Server Error!');
        }
      });
  }

  updateUser(data: any) {
    let currentUser: any = JSON.parse(localStorage.getItem('currentUser'));
    currentUser.user = data.User;
    currentUser.IsAgree = data.IsAgree;
    currentUser.ProfileImageAttachmentFileHandle = data.ProfileImageAttachmentFileHandle;
    currentUser.menus = data.Menus;
    currentUser.dashboardItems = data.DashboardItems;
    currentUser.database = data.Database;
    currentUser.version = data.Version;
    currentUser.isReadonly = data.IsReadOnly;
    currentUser.lastUpdated = data.LastUpdated;
    currentUser.isDeleteDisabled = data.IsDeleteDisabled;
    localStorage.setItem('currentUser', JSON.stringify(currentUser));
  }

  showAlerts(alerts: any) {
    let that = this;
    if (alerts) {
      alerts.forEach(function (alert) {
        if (alert.IsRead && !that.cookieService.get(alert.SystemAlertId)) {
          that.snotifyService.prompt(" ", "System Alerts!", {
            backdrop: 0.3,
            timeout: 0,
            closeOnClick: false,
            position: SnotifyPosition.rightTop,
            html: '<div class="custom-div"><h3>System Alert!</h3><div class="content">' + alert.AlertText + '</div></div>',
            buttons: [
              { text: 'Read', action: (toast) => { that.addAlertToLocal(toast, alert); }, bold: true }
            ]
          });
        }
        else if (!that.cookieService.get(alert.SystemAlertId)) {
          that.snotifyService.prompt(" ", "System Alerts!", {
            backdrop: 0.3,
            timeout: 3000,
            closeOnClick: true,
            position: SnotifyPosition.rightTop,
            html: '<div class="custom-div"><h3>System Alert!</h3><div class="content">' + alert.AlertText + '</div></div>',
          });
        }
      });
    }
  }

  addAlertToLocal(toast: any, alert: any) {
    let that = this;
    that.cookieService.set(alert.SystemAlertId, JSON.stringify(alert));
    that.snotifyService.remove(toast.id);
  }
  
  setTitle() {
    this.titleService.setTitle('PBASE');
  }
}

import { Component, OnInit, Output, ViewContainerRef, ViewChild, EventEmitter } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { LoginService } from '../authentication/login/login.service';
import { Observable } from 'rxjs/Rx';
import { HttpService } from '../shared/http.service';
import { NgxPermissionsService } from 'ngx-permissions';
import { BreadcrumbService } from '../shared/breadcrumb/breadcrumb.service';
import { ROUTES } from '../shared/sidebar/menu-items';
import { ToastrService } from 'ngx-toastr';
//custom
import { Breadcrumb } from '../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus, InfoMessage } from '../shared/all-routes';

@Component({
    selector: 'app-dashboard-tc',
  templateUrl: './dashboard-tc.component.html',
  styleUrls: ['./dashboard-tc.component.scss']
})
export class DashboardTCComponent implements OnInit {
    data: any = {};
  agreements: any = [];
  isEdit: boolean = false;
  isSaving: boolean | number = false;
  isDeleting: boolean | number = false;
  public loading = false;
  public lastagreement = false;
  ordering: any = [];
  menu: any = [];
  @Output() agreedOnAgreement: EventEmitter<any> = new EventEmitter<any>();
  TermsHeader: string = "Terms & Conditions";
  indexNum: number = 0;
  constructor(
      private httpService: HttpService,
      private route: ActivatedRoute,
      private loginService: LoginService,
      private toastr: ToastrService,
      private router: Router,
      private ngxPermissionsService: NgxPermissionsService,
      private breadcrumbService: BreadcrumbService,
    ) {
  }

  ngOnInit() {
      let that = this;
      let count = 0;
      for (let i = 0; i < ROUTES.length; i++) {
          if (ROUTES[i].path == '') {
              for (let j = 0; j < ROUTES[i].permissions.length; j++) {
                this.ordering[count] = ROUTES[i].permissions[j];
                  this.menu.push(ROUTES[i].submenu[j]);
                  count++;
              }
          }
          else {
            this.ordering[count] = ROUTES[i].permissions[0];
              this.menu.push(ROUTES[i]);
              count++;
          }
      }

      that.get();
      that.setBreadcrumbs();
  }

  get() {
    let that = this;
    that.loading = true;
    that.httpService
      .get("vw_UserAgreementForm")
      .subscribe((res: any) => {
        that.agreements = res;
        if (that.agreements.length > 0) {
          that.TermsHeader = that.agreements[0].AgreementHeader;
          that.setBreadcrumbs();
        }
        that.loading = false;
      });
    that.httpService
      .get("RequestValidation")
      .subscribe(data => {
        that.data.RequestValidation = data;
        that.loading = false;
      }, erorr => {
      });
  }

  accept(index) {
      let that = this;
      that.save(index);
  }

  save(index) {
    let that = this;
    that.loading = true;
    that.data.IsAcceptDecline = 1;
    that.data.AgreementId = that.agreements[index].AgreementId;
    that.httpService
      .post("/vw_UserAgreementForm", that.data)
      .subscribe(id => {
        if (that.agreements.length == (index + 1)) {
          that.isSaving = false;
          that.lastagreement = true;
          that.loginService.filter("true");
          this.getUpdatedUser();
        }
        else {
          that.isSaving = false;
          window.scrollTo(0, 0);
          that.indexNum = that.indexNum + 1;
          that.loading = false;
          that.TermsHeader = that.agreements[that.indexNum].AgreementHeader;
          that.setBreadcrumbs();
        }
      }, erorr => {
        that.isSaving = false;
        that.loading = false;
      });
  }

  getUpdatedUser() {
      let that = this;
      that.httpService
          .get('User/')
          .subscribe((response: any) => {
              let currentUser: any = JSON.parse(localStorage.getItem('currentUser'));
              currentUser.user = response.User;
              currentUser.ProfileImageAttachmentFileHandle = response.ProfileImageAttachmentFileHandle;
              currentUser.menus = response.Menus;
              currentUser.dashboardItems = response.DashboardItems;
              currentUser.database = response.Database;
              currentUser.version = response.Version;
              currentUser.lastUpdated = response.LastUpdated;
              currentUser.isReadonly = response.IsReadOnly;
              currentUser.isDeleteDisabled = response.IsDeleteDisabled;
            localStorage.setItem('currentUser', JSON.stringify(currentUser));
            let menus = response.Menus.filter(x => x.AccessTypeId != null);
            let permissions: any[] = menus.map(x => x.Id);
            that.ngxPermissionsService.flushPermissions();
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
              that.loading = false;
              if (activemenu) {
                  that.router.navigate([activemenu.path]);
              }
              else {
                  that.router.navigate(['/profile']);
              }
          }, erorr => {
              that.toastr.error('Something went wrong with updated user!', 'Oops!');
          });
  }

  decline(index) {
    let that = this;
    that.loading = true;
    that.data.IsAcceptDecline = 0;
    that.data.AgreementId = that.agreements[index].AgreementId;
    that.httpService
      .post("/vw_UserAgreementForm", that.data)
      .subscribe(id => {
        that.loading = false;
        that.loginService.logout();
      }, erorr => {
        that.isSaving = false;
        that.loading = false;
      });

  }

  setBreadcrumbs() {
      let that = this;
      let breadcrumbs: Breadcrumb[] = [];
      breadcrumbs.push({
          label: that.TermsHeader,
          url: '',
          params: {}
      });
      that.breadcrumbService.set(breadcrumbs);
  }
}

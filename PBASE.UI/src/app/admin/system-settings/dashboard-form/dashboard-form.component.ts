import { Component, OnInit, ViewContainerRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { LookupService } from "../../../shared/lookup.service";
import { ToastrService } from 'ngx-toastr';
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { ConfirmationDialogService } from 'src/app/shared/confirmation-dialog/confirmation-dialog.service';
import { CryptoService } from '../../../shared/crypto/crypto.service';
import { HttpService } from '../../../shared/http.service';
import { Menu, pBMenus, InfoMessage } from '../../../shared/all-routes';

@Component({
  selector: 'app-dashboard-form',
  templateUrl: './dashboard-form.component.html',
  styleUrls: ['./dashboard-form.component.scss']
})

export class DashboardFormComponent implements OnInit {
  @ViewChild('dashboardForm') form;
  queryParams: any = {};
  data: any = {};
  dashboardOption: string;
  isEdit: boolean = false;
  isSaving: boolean | number = false;
  isDeleting: boolean | number = false;
  submitted = false;
  isLoading: boolean = false;
  appInfo: any = {};
  dashboardTab: any = {};
  pBRoutes: Menu = pBMenus.find(x => x.id == "DashboardOptions");

  constructor(
    private router: Router,
    private lookupService: LookupService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private breadcrumbService: BreadcrumbService,
    private cryptoService: CryptoService,
    private httpService: HttpService,
    private confirmationDialogService: ConfirmationDialogService,
  ) {

  }

  ngOnInit() {
    let that = this;
    that.queryParams = that.route.snapshot.queryParams;
    that.appInfo = that.httpService.getAppInfo(-9985);
    that.dashboardTab = that.httpService.getAppInfo(-9947);
    that.dashboardOption = that.cryptoService.decryptString(that.queryParams.dashboardOption);

    //verifiying the params
    if (!that.queryParams.id || !that.dashboardOption) {
      that.router.navigate(['/' + that.pBRoutes.grid.routeInfo.route]);
    }
    that.data.DashboardOption = that.dashboardOption;
    that.get();
    that.setBreadcrumbs();
  }

  get() {
    let that = this;
    that.isLoading = true;
    that.httpService
      .get(that.pBRoutes.api + (that.queryParams.id ? that.queryParams.id : '0'))
      .subscribe((res: any) => {
        that.data = res;
        that.data.DashboardOption = that.dashboardOption;
        let roles = [];
        that.data.AspNetRoleIds.forEach((id) => {
          let menu = that.data.allRoles.find(x => x.LookupId == id);
          if (menu) {
            roles.push(menu);
          }
        });
        that.sortRoles(roles);
        that.isLoading = false;
      });
  }

  sortRoles(roles) {
    let sortedRoles = roles.sort((a, b) => a.LookupValue.localeCompare(b.LookupValue));
    this.data.AspNetRoleIds = sortedRoles.map(x => x.LookupId);
  }

  submit(valid: boolean) {
    let that = this;
    that.submitted = true;
    if (valid && !that.appInfo.isUserReadonly) {
      that.isSaving = true;
      that.queryParams.id ? that.update() : that.save();
    }
    else {
      valid == true ? that.toastr.error('You cannot save your changes') : "";
    }
  }

  save() {
    let that = this;
    that.httpService
      .post(that.pBRoutes.api, that.data)
      .subscribe(id => {
        that.form.control.markAsPristine();
        that.goBack(InfoMessage.SaveMessage);
      }, erorr => {
        that.isSaving = false;
      });
  }

  update() {
    let that = this;
    that.httpService
      .put(that.pBRoutes.api + that.queryParams.id, that.data)
      .subscribe(template => {
        that.form.control.markAsPristine();
        that.goBack(InfoMessage.SaveMessage);
      }, erorr => {
        that.isSaving = false;
      });
  }

  delete() {
    let that = this;
    that.isDeleting = true;
    that.httpService
      .delete(that.pBRoutes.api + that.queryParams.id)
      .subscribe(template => {
        that.form.control.markAsPristine();
        that.goBack(InfoMessage.DeleteMessage);
      }, erorr => {
        that.isDeleting = false;
      });
  }

  goBack(message?: string) {
    let that = this;
    that.router.navigated = false;
    that.router
      .navigate(['/' + that.pBRoutes.grid.routeInfo.route], {
        queryParams: {
          localName: that.cryptoService.encryptString(that.pBRoutes.grid.vwName)
        }
      }).then(() => {
        if (message) {
          that.toastr.success(message);
        }
      });
  }

  setBreadcrumbs() {
    let that = this;
    let breadcrumbs: Breadcrumb[] = [];
    breadcrumbs.push({
      label: that.pBRoutes.grid.routeInfo.label,
      url: '/' + that.pBRoutes.grid.routeInfo.route,
      params: {
        localName: that.cryptoService.encryptString(that.pBRoutes.grid.vwName)
      }
    });
    breadcrumbs.push({
      label: that.pBRoutes.editForm.routeInfo.label,
      url: '',
      params: {}
    });
    this.breadcrumbService.set(breadcrumbs);
  }
}

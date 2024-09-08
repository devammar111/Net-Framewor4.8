import { Component, OnInit, ViewContainerRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { LookupService } from "../../../shared/lookup.service";
import { ToastrService } from 'ngx-toastr';
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { ConfirmationDialogService } from 'src/app/shared/confirmation-dialog/confirmation-dialog.service';
import { CryptoService } from '../../../shared/crypto/crypto.service';
import { HttpService } from '../../../shared/http.service';
import { Menu, pBMenus, InfoMessage } from 'src/app/shared/all-routes';

@Component({
  selector: 'app-options-form',
  templateUrl: './options-form.component.html',
  styleUrls: ['./options-form.component.scss']
})

export class OptionsFormComponent implements OnInit {
  @ViewChild('optionsForm') form;

  optionTab: any = {};
  queryParams: any = {};
  menuOption: string;
  data: any = {};
  isEdit: boolean = false;
  isLoading: boolean = false;
  isSaving: boolean | number = false;
  isDeleting: boolean | number = false;
  appInfo: any = {};
  submitted = false;
  optionsRoutes: Menu = pBMenus.find(x => x.id == "Options");

  constructor(
    private router: Router,
    private lookupService: LookupService,
    private route: ActivatedRoute,
    private breadcrumbService: BreadcrumbService,
    private cryptoService: CryptoService,
    private httpService: HttpService,
    private toastr: ToastrService,
    private confirmationDialogService: ConfirmationDialogService,
  ) {
  }

  ngOnInit() {
    let that = this;
    that.optionTab = that.httpService.getAppInfo(-9980);
    that.appInfo = that.httpService.getAppInfo(-9985);
    that.queryParams = that.route.snapshot.queryParams;
    that.menuOption = that.cryptoService.decryptString(that.queryParams.menuOption);

    //verifiying the params
    if (!that.queryParams.id || !that.menuOption) {
      that.router.navigate(['/' + that.optionsRoutes.grid.routeInfo.route]);
    }
    that.data.MenuOption = that.menuOption;
    that.get();
    that.setBreadcrumbs();
  }

  get() {
    let that = this;
    that.isLoading = true;
    that.httpService
      .get(that.optionsRoutes.api + (that.queryParams.id ? that.queryParams.id : '0'))
      .subscribe((res: any) => {
        that.data = res;
        that.data.MenuOption = that.menuOption;
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
      .post(that.optionsRoutes.api, that.data)
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
      .put(that.optionsRoutes.api + that.queryParams.id, that.data)
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
      .delete(that.optionsRoutes.api + that.queryParams.id)
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
      .navigate(['/' + that.optionsRoutes.grid.routeInfo.route], {
        queryParams: {
          localName: that.cryptoService.encryptString(that.optionsRoutes.grid.vwName)
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
      label: that.optionsRoutes.grid.routeInfo.label,
      url: '/' + that.optionsRoutes.grid.routeInfo.route,
      params: {
          localName: that.cryptoService.encryptString(that.optionsRoutes.grid.vwName)
      }
    });
    breadcrumbs.push({
      label: that.optionsRoutes.editForm.routeInfo.label,
      url: '',
      params: {}
    });
    this.breadcrumbService.set(breadcrumbs);
  }
}

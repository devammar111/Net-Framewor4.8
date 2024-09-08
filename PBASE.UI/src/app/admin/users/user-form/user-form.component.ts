import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
//service
import { ConfirmationDialogService } from '../../../shared/confirmation-dialog/confirmation-dialog.service';
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { ToastrService } from 'ngx-toastr';
import { HttpService } from '../../../shared/http.service';
import { LookupService } from "../../../shared/lookup.service";
import { NgxPermissionsService } from 'ngx-permissions';
import { CryptoService } from '../../../shared/crypto/crypto.service';
//custom
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus, InfoMessage } from '../../../shared/all-routes';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.scss']
})
export class UserFormComponent implements OnInit {
  data: any = {};
  queryParams: any = {};
  heading: string;
  api: string = 'userform/';
  apiResendEmail: string = 'account/setpassword/';
  userSection: Menu = pBMenus.find(x => x.id == "Users");
  userForm: FormGroup;
  form: FormGroup;
  isSaving: boolean | number = false;
  Lockouts: any[];
  allUserGroups = [];
  submitted = false;
  LockoutEnabled: boolean = false;
  IsDeleteDisabled: boolean = false;
  IsReadOnly: boolean = false;
  showChangePassword: boolean = false;
  appInfo: any = {};

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private breadcrumbService: BreadcrumbService,
    private confirmationDialogService: ConfirmationDialogService,
    private toastr: ToastrService,
    private lookupService: LookupService,
    private cryptoService: CryptoService,
    private httpService: HttpService,
    private ngxPermissionsService: NgxPermissionsService,
    fb: FormBuilder
  ) {
    this.userForm = fb.group({
      userName: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
      assignedRoles: ['', Validators.required],
      accountClosed: ['', Validators.required],
      IsReadOnly: ['', Validators.required],
      IsDeleteDisabled: ['', Validators.required],
      userGroup: ['', Validators.required],
      isReadOnly: [''],
    })
    this.form = this.userForm;
  }

  ngOnInit() {
      let that = this;
    that.appInfo = that.httpService.getAppInfo(-9987);
      that.queryParams = that.route.snapshot.queryParams;
      that.heading = that.queryParams.id ? that.userSection.editForm.routeInfo.label : that.userSection.addForm.routeInfo.label;
      that.get();
      that.setBreadcrumbs();
  }

  get() {
    let that = this;
    that.httpService
      .get(that.api + (that.queryParams.id ? that.queryParams.id : '0'))
      .subscribe((res: any) => {
        that.data = res;
        that.Lockouts = that.lookupService.getBooleanOptions();
        let selectedRole = that.data.AssignedRoles ? that.data.vw_lookuprole.find(x => x.LookupValue == that.data.AssignedRoles[0]) : null;
        if (selectedRole) {
          that.data.RoleId = selectedRole.LookupId;
          that.onChangeUserType(that.data.RoleId);
        }
        that.LockoutEnabled = that.queryParams.id ? that.data.LockoutEnabled : that.lookupService.defaultLockoutEnabled;
        that.IsReadOnly = that.data.IsReadOnly;
        that.IsDeleteDisabled = that.data.IsDeleteDisabled;
        that.showChangePassword = that.data.PasswordHash ? false : true;
        that.filterRole();
      });
  }

   filterRole() {
      let that = this;
     let currentUserTypeId = JSON.parse(localStorage.getItem('currentUser')).user.UserTypeId;
     that.data.vw_lookuprole = (currentUserTypeId != 3 ? that.data.vw_lookuprole.filter(x => x.LookupId != 3) : that.data.vw_lookuprole);     
  }

  onChangeUserType(id) {
    let that = this;
    if (id) {
      that.allUserGroups = that.data.vw_lookupusergroups.filter(x => x.AspNetRoleId == id);
      var userGroup = that.allUserGroups.find(x => x.LookupId == that.data.UserGroupId);
      if (!userGroup) {
        that.data.UserGroupId = null;
      }
    }
    else {
      that.allUserGroups = [];
      that.data.UserGroupId = null;
    }
  }

  resendEmail() {
    let that = this;
    that.httpService
      .put(that.apiResendEmail + that.queryParams.id, that.data)
      .subscribe(template => {
        that.form.markAsPristine();
        that.goBack("An Email has been sent to your Email Address to Set Password!");
      }, erorr => {
        that.isSaving = false;
      });
  }

  submit(valid: boolean) {
    let that = this;
    that.submitted = true;
    if (valid && !that.appInfo.isUserReadonly) {
      that.isSaving = true;
      that.data.AssignedRoles = [];
      that.data.vw_lookuprole.forEach((role: { LookupId: number, LookupValue: string }) => {
        if (role.LookupId == that.data.RoleId) {
          that.data.AssignedRoles.push(role.LookupValue);
        }
      });
      that.data.LockoutEnabled = that.LockoutEnabled;
      that.data.IsReadOnly = that.IsReadOnly;
      that.data.IsDeleteDisabled = that.IsDeleteDisabled;
      that.queryParams.id ? that.update() : that.save();
    }
    else {
      that.appInfo.isUserReadonly ? that.toastr.error('You cannot save your changes') : "";
    }
  }

  save() {
    let that = this;
    that.httpService
      .post(that.api, that.data)
      .subscribe(id => {
        that.form.markAsPristine();
        that.goBack(InfoMessage.SaveMessage);
      }, erorr => {
        that.isSaving = false;
      });
  }

  update() {
    let that = this;
    that.httpService
      .put(that.api + that.queryParams.id, that.data)
      .subscribe(user => {
        that.httpService
          .get('user/')
          .subscribe(response => {
            let currentUser: any = JSON.parse(localStorage.getItem('currentUser'));
            currentUser.user = response.User;
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
            that.ngxPermissionsService.loadPermissions(permissions);
            that.form.markAsPristine();
            that.goBack(InfoMessage.SaveMessage);
          });
      }, erorr => {
        that.isSaving = false;
      });
  }

  goBack(message?: string) {
    let that = this;
    that.router.navigated = false;
    that.router
      .navigate(['/' + that.userSection.grid.routeInfo.route], {
        queryParams: {
          localName: that.cryptoService.encryptString(that.userSection.grid.vwName),
        }
      })
      .then(() => {
        if (message) {
          that.toastr.success(message);
        }
      });
  }

  goToEdit(message?: string, id? : number) {
    let that = this;
    that.router.navigated = false;
    that.router
      .navigate(['/' + that.userSection.grid.routeInfo.route + '/' + that.userSection.editForm.routeInfo.route], {
        queryParams: {
          id: id
        }
      })
      .then(() => {
        if (message) {
          that.toastr.success(message);
        }
      });
  }

  setBreadcrumbs() {
    let that = this;
    let breadcrumbs: Breadcrumb[] = [];
    breadcrumbs.push({
      label: that.userSection.grid.routeInfo.label,
      url: "/" + that.userSection.grid.routeInfo.route,
      params: {
        localName: that.cryptoService.encryptString(that.userSection.grid.vwName)
      }
    });
    breadcrumbs.push({
      label: that.heading,
      url: '',
      params: {}
    });
    this.breadcrumbService.set(breadcrumbs);
  }

  onLockoutEnabled(value) {
    this.LockoutEnabled = value;
  }

  onIsReadOnly(value) {
      this.IsReadOnly = value;
  }

  onIsDeleteDisabled(value) {
      this.IsDeleteDisabled = value;
  }
}

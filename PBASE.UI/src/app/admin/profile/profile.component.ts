import { Component, OnInit, ViewContainerRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BreadcrumbService } from '../../shared/breadcrumb/breadcrumb.service';
import { ConfirmationDialogService } from '../../shared/confirmation-dialog/confirmation-dialog.service';
import { Breadcrumb } from '../../shared/breadcrumb/breadcrumb.model';
import { NgxPermissionsService } from 'ngx-permissions';
import { HttpService } from '../../shared/http.service';
import { Menu, pBMenus, InfoMessage } from '../../shared/all-routes';
import { CryptoService } from '../../shared/crypto/crypto.service';
import { ROUTES } from "../../shared/sidebar/menu-items";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  @ViewChild('profileForm') form;
  id: number;
  data: any = {};
  password: any = {};
  allMenuOptions = [];
  isGroup: boolean = false;
  isBespoke: boolean = false;
  isSaving: boolean | number = false;
  appInfo: any = {};
  submitted = false;
  queryParams: any = {};
  ordering: any = [];
  menu: any = [];
  permissions: any;
  fileOptions: any = {};
  pBRoutes: Menu = pBMenus.find(x => x.id == "Profile");

  constructor(
    private router: Router,
    private toastr: ToastrService,
    private breadcrumbService: BreadcrumbService,
    private confirmationDialogService: ConfirmationDialogService,
    private cryptoService: CryptoService,
    private ngxPermissionsService: NgxPermissionsService,
    private httpService: HttpService) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    }
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
    that.setBreadcrumbs();
    that.appInfo = that.httpService.getAppInfo(-1);
    that.get();
    that.fileOptions = { accept: [".jpg", ".png", ".gif",] };
  }


  get() {
    let that = this;
    that.httpService
      .get(that.pBRoutes.api+'User')
      .subscribe((res: any) => {
        that.data = res;
        that.id = that.data.Id;
        that.data.RoleId = that.data.AssignedRoles[0];
      });
  }

  submit(valid: boolean) {
    let that = this;
    that.submitted = true;
    if (valid && !that.appInfo.isUserReadonly) {
      that.isSaving = true;
      that.id ? that.update() : that.save();
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
      .put(that.pBRoutes.api + 'profile/' + that.id, that.data)
      .subscribe(response => {
        that.toastr.success('Profile has been updated successfully!', 'Success!');
        that.getUpdatedUser();
      }, erorr => {
        });
    that.isSaving = false;
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
        that.ngxPermissionsService.loadPermissions(permissions);
        that.form.control.markAsPristine();
        that.reloadForm();
      }, erorr => {
          that.toastr.error('Something went wrong with updated user!', 'Oops!');
      });
  }

  reloadForm() {
    this.router.navigated = false;
    this.router.navigate([this.router.url]);
  }

  onNotify(filedata: any) {
    let that = this;
    that.data.AttachmentFileName = filedata.FileName;
    that.data.AttachmentFileSize = filedata.FileSize;
    that.data.AttachmentFileType = filedata.MimeType;
    that.data.AttachmentFileHandle = filedata.FileHandle;
    that.form.controls.attachment.markAsDirty();
    that.form.controls.attachment.markAsTouched();
  }

  goBack(message?: string) {
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
    this.breadcrumbService.set(breadcrumbs);
  }

}

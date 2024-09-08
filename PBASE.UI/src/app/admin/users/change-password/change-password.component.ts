import { Component, OnInit, ViewContainerRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { ChangePasswordService } from './change-password.service';
import { CryptoService } from '../../../shared/crypto/crypto.service'
import { ToastrService } from 'ngx-toastr';
import { RegistrationValidator } from '../../profile/confirmvalidator';
import { ConfirmationDialogService } from '../../../shared/confirmation-dialog/confirmation-dialog.service';
import { HttpService } from '../../../shared/http.service';
import { Menu, pBMenus, InfoMessage } from '../../../shared/all-routes';
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { ROUTES } from "../../../shared/sidebar/menu-items";

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {

  id: number;
  changePasswordForm: FormGroup;
  form: FormGroup;
  data: any = {};
  isUpdating: boolean | number = false;
  submitted = false;
  text: string = "(!\"#$%&'()*+,-./:;<=>?@[]^_`{|}~)";
  icon: string = "fa fa-times";
  icon_total_charecter: string = "fa fa-times";
  icon_spectial_charecter: string = "fa fa-times";
  icon_upper: string = "fa fa-times";
  icon_lower: string = "fa fa-times";
  icon_number: string = "fa fa-times";
  m_strUpperCase: string = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
  m_strLowerCase: string = "abcdefghijklmnopqrstuvwxyz";
  m_strNumber: string = "0123456789";
  m_strCharacters: string = "!\"#$%&'()*+,-./:;<=>?@[]^_`{|}~";
  show: boolean = false;
  showPassword: boolean = false;
  showConfPassword: boolean = false;
  ptext: string = "show";
  ptype: string = "password";
  cptype: string = "password";;
  cptext: string = "show";;
  appInfo: any = {};
  queryParams: any = {};
  ordering: any = [];
  menu: any = [];
  permissions: any;
  pBRoutes: Menu = pBMenus.find(x => x.id == "ChangePassword");

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private changePasswordService: ChangePasswordService,
    private confirmationDialogService: ConfirmationDialogService,
    private cryptoService: CryptoService,
    private breadcrumbService: BreadcrumbService,
    private toastr: ToastrService,
    private httpService: HttpService,
    fb: FormBuilder,
    private formBuilder: FormBuilder,
  ) {
    this.changePasswordForm = this.formBuilder.group({
      oldPassword: ['', Validators.required],
      newPassword: ['', Validators.required],
      confPassword: ['', Validators.required]
    }, {
        validator: RegistrationValidator.validate.bind(this)
      });
    this.form = this.changePasswordForm;
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
    that.appInfo = that.httpService.getAppInfo(-1);
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    //that.data.Id = currentUser.user.Id;
    that.queryParams.id = currentUser.user.Id;
    that.setBreadcrumbs();
  }

  submit(valid: boolean) {
    let that = this;
    that.submitted = true;
    if (valid && !that.appInfo.isUserReadonly) {
      that.isUpdating = true;
      that.queryParams.id ? that.update() : that.toastr.error('Something went wrong. Please logout and try again.');
    }
    else {
      valid == true ? that.toastr.error('You cannot save your changes') : "";
    }
  }

  update() {
    let that = this;
    that.httpService
      .put(that.pBRoutes.api + that.queryParams.id, that.data)
      .subscribe(res => {
        that.data = res;
        //that.form.control.markAsPristine();
        that.reloadForm('Password has been updated successfully!');
      }, erorr => {
        that.toastr.error('You cannot save your changes')
      });
    that.isUpdating = false;
  }

  reloadForm(message?: string) {
    let that = this;
    if (message) {
      that.toastr.success(message);
    }
    that.router.navigated = false;
    this.router.navigateByUrl(this.router.url);
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

  showList() {
    this.show = true;
  }

  hideList() {
    this.show = false;
  }

  inputChange() {
    if (this.data.NewPassword) {
      if (this.data.NewPassword.length >= 8) {
        this.icon_total_charecter = "fa fa-check";
      }
      else {
        this.icon_total_charecter = "fa fa-times";
      }

      let nUpperCount = this.countContain(this.data.NewPassword, this.m_strUpperCase);
      if (nUpperCount >= 1) {
        this.icon_upper = "fa fa-check";
      }
      else {
        this.icon_upper = "fa fa-times";
      }

      let nLowerCount = this.countContain(this.data.NewPassword, this.m_strLowerCase);
      if (nLowerCount >= 1) {
        this.icon_lower = "fa fa-check";
      }
      else {
        this.icon_lower = "fa fa-times";
      }
      let nChracterCount = this.countContain(this.data.NewPassword, this.m_strCharacters);
      if (nChracterCount >= 1) {
        this.icon_spectial_charecter = "fa fa-check";
      }
      else {
        this.icon_spectial_charecter = "fa fa-times";
      }
      let nNumberCount = this.countContain(this.data.NewPassword, this.m_strNumber);
      if (nNumberCount >= 1) {
        this.icon_number = "fa fa-check";
      }
      else {
        this.icon_number = "fa fa-times";
      }
      this.data.ConfirmPassword = this.data.NewPassword;
    }
    else {
      this.icon_total_charecter = "fa fa-times";
      this.icon_number = "fa fa-times";
      this.icon_spectial_charecter = "fa fa-times";
      this.icon_lower = "fa fa-times";
      this.icon_upper = "fa fa-times";
      this.data.ConfirmPassword = '';
    }

  }

  countContain(strPassword, strCheck) {
    // Declare variables
    var nCount = 0;

    for (let i = 0; i < strPassword.length; i++) {
      if (strCheck.indexOf(strPassword.charAt(i)) > -1) {
        nCount++;
      }
    }

    return nCount;
  }

  showHidePassword() {
    this.showPassword = !this.showPassword;
    if (this.showPassword) {
      this.ptype = "text";
      this.ptext = "hide";
    }
    else {
      this.ptype = "password";
      this.ptext = "show";
    }
  }

  showHideConfPassword() {
      this.showConfPassword = !this.showConfPassword;
      if (this.showConfPassword) {
      this.cptype = "text";
      this.cptext = "hide";
    }
    else {
      this.cptype = "password";
      this.cptext = "show";
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

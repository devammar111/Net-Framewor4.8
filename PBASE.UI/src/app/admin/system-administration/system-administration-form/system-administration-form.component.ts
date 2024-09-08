import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
//service
import { ConfirmationDialogService } from '../../../shared/confirmation-dialog/confirmation-dialog.service';
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { ToastrService } from 'ngx-toastr';
import { HttpService } from '../../../shared/http.service';
import { CryptoService } from '../../../shared/crypto/crypto.service';
//custom
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus, InfoMessage } from '../../../shared/all-routes';
import { ROUTES } from "../../../shared/sidebar/menu-items";

@Component({
  selector: 'app-system-administration-form',
  templateUrl: './system-administration-form.component.html',
  styles: []
})
export class SystemAdministrationFormComponent implements OnInit {

  SystemAdministration: Menu = pBMenus.find(x => x.id == "SystemAdministration");
  @ViewChild('SystemAdministrationForm') form;
  heading: string;
  data: any = {};
  queryParams: any = {};
  isSaving: boolean = false;
  appInfo: any = {};
  ordering: any = [];
  menu: any = [];
  permissions: any;

  constructor(
    private route: ActivatedRoute,
    private cryptoService: CryptoService,
    private httpService: HttpService,
    private router: Router,
    private breadcrumbService: BreadcrumbService,
    private toastr: ToastrService,
    private confirmationDialogService: ConfirmationDialogService
  ) {
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
    that.appInfo = that.httpService.getAppInfo(-9945);
    that.queryParams = that.route.snapshot.queryParams;
    that.heading =  that.SystemAdministration.editForm.routeInfo.label;
    that.get();
    that.setBreadcrumbs();
  }

  get() {
    let that = this;
    that.httpService
      .get(that.SystemAdministration.api)
      .subscribe((res: any) => {
        that.data = res;

      });
  }

  submit(valid: boolean) {
    let that = this;
    if (valid && !that.appInfo.isUserReadonly) {
      that.isSaving = true;
      that.httpService
        .put(that.SystemAdministration.api + that.data.EncApplicationInformationId, that.data)
        .subscribe(template => {
          that.form.control.markAsPristine();
          that.reload(InfoMessage.SaveMessage);
        }, erorr => {
          that.isSaving = false;
        });
    }
    else {
      valid == true ? that.toastr.error('You cannot save your changes') : "";
    }
  }

  goBack() {
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

  reload(message) {
    this.router.navigated = false;
    this.router.navigateByUrl(this.router.url).then(() => {
      if (message) {
        this.toastr.success(message);
      }
    });;
  }

  setBreadcrumbs() {
    let that = this;
    let breadcrumbs: Breadcrumb[] = [];
    breadcrumbs.push({
      label: that.heading,
      url: '',
      params: {}
    });
    that.breadcrumbService.set(breadcrumbs);
  }

}

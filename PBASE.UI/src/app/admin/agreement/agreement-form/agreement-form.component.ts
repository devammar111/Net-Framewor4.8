import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
//service
import { ConfirmationDialogService } from '../../../shared/confirmation-dialog/confirmation-dialog.service';
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { ToastrService } from 'ngx-toastr';
import { HttpService } from '../../../shared/http.service';
import { CryptoService } from '../../../shared/crypto/crypto.service';
//custom
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus, InfoMessage } from '../../../shared/all-routes';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { LookupService } from 'src/app/shared/lookup.service';
import { DateConvertor } from 'src/app/shared/date-convertor';
import { NgbDatepickerConfig } from '@ng-bootstrap/ng-bootstrap';
import { StateManagementService } from 'src/app/shared/state-management/state-management.service';

@Component({
  selector: 'app-agreement-form',
  templateUrl: './agreement-form.component.html',
  styles: []
})
export class AgreementFormComponent implements OnInit {
  agreement: Menu = pBMenus.find(x => x.id == "Agreement");
  @ViewChild('AgreementForm') form;
  dateConvertor: DateConvertor;
  heading: string;
  data: any = {};
  queryParams: any = {};
  isSaving: boolean = false;
  isDeleting: boolean = false;
  EmailTemplateTags: any = [];
  Lockouts: any[] = [];
  EmailTemplateTagsIds: any = [];
  IsDeleteDisabled: boolean = false;
  AgreementDateField: any;
  collapsed: any = [false, false, false];
  appInfo: any = {};
  editorConfig: AngularEditorConfig = {};

  constructor(
    private location: Location,
    private route: ActivatedRoute,
    private cryptoService: CryptoService,
    private httpService: HttpService,
    private router: Router,
    private lookupService: LookupService,
    private breadcrumbService: BreadcrumbService,
    private toastr: ToastrService,
    private config: NgbDatepickerConfig,
    private stateManagementService: StateManagementService,
    private confirmationDialogService: ConfirmationDialogService
  ) {
    let today = new Date();
    config.minDate = { year: today.getFullYear(), month: (today.getMonth() + 1), day: today.getDate() };
    config.outsideDays = 'hidden';
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    }
  }

  ngOnInit() {
    let that = this;
    that.dateConvertor = new DateConvertor();
    that.appInfo = that.httpService.getAppInfo(-9978);
    that.queryParams = that.route.snapshot.queryParams;
    that.getEditorConfig();
    that.heading = that.queryParams.id ? that.agreement.editForm.routeInfo.label : that.agreement.addForm.routeInfo.label;
    that.Lockouts = that.lookupService.getBooleanOptions();
    that.get();
    that.setBreadcrumbs();
  }

  ngOnDestroy() {
    let that = this;
    that.stateManagementService.save(that.agreement.id, that.collapsed);
  }

  get() {
    let that = this;
    that.httpService
      .get(that.agreement.api + (that.queryParams.id ? that.queryParams.id : '0'))
      .subscribe((res: any) => {
        that.data = res;
        that.collapsed = that.stateManagementService.set(that.agreement.id, that.collapsed);
        that.AgreementDateField = that.dateConvertor.fromModel(that.data.AgreementDate);
      });
  }

  submit(valid: boolean) {
    let that = this;
    that.data.AgreementDate = that.dateConvertor.toModel(that.AgreementDateField);
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
      .post(that.agreement.api, that.data)
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
      .put(that.agreement.api + that.queryParams.id, that.data)
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
      .delete(that.agreement.api + that.queryParams.id)
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
      .navigate(['/' + that.agreement.grid.routeInfo.route], {
        queryParams: {
          localName: that.cryptoService.encryptString(that.agreement.grid.vwName)
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
      label: that.agreement.grid.routeInfo.label,
      url: '/' + that.agreement.grid.routeInfo.route,
      params: {
        localName: that.cryptoService.encryptString(that.agreement.grid.vwName)
      }
    });
    breadcrumbs.push({
      label: that.heading,
      url: '',
      params: {}
    });
    that.breadcrumbService.set(breadcrumbs);
  }

  getEditorConfig() {
    let that = this;
    that.editorConfig = {
      spellcheck: true,
      minHeight: '10rem',
      placeholder: 'Enter text here...',
      translate: 'no',
      customClasses: [
        {
          name: "quote",
          class: "quote",
        },
        {
          name: 'redText',
          class: 'redText'
        },
        {
          name: "titleText",
          class: "titleText",
          tag: "h1",
        },
      ]
    };
    if (that.appInfo.isReadonly || eval(that.queryParams.IsLocked)) {
      that.editorConfig.editable = false;
      that.editorConfig.showToolbar = false;
    }
    else {
      that.editorConfig.editable = true;
    }
  }

  isLocked() {
    let that = this;
    if (eval(that.queryParams.IsLocked)) {
      return true;
    }
    return false;
  }

  isMenuReadOnly() {
    let that = this;
    if (that.appInfo.isReadonly) {
      return true;
    }
    return false;
  }

}

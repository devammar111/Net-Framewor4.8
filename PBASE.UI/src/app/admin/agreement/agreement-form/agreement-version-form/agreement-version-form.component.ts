import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
//service
import { ConfirmationDialogService } from '../../../../shared/confirmation-dialog/confirmation-dialog.service';
import { BreadcrumbService } from '../../../../shared/breadcrumb/breadcrumb.service';
import { ToastrService } from 'ngx-toastr';
import { HttpService } from '../../../../shared/http.service';
import { CryptoService } from '../../../../shared/crypto/crypto.service';
//custom
import { Breadcrumb } from '../../../../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus, InfoMessage } from '../../../../shared/all-routes';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { LookupService } from 'src/app/shared/lookup.service';
import { DateConvertor } from 'src/app/shared/date-convertor';
import { StateManagementService } from 'src/app/shared/state-management/state-management.service';

@Component({
   selector: 'app-agreement-version-form',
   templateUrl: './agreement-version-form.component.html',
  styles: []
})
export class AgreementVersionFormComponent implements OnInit {
  agreement: Menu = pBMenus.find(x => x.id == "Agreement");
   agreementversion: Menu = pBMenus.find(x => x.id == "AgreementVersion");
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
   appInfo: any = {};
   collapsed: any = [true];

  editorConfig: AngularEditorConfig = {
     editable: false,
     enableToolbar:false,
    spellcheck: true,
    minHeight: '10rem',
    placeholder: 'Enter text here...',
    translate: 'no',
    //uploadUrl: 'v1/images', // if needed
    customClasses: [ // optional
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

  constructor(
    private route: ActivatedRoute,
    private cryptoService: CryptoService,
    private httpService: HttpService,
    private router: Router,
    private lookupService: LookupService,
     private breadcrumbService: BreadcrumbService,
     private stateManagementService: StateManagementService,
    private toastr: ToastrService,
    private confirmationDialogService: ConfirmationDialogService
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    }
  }

  ngOnInit() {
    let that = this;
    that.dateConvertor = new DateConvertor();
    that.appInfo = that.httpService.getAppInfo(-9978);
    that.queryParams = that.route.snapshot.queryParams;
     that.heading = that.queryParams.id ? that.agreementversion.editForm.routeInfo.label : that.agreementversion.addForm.routeInfo.label;
    that.Lockouts = that.lookupService.getBooleanOptions();
    that.get();
    that.setBreadcrumbs();
  }

  get() {
    let that = this;
    that.httpService
       .get(that.agreementversion.api + (that.queryParams.id ? that.queryParams.id : '0'))
      .subscribe((res: any) => {
        that.data = res;
        that.data.vw_lookupRole = (that.queryParams.id ? that.data.vw_lookupRole : that.data.vw_lookupRole.filter(x => x.GroupBy === "ACTIVE"));
         that.AgreementDateField = that.dateConvertor.fromModel(that.data.AgreementDate);
         that.collapsed = that.stateManagementService.set(that.agreementversion.id, that.collapsed);
      });
   }
   ngOnDestroy() {
      let that = this;
      that.stateManagementService.save(that.agreementversion.id, that.collapsed);
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
       .post(that.agreementversion.api, that.data)
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
       .put(that.agreementversion.api + that.queryParams.id, that.data)
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
       .delete(that.agreementversion.api + that.queryParams.id)
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
       .navigate(['/' + that.agreementversion.grid.routeInfo.route], {
          queryParams: {
             id: that.queryParams.id,
             IsLocked: that.queryParams.IsLocked,
             localName: that.cryptoService.encryptString(that.agreementversion.grid.vwName)
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
       label: that.agreement.editForm.routeInfo.label,
       url: '/agreement/edit-agreement',
       params: {
          id: that.queryParams.id,
          IsLocked: that.queryParams.IsLocked,
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

}

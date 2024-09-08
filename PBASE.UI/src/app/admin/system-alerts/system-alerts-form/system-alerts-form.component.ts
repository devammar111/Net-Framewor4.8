import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
//service
import { ConfirmationDialogService } from '../../../shared/confirmation-dialog/confirmation-dialog.service';
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { ToastrService } from 'ngx-toastr';
import { HttpService } from '../../../shared/http.service';
import { CryptoService } from '../../../shared/crypto/crypto.service';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
//custom
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus, InfoMessage } from '../../../shared/all-routes';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { LookupService } from 'src/app/shared/lookup.service';
import { DateConvertor } from 'src/app/shared/date-convertor';

@Component({
  selector: 'app-system-alerts-form',
  templateUrl: './system-alerts-form.component.html',
  styles: []
})
export class SystemAlertsFormComponent implements OnInit {
  SystemAlerts: Menu = pBMenus.find(x => x.id == "SystemAlerts");
  dateConvertor: DateConvertor;
  @ViewChild('SystemAlertsForm') form;
  heading: string;
  ClosedDateTimeField: any;
  OpenDateTimeField: any;
  data: any = {};
  queryParams: any = {};
  isSaving: boolean = false;
  isDeleting: boolean = false;
  EmailTemplateTags: any = [];
  Lockouts: any[] = [];
  EmailTemplateTagsIds: any = [];
  appInfo: any = {};
  editorConfig: AngularEditorConfig = {
    editable: true,
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
    that.appInfo = that.httpService.getAppInfo(-9979);
    that.queryParams = that.route.snapshot.queryParams;
    that.heading = that.queryParams.id ? that.SystemAlerts.editForm.routeInfo.label : that.SystemAlerts.addForm.routeInfo.label;
    that.Lockouts = that.lookupService.getBooleanOptions();
    that.get();
    that.setBreadcrumbs();
  }

  get() {
    let that = this;
    that.httpService
      .get(that.SystemAlerts.api + (that.queryParams.id ? that.queryParams.id : '0'))
      .subscribe((res: any) => {
        that.data = res;
        that.ClosedDateTimeField = that.dateConvertor.getDateTimeFromModel(that.data.ClosedDateTime);
        that.OpenDateTimeField = that.dateConvertor.getDateTimeFromModel(that.data.OpenDateTime);

      });
  }

  submit(valid: boolean) {
    let that = this;
    if (valid && !that.appInfo.isUserReadonly) {
      that.isSaving = true;
      that.data.ClosedDateTime = that.dateConvertor.dateTimeToModel(that.ClosedDateTimeField);
      that.data.OpenDateTime = that.dateConvertor.dateTimeToModel(that.OpenDateTimeField);
      that.queryParams.id ? that.update() : that.save();
    }
    else {
      valid == true ? that.toastr.error('You cannot save your changes') : "";
    }
  }

  save() {
    let that = this;
    that.httpService
      .post(that.SystemAlerts.api, that.data)
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
      .put(that.SystemAlerts.api + that.queryParams.id, that.data)
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
      .delete(that.SystemAlerts.api + that.queryParams.id)
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
      .navigate(['/' + that.SystemAlerts.grid.routeInfo.route], {
        queryParams: {
          localName: that.cryptoService.encryptString(that.SystemAlerts.grid.vwName)
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
      label: that.SystemAlerts.grid.routeInfo.label,
      url: '/' + that.SystemAlerts.grid.routeInfo.route,
      params: {
        localName: that.cryptoService.encryptString(that.SystemAlerts.grid.vwName)
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

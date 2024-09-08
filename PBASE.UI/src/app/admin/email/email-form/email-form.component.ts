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
import { DateConvertor } from '../../../shared/date-convertor';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { FilePickerService } from '../../../shared/file-picker/file-picker.service';

@Component({
  selector: 'app-email-form',
  templateUrl: './email-form.component.html',
  styleUrls: ['./email-form.component.scss']
})
export class EmailFormComponent implements OnInit {
  heading: string;
  data: any = {};
  queryParams: any = {};
  isSaving: boolean = false;
  isDeleting: boolean = false;
  emails: Menu = pBMenus.find(x => x.id == "Email");
  @ViewChild('emailForm') form;
  RequestedDateField: any;
  SentDateField: any;
  dateConvertor: DateConvertor;
  appInfo: any = {};
  public myDatePickerOptions: {
      dateFormat: 'dd/yy',
  };

  editorConfig: AngularEditorConfig = {
    editable: false,
    spellcheck: true,
    translate: 'no',
    showToolbar: false
  };

  constructor(
    private route: ActivatedRoute,
    private httpService: HttpService,
    public filePickerService: FilePickerService,
    private cryptoService: CryptoService,
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
    that.appInfo = that.httpService.getAppInfo(-9981);
    that.dateConvertor = new DateConvertor();
    that.queryParams = that.route.snapshot.queryParams;
    that.heading = that.queryParams.id ? that.emails.editForm.routeInfo.label : that.emails.addForm.routeInfo.label;
    that.get();
    that.setBreadcrumbs();
  }

  get() {
    let that = this;
    that.httpService
      .get(that.emails.api + (that.queryParams.id ? that.queryParams.id : '0'))
      .subscribe((res: any) => {
        that.data = res;
        that.RequestedDateField = that.dateConvertor.fromModel(that.data.RequestedDate);
        that.SentDateField = that.dateConvertor.fromModel(that.data.SentDate);
      });
  }

  getsrc(fileHandle) {
    return this.filePickerService.generateLinkPreview(fileHandle);
  }

  resend(valid: boolean) {
    let that = this;
    if (valid && !that.appInfo.isUserReadonly) {
      that.isSaving = true;
      that.httpService
        .post(that.emails.api, that.data)
        .subscribe(id => {
          that.form.control.markAsPristine();
          that.goBack("Email has been sent.");
        }, erorr => {
          that.isSaving = false;
        });
    }
    else {
      valid == true ? that.toastr.error('You cannot save your changes') : "";
    }
  }

  goBack(message?: string) {
    let that = this;
    that.router.navigated = false;
    that.router
      .navigate(['/' + that.emails.grid.routeInfo.route], {
        queryParams: {
          localName: that.cryptoService.encryptString(that.emails.grid.vwName)
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
      label: that.emails.grid.routeInfo.label,
      url: '/' + that.emails.grid.routeInfo.route,
      params: {
        localName: that.cryptoService.encryptString(that.emails.grid.vwName)
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

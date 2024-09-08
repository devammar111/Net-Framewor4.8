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
import { AngularEditorConfig } from '@kolkov/angular-editor';

@Component({
  selector: 'app-email-template-form',
  templateUrl: './email-template-form.component.html',
  styleUrls: ['./email-template-form.component.css']
})
export class EmailTemplateFormComponent implements OnInit {

  heading: string;
  data: any = {};
  queryParams: any = {};
  isSaving: boolean = false;
  isDeleting: boolean = false;
  hideSave: boolean = false
  emailtemplate: Menu = pBMenus.find(x => x.id == "EmailTemplate");
  EmailTemplateTags: any = [];
  EmailTemplateTagsIds: any = [];
  appInfo: any = {};

  @ViewChild('emailTemplateForm') form;
  editorConfig: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '25rem',
    minHeight: '5rem',
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
    that.appInfo = that.httpService.getAppInfo(-9982);
      that.queryParams = that.route.snapshot.queryParams;
      that.heading = that.queryParams.id ? that.emailtemplate.editForm.routeInfo.label : that.emailtemplate.addForm.routeInfo.label;
      that.get();
      that.setBreadcrumbs();
  }

  get() {
    let that = this;
    that.httpService
      .get(that.emailtemplate.api + (that.queryParams.id ? that.queryParams.id : '0'))
      .subscribe((res: any) => {
        that.data = res;
        that.EmailTemplateTags = that.data.EmailTemplateTags.filter(x => x.LookupExtraInt == that.data.EmailTemplateTypeId);
        that.EmailTemplateTagsIds = that.data.EmailTemplateTagIds;
      });
  }

  submit(valid: boolean) {
    let that = this;
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
      .post(that.emailtemplate.api, that.data)
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
      .put(that.emailtemplate.api + that.queryParams.id, that.data)
      .subscribe(email => {
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
      .delete(that.emailtemplate.api + that.queryParams.id)
      .subscribe(email => {
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
      .navigate(['/' + that.emailtemplate.grid.routeInfo.route], {
        queryParams: {
          localName: that.cryptoService.encryptString(that.emailtemplate.grid.vwName)
        }
      }).then(() => {
        if (message) {
          that.toastr.success(message);
        }
      });
  }

  onTemplateTypeChange(id) {
    let that = this;
    //that.data.EmailTemplateTagIds = null;
    that.EmailTemplateTags = that.data.EmailTemplateTags;
    that.EmailTemplateTags = that.EmailTemplateTags.filter(x => x.LookupExtraInt == id);
    that.EmailTemplateTagsIds = [];
    that.EmailTemplateTags.forEach(function (element) {
      that.EmailTemplateTagsIds.push(element.LookupId);
    });
    var templateType = that.data.vw_LookupEmailTemplateType.filter(x => x.LookupId == id);
    if (that.queryParams.id) {
      if (templateType[0].LookupExtraInt == -9971 || templateType[0].LookupExtraInt == -9972) {
        that.httpService
          .get('checkEmailTemplate/' + id)
          .subscribe(data => {
            if (data == "yes") {
              that.hideSave = true;
              that.toastr.info("Only one template is allowed for this Template Type");
            }
            else {
              that.hideSave = false;
            }
          });
      }
      else {
        that.hideSave = false;
      }
    }
    else if (templateType[0].LookupExtraInt == -9971) {
      that.httpService
        .get('checkEmailTemplate/' + id)
        .subscribe(data => {
          if (data == "yes") {
            that.hideSave = true;
            that.toastr.info("Only one template is allowed for this Template Type");
          }
          else {
            that.hideSave = false;
          }
        });
    }
    else {
      that.hideSave = false;
    }
  }

  setBreadcrumbs() {
    let that = this;
    let breadcrumbs: Breadcrumb[] = [];
    breadcrumbs.push({
      label: that.emailtemplate.grid.routeInfo.label,
      url: '/' + that.emailtemplate.grid.routeInfo.route,
      params: {
        localName: that.cryptoService.encryptString(that.emailtemplate.grid.vwName)
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

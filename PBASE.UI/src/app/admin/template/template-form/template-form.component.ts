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

@Component({
  selector: 'app-template-form',
  templateUrl: './template-form.component.html',
  styleUrls: ['./template-form.component.scss']
})
export class TemplateFormComponent implements OnInit {
  heading: string;
  data: any = {};
  queryParams: any = {};
  isSaving: boolean = false;
  isDeleting: boolean = false;
  hideSave: boolean = false;
  TemplateTags: any = [];
  TemplateTagsIds: any = [];
  savedTemplateTypeId: number = 0;
  appInfo: any = {};

  templates: Menu = pBMenus.find(x => x.id == "Templates");
  @ViewChild('templateForm') form;

  constructor(
    private route: ActivatedRoute,
    private httpService: HttpService,
    private router: Router,
    private breadcrumbService: BreadcrumbService,
    private toastr: ToastrService,
    private cryptoService: CryptoService,
    private confirmationDialogService: ConfirmationDialogService
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    }
  }

  ngOnInit() {
      let that = this;
    that.appInfo = that.httpService.getAppInfo(-9989);
      that.queryParams = that.route.snapshot.queryParams;
      that.heading = that.queryParams.id ? that.templates.editForm.routeInfo.label : that.templates.addForm.routeInfo.label;
      that.get();
      that.setBreadcrumbs();
  }

  get() {
    let that = this;
    that.httpService
      .get(that.templates.api + (that.queryParams.id ? that.queryParams.id : '0'))
      .subscribe((res: any) => {
        that.data = res;
        that.savedTemplateTypeId = that.data.TemplateTypeId;
        that.TemplateTags = that.data.TemplateTags;
        that.TemplateTags = that.TemplateTags.filter(x => x.LookupExtraInt == that.data.TemplateTypeId);
        that.TemplateTagsIds = that.data.TemplateTagIds;
      });
  }

  submit(valid: boolean) {
    let that = this;
    if (valid && that.data.AttachmentFileHandle && !that.appInfo.isUserReadonly) {
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
      .post(that.templates.api, that.data)
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
      .put(that.templates.api + that.queryParams.id, that.data)
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
      .delete(that.templates.api + that.queryParams.id)
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
      .navigate(['/' + that.templates.grid.routeInfo.route], {
        queryParams: {
          localName: that.cryptoService.encryptString(that.templates.grid.vwName)
        }
      }).then(() => {
        if (message) {
          that.toastr.success(message);
        }
      });
  }

  onTemplateTypeChange(id) {
      let that = this;    
      if (id) {
          that.TemplateTags = that.data.TemplateTags;
          that.TemplateTags = that.TemplateTags.filter(x => x.LookupExtraInt == id);
          that.TemplateTagsIds = [];
          that.TemplateTags.forEach(function (element) {
              that.TemplateTagsIds.push(element.LookupId);
          });

          var templateType = that.data.vw_LookupTemplateTypes.filter(x => x.LookupId == id);
          if (that.queryParams.id) {
              if (templateType[0].LookupExtraInt == -9971 || templateType[0].LookupExtraInt == -9972) {
                  that.httpService
                      .get('checkTemplate/' + id )
                      .subscribe(data => {
                          if (that.savedTemplateTypeId == id) {
                              that.hideSave = false;
                          }
                          else if (data == "yes") {
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
                  .get('checkTemplate/' + id)
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
      else {
          that.hideSave = false;
      }
    
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

  setBreadcrumbs() {
    let that = this;
    let breadcrumbs: Breadcrumb[] = [];
    breadcrumbs.push({
      label: that.templates.grid.routeInfo.label,
      url: '/' + that.templates.grid.routeInfo.route,
      params: {
        localName: that.cryptoService.encryptString(that.templates.grid.vwName)
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

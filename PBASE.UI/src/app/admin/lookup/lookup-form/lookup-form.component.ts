import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
//service
import { ConfirmationDialogService } from '../../../shared/confirmation-dialog/confirmation-dialog.service';
import { LookupService } from "../../../shared/lookup.service";
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { ToastrService } from 'ngx-toastr';
import { HttpService } from '../../../shared/http.service';
import { CryptoService } from '../../../shared/crypto/crypto.service';
//custom
import { DateConvertor } from '../../../shared/date-convertor';;
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus, InfoMessage } from '../../../shared/all-routes';

@Component({
  selector: 'app-lookup-form',
  templateUrl: './lookup-form.component.html',
  styleUrls: ['./lookup-form.component.scss']
})
export class LookupFormComponent implements OnInit {
  queryParams: any = {};
  lookupTypes: Menu = pBMenus.find(x => x.id == "LookupType");
  lookups: Menu = pBMenus.find(x => x.id == "Lookups");
  heading: string;
  lookupName: string;
  LookupExtraDate: any;
  lookup: any = {};
  isSaving: boolean | number = false;
  isDeleting: boolean | number = false;
  Archives: any[];
  dateConvertor: DateConvertor;
  appInfo: any = {};

  @ViewChild('lookupForm') form;
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private httpService: HttpService,
    private lookupService: LookupService,
    private breadcrumbService: BreadcrumbService,
    private cryptoService: CryptoService,
    private confirmationDialogService: ConfirmationDialogService,
    private toastr: ToastrService,
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    }
  }

  ngOnInit() {
      let that = this;
    that.appInfo = that.httpService.getAppInfo(-9988);
      that.queryParams = that.route.snapshot.queryParams;
      that.dateConvertor = new DateConvertor();
      that.lookupName = that.cryptoService.decryptString(that.queryParams.lookupName);
      that.heading = that.queryParams.lookupId ? ('Edit ' + that.lookupName) : ('Add ' + that.lookupName);
      that.get();
      that.Archives = that.lookupService.getBooleanOptions();
      that.setBreadcrumbs();
  }

  get() {
    let that = this;
    that.httpService
      .get(that.lookups.api + (that.queryParams.lookupId ? that.queryParams.lookupId : '0') + '?lookupTypeId=' + that.queryParams.id + '&enclookupTypeId=' + that.queryParams.id )
      .subscribe(lookup => {
        that.lookup = lookup;
        that.LookupExtraDate = that.dateConvertor.fromModel(that.lookup.LookupExtraDate);
      });
  }

  submit(valid) {
    let that = this;
    if (valid && !that.appInfo.isUserReadonly) {
      that.isSaving = true;
      that.lookup.LookupExtraDate = that.dateConvertor.toModel(that.LookupExtraDate);
      that.lookup.EncryptedLookupTypeId = that.queryParams.id;
      that.queryParams.lookupId ? that.update() : that.save();
    }
    else {
        valid == true ? that.toastr.error('You cannot save your changes') : "";
    }
  }

  save() {
    let that = this;
    that.httpService
      .post(that.lookups.api, that.lookup)
      .subscribe(lookup => {
        that.form.control.markAsPristine();
        that.goBack(InfoMessage.SaveMessage);
      }, erorr => {
        that.isSaving = false;
      });
  }

  update() {
    let that = this;
    that.httpService
      .put(that.lookups.api + that.queryParams.lookupId, that.lookup)
      .subscribe(lookup => {
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
      .delete(that.lookups.api + that.queryParams.lookupId)
      .subscribe(lookup => {
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
      .navigate([that.lookupTypes.grid.routeInfo.route + '/' + that.lookups.grid.routeInfo.route], {
        queryParams: {
          id: that.queryParams.id,
          lookupName: that.queryParams.lookupName,
          label1: that.queryParams.label1,
          label2: that.queryParams.label2,
          label3: that.queryParams.label3,
          label4: that.queryParams.label4,
          localName: that.cryptoService.encryptString(that.lookups.grid.vwName)
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
      label: that.lookupTypes.grid.routeInfo.label,
      url: '/' + that.lookupTypes.grid.routeInfo.route,
      params: {
        localName: that.cryptoService.encryptString(that.lookupTypes.grid.vwName)
      }
    });
    breadcrumbs.push({
      label: that.lookupName,
      url: '/' + that.lookupTypes.grid.routeInfo.route + '/' + that.lookups.grid.routeInfo.route,
      params: {
        id: that.queryParams.id,
        lookupName: that.queryParams.lookupName,
        label1: that.queryParams.label1,
        label2: that.queryParams.label2,
        label3: that.queryParams.label3,
        label4: that.queryParams.label4,
        localName: that.cryptoService.encryptString(that.lookups.grid.vwName)
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

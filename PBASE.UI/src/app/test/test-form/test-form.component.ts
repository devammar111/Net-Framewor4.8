import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
//service
import { ConfirmationDialogService } from '../../shared/confirmation-dialog/confirmation-dialog.service';
import { BreadcrumbService } from '../../shared/breadcrumb/breadcrumb.service';
import { ToastrService } from 'ngx-toastr';
import { HttpService } from '../../shared/http.service';
import { CryptoService } from '../../shared/crypto/crypto.service';
//custom
import { Breadcrumb } from '../../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus, InfoMessage } from '../../shared/all-routes';
import { DateConvertor } from '../../shared/date-convertor';
import { StateManagementService } from '../../shared/state-management/state-management.service';

@Component({
  selector: 'app-test-form',
  templateUrl: './test-form.component.html',
  styleUrls: ['./test-form.component.scss']
})
export class TestFormComponent implements OnInit {
  heading: string;
  data: any = {};
  dateConvertor: DateConvertor;
  TestDate1: any;
  TestDate2: any;
  TestDate3: any;
  queryParams: any = {};
  isSaving: boolean = false;
  isDeleting: boolean = false;
  TestTags: any = [];
  TestTagsIds: any = [];
  savedTestTypeId: number = 0;
  appInfo: any = {};
  headerSectionInfo: any = {};
  dateSectionInfo: any = {};
  TestText2Info: any = {};
  TestText4Info: any = {};
  TestButton1Info: any = {};
  TestTab1Info: any = {};
  collapsed: any = [false,true, true, true];
  IsTest1: boolean = false;
  IsTest2: boolean = false;
  IsTest3: boolean = false;
  tests: Menu = pBMenus.find(x => x.id == "Tests");
  @ViewChild('testForm') form;

  constructor(
    private route: ActivatedRoute,
    private httpService: HttpService,
    private router: Router,
    private breadcrumbService: BreadcrumbService,
    private toastr: ToastrService,
    private cryptoService: CryptoService,
    private stateManagementService: StateManagementService,
    private confirmationDialogService: ConfirmationDialogService
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    }
  }

  ngOnInit() {
    let that = this;
    that.getInfo();
    that.dateConvertor = new DateConvertor();
    that.queryParams = that.route.snapshot.queryParams;
    that.heading = that.queryParams.testId ? that.tests.editForm.routeInfo.label : that.tests.addForm.routeInfo.label;
    that.get();
    that.setBreadcrumbs();
  }

  ngOnDestroy() {
    let that = this;
    that.stateManagementService.save(that.tests.id, that.collapsed);
  }

  getInfo() {
    let that = this;
    that.appInfo = this.httpService.getAppInfo(-9962);
    that.headerSectionInfo = this.httpService.getAppInfo(-9958);
    that.dateSectionInfo = this.httpService.getAppInfo(-9957);
    that.TestText2Info = this.httpService.getAppInfo(-9956);
    that.TestText4Info = this.httpService.getAppInfo(-9955);
    that.TestButton1Info = this.httpService.getAppInfo(-9954);
    that.TestTab1Info = this.httpService.getAppInfo(-9961);
  }

  get() {
    let that = this;
    that.httpService
        .get(that.tests.api + (that.queryParams.testId ? that.queryParams.testId : '0'))
      .subscribe((res: any) => {
        that.data = res;
        that.data.vw_LookupTestType = (that.queryParams.testId ? that.data.vw_LookupTestType : that.data.vw_LookupTestType.filter(x => x.GroupBy === "ACTIVE"));
        that.collapsed = that.stateManagementService.set(that.tests.id, that.collapsed);
        that.TestDate1 = that.dateConvertor.fromModel(that.data.TestDate1);
          that.TestDate2 = that.dateConvertor.fromModel(that.data.TestDate2);
          that.TestDate3 = that.dateConvertor.fromModel(that.data.TestDate3);
          that.IsTest1 = that.data.IsTest1;
          that.IsTest2 = that.data.IsTest2;
          that.IsTest3 = that.data.IsTest3;

      });
  }

  submit(valid: boolean) {
    let that = this;
    if (valid && !that.appInfo.isUserReadonly) {
      that.isSaving = true;
      that.data.TestDate1 = that.dateConvertor.toModel(that.TestDate1);
      that.data.TestDate2 = that.dateConvertor.toModel(that.TestDate2);
      that.data.TestDate3 = that.dateConvertor.toModel(that.TestDate3);
      that.queryParams.testId ? that.update() : that.save();
    }
    else {
      valid == true ? that.toastr.error('You cannot save your changes') : "";
    }
  }

  save() {
    let that = this;
    that.httpService
      .post(that.tests.api, that.data)
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
        .put(that.tests.api + that.queryParams.testId, that.data)
      .subscribe(test => {
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
      .delete(that.tests.api + that.queryParams.testId)
      .subscribe(test => {
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
      .navigate(['/' + that.tests.grid.routeInfo.route], {
        queryParams: {
          localName: that.cryptoService.encryptString(that.tests.grid.vwName)
        }
      }).then(() => {
        if (message) {
          that.toastr.success(message);
        }
      });
  }

  onIsTest1(value) {
      this.IsTest1 = value;
  }

  onIsTest2(value) {
      this.IsTest2 = value;
  }

  onIsTest3(value) {
      this.IsTest3 = value;
  }
  setBreadcrumbs() {
    let that = this;
    let breadcrumbs: Breadcrumb[] = [];
    breadcrumbs.push({
      label: that.tests.grid.routeInfo.label,
      url: '/' + that.tests.grid.routeInfo.route,
      params: {
        localName: that.cryptoService.encryptString(that.tests.grid.vwName)
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

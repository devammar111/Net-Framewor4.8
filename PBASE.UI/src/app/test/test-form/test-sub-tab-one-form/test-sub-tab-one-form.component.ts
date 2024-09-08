import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
//service
import { ConfirmationDialogService } from '../../../shared/confirmation-dialog/confirmation-dialog.service';
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { ToastrService } from 'ngx-toastr';
import { HttpService } from '../../../shared/http.service';
import { CryptoService } from '../../../shared/crypto/crypto.service';
import { NgbModal, NgbModalOptions, NgbDatepickerConfig } from '@ng-bootstrap/ng-bootstrap';
//custom
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus, InfoMessage } from '../../../shared/all-routes';
import { DateConvertor } from '../../../shared/date-convertor';
import { StateManagementService } from 'src/app/shared/state-management/state-management.service';

@Component({
   selector: 'app-test-sub-tab-one-form',
   templateUrl: './test-sub-tab-one-form.component.html',
  styles: []
})
export class TestSubTabOneFormComponent implements OnInit {

    heading: string;
    data: any = {};
    dateConvertor: DateConvertor;
    TestSubDate1: any;
    TestSubDate2: any;
    TestSubDate3: any;
    queryParams: any = {};
    isSaving: boolean = false;
    isDeleting: boolean = false;
    TestTags: any = [];
    TestTagsIds: any = [];
    savedTestTypeId: number = 0;
  appInfo: any = {};
  TestText4Info: any = {};
  collapsed: any = [false, true, true];
    IsTestSub1: boolean = false;
    IsTestSub2: boolean = false;
    IsTestSub3: boolean = false;

    tests: Menu = pBMenus.find(x => x.id == "Tests");
   testssSubTabOne: Menu = pBMenus.find(x => x.id == "TestsSubTabOne");
   @ViewChild('testsSubTabOne') form;

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
        that.heading = that.queryParams.id ? that.testssSubTabOne.editForm.routeInfo.label : that.testssSubTabOne.addForm.routeInfo.label;
        that.get();
        that.setBreadcrumbs();
  }

  ngOnDestroy() {
    let that = this;
    that.stateManagementService.save(that.testssSubTabOne.id, that.collapsed);
  }

  getInfo() {
    let that = this;
    that.appInfo = this.httpService.getAppInfo(-9960);
    that.TestText4Info = this.httpService.getAppInfo(-9955);
  }

    get() {
        let that = this;
        that.httpService
            .get(that.testssSubTabOne.api + (that.queryParams.id ? that.queryParams.id : '0'))
            .subscribe((res: any) => {
              that.data = res;
              that.data.vw_LookupTestType = (that.queryParams.id ? that.data.vw_LookupTestType : that.data.vw_LookupTestType.filter(x => x.GroupBy === "ACTIVE"));
              that.collapsed = that.stateManagementService.set(that.testssSubTabOne.id, that.collapsed);
              that.TestSubDate1 = that.dateConvertor.fromModel(that.data.TestSubDate1);
                that.TestSubDate2 = that.dateConvertor.fromModel(that.data.TestSubDate2);
                that.TestSubDate3 = that.dateConvertor.fromModel(that.data.TestSubDate3);
                that.IsTestSub1 = that.data.IsTestSub1;
                that.IsTestSub2 = that.data.IsTestSub2;
                that.IsTestSub3 = that.data.IsTestSub3;

            });
    }

    submit(valid: boolean) {
      let that = this;
      if (valid && !that.appInfo.isUserReadonly) {
        that.isSaving = true;
        that.data.TestSubDate1 = that.dateConvertor.toModel(that.TestSubDate1);
        that.data.TestSubDate2 = that.dateConvertor.toModel(that.TestSubDate2);
        that.data.TestSubDate3 = that.dateConvertor.toModel(that.TestSubDate3);
        that.queryParams.id ? that.update() : that.save();
      }
      else {
        valid == true ? that.toastr.error('You cannot save your changes') : "";
      }
    }

    save() {
        let that = this;
        that.data.EncTestId = that.queryParams.testId;
        that.httpService
            .post(that.testssSubTabOne.api, that.data)
            .subscribe(id => {
                //that.form.control.markAsPristine();
                that.goBack(InfoMessage.SaveMessage);
            }, erorr => {
                that.isSaving = false;
            });
    }

    update() {
        let that = this;
        that.httpService
            .put(that.testssSubTabOne.api + that.queryParams.id, that.data)
            .subscribe(test => {
                //that.form.control.markAsPristine();
                that.goBack(InfoMessage.SaveMessage);
            }, erorr => {
                that.isSaving = false;
            });
    }

    delete() {
        let that = this;
        that.isDeleting = true;
        that.httpService
            .delete(that.testssSubTabOne.api + that.queryParams.id)
            .subscribe(test => {
                //that.form.control.markAsPristine();
                that.goBack(InfoMessage.DeleteMessage);
            }, erorr => {
                that.isDeleting = false;
            });
    }

    goBack(message?: string) {
        let that = this;
        that.router.navigated = false;
        that.router
            .navigate(['/' + that.testssSubTabOne.grid.routeInfo.route], {
                queryParams: {
                    testId: that.queryParams.testId,
                    localName: that.cryptoService.encryptString(that.testssSubTabOne.grid.vwName)
                }
            }).then(() => {
                if (message) {
                    that.toastr.success(message);
                }
            });
    }

    onIsTest1(value) {
        this.IsTestSub1 = value;
    }

    onIsTest2(value) {
        this.IsTestSub2 = value;
    }

    onIsTest3(value) {
        this.IsTestSub3 = value;
    }
    setBreadcrumbs() {
        let that = this;
        let breadcrumbs: Breadcrumb[] = [];
        breadcrumbs.push({
            label: that.tests.grid.routeInfo.label,
            url: '/' + that.tests.grid.routeInfo.route,
            params: {
                testId: that.queryParams.testId,
                ref: that.queryParams.ref,
                localName: that.cryptoService.encryptString(that.tests.grid.vwName)
            }
        });
        breadcrumbs.push({
            label: that.testssSubTabOne.grid.routeInfo.label,
            url: '/' + that.testssSubTabOne.grid.routeInfo.route,
            params: {
                testId: that.queryParams.testId,
                localName: that.cryptoService.encryptString(that.testssSubTabOne.grid.vwName)
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

import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
//service
import { ConfirmationDialogService } from '../../shared/confirmation-dialog/confirmation-dialog.service';
import { BreadcrumbService } from '../../shared/breadcrumb/breadcrumb.service';
import { ToastrService } from 'ngx-toastr';
import { HttpService } from '../../shared/http.service';
import { CryptoService } from '../../shared/crypto/crypto.service';
import { NgbModal, NgbModalOptions, NgbDatepickerConfig } from '@ng-bootstrap/ng-bootstrap';
//custom
import { Breadcrumb } from '../../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus, InfoMessage } from '../../shared/all-routes';
import { DateConvertor } from '../../shared/date-convertor';

@Component({
  selector: 'app-note-test-form',
  templateUrl: './note-test-form.component.html',
  styleUrls: ['./note-test-form.component.css']
})
export class NoteTestFormComponent implements OnInit {

    heading: string;
    data: any = {};
    dateConvertor: DateConvertor;
    queryParams: any = {};
    isSaving: boolean = false;
    isDeleting: boolean = false;
    TestTags: any = [];
    TestTagsIds: any = [];
    savedTestTypeId: number = 0;
    appInfo: any = {};
    public isCollapsed = false;
    public isCollapsed1 = false;
    public isCollapsed2 = false;

    tests: Menu = pBMenus.find(x => x.id == "Tests");
    testsSub: Menu = pBMenus.find(x => x.id == "TestsNote");
    @ViewChild('testSubForm') form;

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
        that.dateConvertor = new DateConvertor();
      that.appInfo = that.httpService.getAppInfo(-9959);
        that.queryParams = that.route.snapshot.queryParams;
        that.heading = that.queryParams.id ? that.testsSub.editForm.routeInfo.label : that.testsSub.addForm.routeInfo.label;
        that.get();
        that.setBreadcrumbs();
    }

  get() {
    let that = this;
    that.httpService
      .get(that.testsSub.api + (that.queryParams.id ? that.queryParams.id : '0'))
      .subscribe((res: any) => {
        that.data = res;
        that.data.vw_LookupTestType = (that.queryParams.id ? that.data.vw_LookupTestType : that.data.vw_LookupTestType.filter(x => x.GroupBy === "ACTIVE"));
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
        that.data.EncTestId = that.queryParams.testId;
        that.httpService
            .post(that.testsSub.api, that.data)
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
            .put(that.testsSub.api + that.queryParams.id, that.data)
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
            .delete(that.testsSub.api + that.queryParams.id)
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
            .navigate(['/' + that.testsSub.grid.routeInfo.route], {
                queryParams: {
                    testId: that.queryParams.testId,
                    localName: that.cryptoService.encryptString(that.testsSub.grid.vwName)
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
            label: that.tests.grid.routeInfo.label,
            url: '/' + that.tests.grid.routeInfo.route,
            params: {
                testId: that.queryParams.testId,
                ref: that.queryParams.ref,
                localName: that.cryptoService.encryptString(that.tests.grid.vwName)
            }
        });
        breadcrumbs.push({
            label: that.testsSub.grid.routeInfo.label,
            url: '/' + that.testsSub.grid.routeInfo.route,
            params: {
                testId: that.queryParams.testId,
                localName: that.cryptoService.encryptString(that.testsSub.grid.vwName)
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

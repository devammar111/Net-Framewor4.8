/// <reference path="../../shared/all-routes.ts" />
/// <reference path="../../shared/ag-grid/ag-grid-helper.ts" />
import { Component, ViewChild, OnInit, AfterViewInit, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
//service
import { BreadcrumbService } from '../../shared/breadcrumb/breadcrumb.service';
import { LookupService } from '../../shared/lookup.service';
//custom
import { AgGridSource } from '../../shared/ag-grid/ag-grid-source';
import { AgGridHelper } from '../../shared/ag-grid/ag-grid-helper';
import { Breadcrumb } from '../../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus } from '../../shared/all-routes';
import { CryptoService } from 'src/app/shared/crypto/crypto.service';
import { forkJoin } from 'rxjs';
import { HttpService } from 'src/app/shared/http.service';
import { ACCESSTYPES } from 'src/app/shared/access-types';
import { ToastrService } from 'ngx-toastr';
import { StateManagementService } from 'src/app/shared/state-management/state-management.service';
declare var agGrid: any;

@Component({
  selector: 'app-test-grid',
  templateUrl: './test-grid.component.html',
  styleUrls: ['./test-grid.component.scss']
})
export class TestGridComponent implements OnInit {
  gridOptions: any = {};
  queryParams: any = {};
  appInfo: any = {};
  tab1Info: any = {};
  tab2Info: any = {};
  tab3Info: any = {};
  tests: Menu = pBMenus.find(x => x.id == "Tests");
  @ViewChild('vw_TestGrid') vw_TestGrid: ElementRef;

  constructor(
    private dataSource: AgGridSource,
    private router: Router,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private cryptoService: CryptoService,
    private lookupService: LookupService,
    private httpService: HttpService,
    private stateManagementService: StateManagementService,
    private readonly breadcrumbService: BreadcrumbService
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    }
  }

   ngOnInit() {
      let that = this;
      that.removeStateLocalStroage();
      that.queryParams = that.route.snapshot.queryParams;
      that.gridOptions.GridLabel = that.tests.grid.routeInfo.label;
      that.dataSource.queryName = that.cryptoService.decryptString(that.queryParams.localName);
      that.dataSource.init(that.tests.grid.vwName, that.gridOptions, 'TestId', 'desc', '', '');
      that.gridOptions.context = { gridComponent: that };
      that.getInfo();
      if (!that.appInfo.isReadonly && that.tab1Info.show) {
         that.gridOptions.addCallBack = function () {
            that.navigate();
         };
      }
      that.gridOptions.onGridReady = (params) => {
         that.gridReady(params);
      }
      new agGrid.Grid(that.vw_TestGrid.nativeElement, that.gridOptions);
      that.setBreadcrumbs();
   }

     private gridReady(params?: any) {
        let that = this;
        let testTypes = that.lookupService.getGridTestType();
        let testTypes1 = that.lookupService.getGridTestType();
        let testTypes2 = that.lookupService.getGridTestType();


        forkJoin([testTypes, testTypes1, testTypes2])
          .subscribe((results: any) => {

              let testTypes = results[0].map(function (item) {
                  return item['TestTypeDisplay'];
              });
              let testTypes1 = results[1].map(function (item) {
                  return item['TestTypeDisplay'];
              });
              let testTypes2 = results[2].map(function (item) {
                  return item['TestTypeDisplay'];
              });


              let colDefs = that.createColumnDefs(testTypes, testTypes1, testTypes2);
              that.dataSource.restoreColumnDefs(colDefs);
              
          });
    }

  removeStateLocalStroage() {
    let that = this;
    that.stateManagementService.remove(that.tests.id);
    that.stateManagementService.remove("TestsSub");
     that.stateManagementService.remove("TestssSubTabOne");
  }

  getInfo() {
    let that = this;
    that.appInfo = this.httpService.getAppInfo(-9962);
    that.tab1Info = that.httpService.getAppInfo(-9961);
    that.tab2Info = that.httpService.getAppInfo(-9960);
    that.tab3Info = that.httpService.getAppInfo(-9959);
  }

  private createColumnDefs(testTypes, testTypes1, testTypes2) {
    let that = this;
    let colums: any = [];
    if (that.tab1Info.show || that.tab2Info.show || that.tab3Info.show) {
      colums.push(AgGridHelper.actionTemplate('Action', 'Action', { cellRendererFramework: that.actionCellRenderer }));
    }
    colums.push(AgGridHelper.numberTemplate('Test Id', 'TestId', { hide: true }));
    colums.push(AgGridHelper.stringTemplate('Test Text 1', 'TestText1'));
    colums.push(AgGridHelper.stringTemplate('Test Text 2', 'TestText2'));
    colums.push(AgGridHelper.stringTemplate('Test Text 3', 'TestText3'));
    colums.push(AgGridHelper.dateTemplate('Test Date 1', 'TestDate1'));
    colums.push(AgGridHelper.dateTemplate('Test Date 2', 'TestDate2'));
    colums.push(AgGridHelper.dateTemplate('Test Date 3', 'TestDate3'));
    colums.push(AgGridHelper.dropdownTemplate('Test Type 1', 'TestType1', { items: testTypes }));
    colums.push(AgGridHelper.dropdownTemplate('Test Type 2', 'TestType2', { items: testTypes1 }));
    colums.push(AgGridHelper.dropdownTemplate('Test Type 3', 'TestType3', { items: testTypes2 }));
    colums.push(AgGridHelper.booleanTemplate('Test 1?', 'IsTest1'));
    colums.push(AgGridHelper.booleanTemplate('Test 2?', 'IsTest2'));
    colums.push(AgGridHelper.booleanTemplate('Test 3?', 'IsTest3'));
    colums.push(AgGridHelper.stringTemplate('Created By', 'CreatedUser'));
    colums.push(AgGridHelper.dateTemplate('Created Date', 'CreatedDate'));
    colums.push(AgGridHelper.stringTemplate('Updated By', 'UpdatedUser'));
    colums.push(AgGridHelper.dateTemplate('Updated Date', 'UpdatedDate'));
    return colums;
  }

  actionCellRenderer(params: any) {
    let div: HTMLDivElement = AgGridHelper.createDiv();
    let link: HTMLAnchorElement = AgGridHelper.createLink('fa-pencil-alt', 'Edit');
    if (params.data) {
      link.onclick = function () {
        params.context.gridComponent.navigate(params.data.EncryptedTestId);
      };
      div.appendChild(link);
      return div;
    }
  }

  navigate(id?: string) {
    var that = this;
    if (id) {
      if (that.tab1Info.show) {
        that.router.navigate(['/' + that.tests.grid.routeInfo.route + '/' + that.tests.editForm.routeInfo.route], {
          queryParams: {
            testId: id,
          }
        });
      }
      else if (that.tab2Info.show) {
        let testsSub: Menu = pBMenus.find(x => x.id == "TestsSub");
        that.router.navigate(['/' + testsSub.grid.routeInfo.route], {
          queryParams: {
            testId: id,
          }
        });
      }
      else if (that.tab3Info.show) {
        let testsNote: Menu = pBMenus.find(x => x.id == "TestsNote");
        that.router.navigate(['/' + testsNote.grid.routeInfo.route], {
          queryParams: {
            testId: id,
          }
        });
      }
    }
    else {
      that.router.navigate(['/' + that.tests.grid.routeInfo.route + '/' + that.tests.addForm.routeInfo.route]);
    }
  }

   reload() {
      let that = this;
      that.gridReady();
   }

  setBreadcrumbs() {
    let that = this;
    let breadcrumbs: Breadcrumb[] = [];
    breadcrumbs.push({
      label: that.tests.grid.routeInfo.label,
      url: '',
      params: {}
    });
    that.breadcrumbService.set(breadcrumbs);
  }
}

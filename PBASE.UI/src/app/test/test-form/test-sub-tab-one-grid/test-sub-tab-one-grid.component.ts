import { Component, ViewChild, OnInit, AfterViewInit, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
//service
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { LookupService } from '../../../shared/lookup.service';
//custom
import { AgGridSource } from '../../../shared/ag-grid/ag-grid-source';
import { AgGridHelper } from '../../../shared/ag-grid/ag-grid-helper';
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus } from '../../../shared/all-routes';
import { CryptoService } from 'src/app/shared/crypto/crypto.service';
import { forkJoin } from 'rxjs';
import { HttpService } from 'src/app/shared/http.service';
declare var agGrid: any;

@Component({
   selector: 'app-test-sub-tab-one-grid',
   templateUrl: './test-sub-tab-one-grid.component.html',
  styles: []
})
export class TestSubTabOneGridComponent implements OnInit {
    gridOptions: any = {};
    queryParams: any = {};
   collapsed: any = [true];

    tests: Menu = pBMenus.find(x => x.id == "Tests");
    testssSubTabOne: Menu = pBMenus.find(x => x.id == "TestsSubTabOne");
   @ViewChild('vw_TestSubGrid') vw_TestSubGrid: ElementRef;

    constructor(
        private dataSource: AgGridSource,
        private router: Router,
        private route: ActivatedRoute,
        private cryptoService: CryptoService,
      private lookupService: LookupService,
      private httpService: HttpService,
        private readonly breadcrumbService: BreadcrumbService
    ) {
        this.router.routeReuseStrategy.shouldReuseRoute = function () {
            return false;
        }
    }

    ngOnInit() {
        let that = this;
        that.queryParams = that.route.snapshot.queryParams;
        that.gridOptions.GridLabel = that.testssSubTabOne.grid.routeInfo.label;
        that.dataSource.queryName = that.cryptoService.decryptString(that.queryParams.localName);
        that.dataSource.init(that.testssSubTabOne.grid.vwName, that.gridOptions, 'TestSubId', 'desc', 'TestId', that.queryParams.testId ? that.queryParams.testId : '0');
        that.gridOptions.context = { gridComponent: that };
      let tab1Info = that.httpService.getAppInfo(-9960);
      if (!tab1Info.isReadonly) {
        that.gridOptions.addCallBack = function () {
          that.navigate();
        };
      }
       that.gridOptions.onGridReady = (params) => {
          that.gridReady(params);
       }
       new agGrid.Grid(that.vw_TestSubGrid.nativeElement, that.gridOptions);
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
       

    private createColumnDefs(testTypes, testTypes1, testTypes2) {
        let that = this;
        return [
            AgGridHelper.actionTemplate('Action', 'Action', { cellRendererFramework: that.actionCellRenderer }),
            AgGridHelper.numberTemplate('Test Sub Id', 'TestSubId', { hide: true }),
            AgGridHelper.numberTemplate('Test Id', 'TestId', { hide: true }),
            AgGridHelper.stringTemplate('Test Sub Text 1', 'TestSubText1'),
            AgGridHelper.stringTemplate('Test Sub Text 2', 'TestSubText2'),
            AgGridHelper.stringTemplate('Test Sub Text 3', 'TestSubText3'),
            AgGridHelper.dateTemplate('Test Sub Date 1', 'TestSubDate1'),
            AgGridHelper.dateTemplate('Test Sub Date 2', 'TestSubDate2'),
            AgGridHelper.dateTemplate('Test Sub Date 3', 'TestSubDate3'),
            AgGridHelper.booleanTemplate('Sub Test 1?', 'IsTestSub1'),
            AgGridHelper.booleanTemplate('Sub Test 2', 'IsTestSub2'),
            AgGridHelper.booleanTemplate('Sub Test 3', 'IsTestSub3'),
            AgGridHelper.stringTemplate('Created By', 'CreatedUser'),
            AgGridHelper.dateTemplate('Created Date', 'CreatedDate'),
            AgGridHelper.stringTemplate('Updated By', 'UpdatedUser'),
            AgGridHelper.dateTemplate('Updated Date', 'UpdatedDate'),
        ];
    }

    actionCellRenderer(params: any) {
        let div: HTMLDivElement = AgGridHelper.createDiv();
      let link: HTMLAnchorElement = AgGridHelper.createLink('fa-pencil-alt', 'Edit');
        if (params.data) {
            link.onclick = function () {
                params.context.gridComponent.navigate(params.data.EncryptedTestSubId);
            };
            div.appendChild(link);
            return div;
        }
    }

    navigate(id?: string) {
        var that = this;
        if (id) {
            that.router.navigate(['/' + that.testssSubTabOne.grid.routeInfo.route + '/' + that.testssSubTabOne.editForm.routeInfo.route], {
                queryParams: {
                    id: id,
                    testId: that.queryParams.testId,
                }
            });
        }
        else {
            that.router.navigate(['/' + that.testssSubTabOne.grid.routeInfo.route + '/' + that.testssSubTabOne.addForm.routeInfo.route], {
                queryParams: {
                    testId: that.queryParams.testId,
                }
            });
        }
    }

   reload() {
      let that = this;
      that.gridReady();
   }

}

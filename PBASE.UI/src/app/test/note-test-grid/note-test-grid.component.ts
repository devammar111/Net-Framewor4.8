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
declare var agGrid: any;

@Component({
  selector: 'app-note-test-grid',
  templateUrl: './note-test-grid.component.html',
  styles: []
})
export class NoteTestGridComponent implements OnInit {

    gridOptions: any = {};
    queryParams: any = {};
    tests: Menu = pBMenus.find(x => x.id == "Tests");
    testsnote: Menu = pBMenus.find(x => x.id == "TestsNote");
   @ViewChild('vw_TestNoteGrid') vw_TestNoteGrid: ElementRef;

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
        that.gridOptions.GridLabel = that.testsnote.grid.routeInfo.label;
        that.dataSource.queryName = that.cryptoService.decryptString(that.queryParams.localName);
        that.dataSource.init(that.testsnote.grid.vwName, that.gridOptions, 'TestNoteId', 'desc', 'TestId', that.queryParams.testId ? that.queryParams.testId : '0');
        that.gridOptions.context = { gridComponent: that };
      let appInfo = this.httpService.getAppInfo(-9959);
      if (!appInfo.isReadonly) {
        that.gridOptions.addCallBack = function () {
          that.navigate();
        };
       }
       that.gridOptions.onGridReady = (params) => {
          that.gridReady(params);
       }
       new agGrid.Grid(that.vw_TestNoteGrid.nativeElement, that.gridOptions);
       that.setBreadcrumbs();
   }

   private gridReady(params?: any) {
      let that = this;
            let testTypes = that.lookupService.getGridTestType();
            forkJoin([testTypes])
                .subscribe((results: any) => {

                    let testTypes = results[0].map(function (item) {
                        return item['TestTypeDisplay'];
                    });

                    let colDefs = that.createColumnDefs(testTypes);
                    that.dataSource.restoreColumnDefs(colDefs);
                    
                });
        }


    private createColumnDefs(testTypes) {
        let that = this;
        return [
            AgGridHelper.actionTemplate('Action', 'Action', { cellRendererFramework: that.actionCellRenderer }),
            AgGridHelper.numberTemplate('Test Sub Id', 'TestNoteId', { hide: true }),
            AgGridHelper.numberTemplate('Test Id', 'TestId', { hide: true }),
            AgGridHelper.dropdownTemplate('Test Note Type', 'TestNoteType', { items: testTypes }),
            AgGridHelper.stringTemplate('Test Note', 'TestNoteText'),
            AgGridHelper.stringTemplate('Created By', 'CreatedBy'),
            AgGridHelper.dateTemplate('Created Date', 'CreatedDate'),
        ];
    }

    actionCellRenderer(params: any) {
        let div: HTMLDivElement = AgGridHelper.createDiv();
      let link: HTMLAnchorElement = AgGridHelper.createLink('fa-pencil-alt', 'Edit');
        if (params.data) {
            link.onclick = function () {
                params.context.gridComponent.navigate(params.data.EncryptedTestNoteId);
            };
            div.appendChild(link);
            return div;
        }
    }

    navigate(id?: string) {
        var that = this;
        if (id) {
            that.router.navigate(['/' + that.testsnote.grid.routeInfo.route + '/' + that.testsnote.editForm.routeInfo.route], {
                queryParams: {
                    id: id,
                    testId: that.queryParams.testId,
                }
            });
        }
        else {
            that.router.navigate(['/' + that.testsnote.grid.routeInfo.route + '/' + that.testsnote.addForm.routeInfo.route], {
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
            label: that.testsnote.grid.routeInfo.label,
            url: '',
            params: {}
        });
        that.breadcrumbService.set(breadcrumbs);
    }


}

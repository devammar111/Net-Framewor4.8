import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { AgGridHelper } from '../../../../shared/ag-grid/ag-grid-helper';
import { BreadcrumbService } from '../../../../shared/breadcrumb/breadcrumb.service';
import { LookupService } from '../../../../shared/lookup.service';
import { AgGridSource } from '../../../../shared/ag-grid/ag-grid-source';
import { Router, ActivatedRoute } from '@angular/router';
import { pBMenus, Menu } from 'src/app/shared/all-routes';
import { CryptoService } from 'src/app/shared/crypto/crypto.service';
declare var agGrid: any;

@Component({
  selector: 'app-agreement-user-list',
  templateUrl: './agreement-user-list.component.html',
  styles: []
})
export class AgreementUserListComponent implements OnInit {
  AgreementId: string;
  gridOptions: any = {};
  queryParams: any = {};
  @ViewChild('vw_AgreementUserSubGrid') vw_AgreementUserSubGrid: ElementRef
  agreement: Menu = pBMenus.find(x => x.id == "AgreementUser");
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private breadcrumbService: BreadcrumbService,
    private lookupService: LookupService,
    private cryptoService: CryptoService,
    private dataSource: AgGridSource) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    }

  }

  ngOnInit() {
    let that = this;
    that.queryParams = that.route.snapshot.queryParams;
    that.gridOptions.GridLabel = that.agreement.grid.routeInfo.label;
    that.dataSource.queryName = that.cryptoService.decryptString(that.queryParams.localName);
    that.dataSource.init(that.agreement.grid.vwName, that.gridOptions, 'AgreementId', 'desc', 'AgreementId', that.queryParams.id);
    that.gridOptions.context = { gridComponent: that };
      that.gridOptions.onGridReady = (params) => {
         that.gridReady(params)
      }
      new agGrid.Grid(that.vw_AgreementUserSubGrid.nativeElement, that.gridOptions);
   }

   gridReady(params?) {
      var that = this;
      let colDefs = that.createColumnDefs();
      that.dataSource.restoreColumnDefs(colDefs);

   }

  createColumnDefs() {
    return [
      AgGridHelper.numberTemplate('Unique Id', 'UniqueId', { hide: true }),
      AgGridHelper.numberTemplate('Agreement Id', 'AgreementId', { hide: true }),
      AgGridHelper.numberTemplate('User Id', 'UserId', { hide: true }),
      AgGridHelper.numberTemplate('User Agreement Id', 'UserAgreementId', { hide: true }),
      AgGridHelper.stringTemplate('User', 'UserFullName'),
      AgGridHelper.booleanTemplate('Accepted', 'IsAcceptDecline'),
      AgGridHelper.dateTimeTemplate('Response Date', 'AcceptDeclineDate'),

    ];
  }

   reload() {
      let that = this;
      that.gridReady();
   }

}

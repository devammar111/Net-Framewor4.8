import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { AgGridHelper } from '../../../shared/ag-grid/ag-grid-helper';
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { LookupService } from '../../../shared/lookup.service';
import { AgGridSource } from '../../../shared/ag-grid/ag-grid-source';
import { Router, ActivatedRoute } from '@angular/router';
import { forkJoin } from 'rxjs';
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { pBMenus, Menu } from 'src/app/shared/all-routes';
import { CryptoService } from 'src/app/shared/crypto/crypto.service';
import { HttpService } from 'src/app/shared/http.service';
declare var agGrid: any;

@Component({
  selector: 'app-system-alerts-list',
  templateUrl: './system-alerts-list.component.html',
  styles: []
})
export class SystemAlertsListComponent implements OnInit {

  gridOptions: any = {};
  queryParams: any = {};
  @ViewChild('vw_SystemAlertGrid') vw_SystemAlertGrid: ElementRef
  SystemAlerts: Menu = pBMenus.find(x => x.id == "SystemAlerts");
  api: string = 'vw_SystemAlertGrid/';
  isCollapsed = false;
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private breadcrumbService: BreadcrumbService,
    private lookupService: LookupService,
    private cryptoService: CryptoService,
    private httpService: HttpService,
    private dataSource: AgGridSource) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    }

  }

  ngOnInit() {
    let that = this;
    that.queryParams = that.route.snapshot.queryParams;
    that.gridOptions.GridLabel = that.SystemAlerts.grid.routeInfo.label;
    that.dataSource.queryName = that.cryptoService.decryptString(that.queryParams.localName);
    that.dataSource.init(that.SystemAlerts.grid.vwName, that.gridOptions, 'SystemAlertId', 'desc', '', '');
    that.gridOptions.GridLabel = "SystemAlert";
    that.dataSource.init('vw_SystemAlertGrid', that.gridOptions, 'SystemAlertId', 'desc', '', '');
    that.gridOptions.context = { gridComponent: that };
    let appInfo = this.httpService.getAppInfo(-9979);
    if (!appInfo.isReadonly) {
      that.gridOptions.addCallBack = function () {
        that.navigate();
      };
    }
      that.gridOptions.onGridReady = (params) => {
         that.gridReady(params);
      }
      new agGrid.Grid(that.vw_SystemAlertGrid.nativeElement, that.gridOptions);
      that.setBreadcrumbs();
   }

   gridReady(params?) {
      var that = this;
      let AlertType = that.lookupService.getGridAlertTypes();
      forkJoin([AlertType])
         .subscribe((results: any) => {
            let AlertType = results[0].map(function (item) {
               return item['AlertType'];
            });
            let colDefs = that.createColumnDefs(AlertType);
            that.dataSource.restoreColumnDefs(colDefs);

         });
   }

  createColumnDefs(AlertType) {
    return [
      AgGridHelper.actionTemplate('Action', 'Action', { cellRendererFramework: this.renderActionLink }),
      AgGridHelper.numberTemplate('System Alert Id', 'SystemAlertId', { hide: true }),
      AgGridHelper.dropdownTemplate('Alert Type', 'AlertType', { items: AlertType }),
      AgGridHelper.dateTimeTemplate('Active/Close Time', 'ClosedDateTime'),
      AgGridHelper.dateTimeTemplate('Deactive/Open Time', 'OpenDateTime'),
      AgGridHelper.numberTemplate('Warning Time', 'WarningTime'),
      AgGridHelper.numberTemplate('Open Message Time', 'OpenMessageTime'),

    ];
  }

  renderActionLink(params: any) {
    let div: HTMLDivElement = document.createElement('div');
    let link: HTMLAnchorElement = document.createElement('a');
    div.className = "text-center";
    link.className = "anchor-color";
    link.innerHTML = "<i class='fa fa-pencil-alt' title='Edit'></i></a>";
    link.onclick = function () {
      params.context.gridComponent.navigate(params.data.EncryptedSystemAlertId);
    };
    div.appendChild(link);
    return div;
  }

  navigate(id?: string) {
    var that = this;
    if (id) {
      that.router.navigate(['/' + that.SystemAlerts.grid.routeInfo.route + '/' + that.SystemAlerts.editForm.routeInfo.route], {
        queryParams: {
          id: id,
        }
      });
    }
    else {
      that.router.navigate(['/' + that.SystemAlerts.grid.routeInfo.route + '/' + that.SystemAlerts.addForm.routeInfo.route]);
    }
  }

   reload() {
      let that = this;
      that.gridReady();
   }
  setBreadcrumbs() {
    let that = this;
    let breadcrumbs: Breadcrumb[] = [];
    breadcrumbs.push({ label: 'System Alerts', url: '', params: {} });
    that.breadcrumbService.set(breadcrumbs);
  }

}

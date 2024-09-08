import { Component, ViewChild, OnInit, AfterViewInit, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
//service
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
//custom
import { AgGridSource } from '../../../shared/ag-grid/ag-grid-source';
import { AgGridHelper } from '../../../shared/ag-grid/ag-grid-helper';
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus } from '../../../shared/all-routes';
declare var agGrid: any;

@Component({
  selector: 'app-export-log-grid',
  templateUrl: './export-log-grid.component.html',
  styleUrls: ['./export-log-grid.component.scss']
})
export class ExportLogGridComponent implements OnInit {
  gridOptions: any = {};
  exportLogs: Menu = pBMenus.find(x => x.id == "ExportLog");
  @ViewChild('vw_ExportLogGrid') vw_ExportLogGrid: ElementRef;
  constructor(private dataSource: AgGridSource,
    private router: Router,
    private readonly breadcrumbService: BreadcrumbService) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    }
  }

  ngOnInit() {
    let that = this;
    that.gridOptions.GridLabel = that.exportLogs.grid.routeInfo.label;
    that.dataSource.init(that.exportLogs.grid.vwName, that.gridOptions, 'UserExportLogId', 'desc', '', '');
    that.gridOptions.context = { gridComponent: that };
      that.gridOptions.onGridReady = (params) => {
         that.gridReady(params);
      }
      new agGrid.Grid(that.vw_ExportLogGrid.nativeElement, that.gridOptions);
      that.setBreadcrumbs();
   }

   gridReady(params?) {
      var that = this;
      let colDefs = that.createColumnDefs();
      that.dataSource.restoreColumnDefs(colDefs);

   }

  private createColumnDefs() {
    return [
      AgGridHelper.numberTemplate('User Export Log Id', 'UserExportLogId', { hide: true }),
      AgGridHelper.stringTemplate('User', 'UserFullName'),
      AgGridHelper.stringTemplate('Menu Option', 'ApplicationOption'),
      AgGridHelper.stringTemplate('Data Source', 'DataSource'),
      AgGridHelper.stringTemplate('Saved Filter Name', 'SavedFilterName'),
      AgGridHelper.stringTemplate('Applied Filter', 'Filter'),
      AgGridHelper.stringTemplate('Export File Name', 'ExportFileName'),
      AgGridHelper.dateTimeTemplate('Export Date Time', 'CreatedDate'),
      AgGridHelper.stringTemplate('IP Address', 'IPAddress'),
      AgGridHelper.stringTemplate('Location', 'Location'),
    ];
  }

   reload() {
      let that = this;
      that.gridReady();
   }

  setBreadcrumbs() {
    let that = this;
    let breadcrumbs: Breadcrumb[] = [];
    breadcrumbs.push({ label: that.exportLogs.grid.routeInfo.label, url: '', params: {} });
    that.breadcrumbService.set(breadcrumbs);
  }
}

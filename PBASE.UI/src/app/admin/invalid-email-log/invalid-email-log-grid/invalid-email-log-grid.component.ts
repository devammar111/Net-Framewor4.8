import { Component, ViewChild, OnInit, AfterViewInit, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
//service
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
//custom
import { AgGridSource } from '../../../shared/ag-grid/ag-grid-source';
import { AgGridHelper } from '../../../shared/ag-grid/ag-grid-helper';
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus } from '../../../shared/all-routes';
import { CryptoService } from 'src/app/shared/crypto/crypto.service';
import { HttpService } from 'src/app/shared/http.service';
import { FilePickerComponent } from 'src/app/shared/file-picker/file-picker.component';
declare var agGrid: any;

@Component({
  selector: 'app-invalid-email-log-grid',
  templateUrl: './invalid-email-log-grid.component.html'
})
export class InvalidEmailLogGridComponent implements OnInit {
  gridOptions: any = {};
  queryParams: any = {};
  pbRoute: Menu = pBMenus.find(x => x.id == "InvalidEmailLog");
  @ViewChild('vw_InvalidEmailLogGrid') vw_InvalidEmailLogGrid: ElementRef;

  constructor(
    private dataSource: AgGridSource,
    private router: Router,
    private route: ActivatedRoute,
    private cryptoService: CryptoService,
    private httpService: HttpService,
    private readonly breadcrumbService: BreadcrumbService,
    private filePickerComponent: FilePickerComponent
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    }
  }

  ngOnInit() {
    let that = this;
    that.queryParams = that.route.snapshot.queryParams;
    that.gridOptions.GridLabel = that.pbRoute.grid.routeInfo.label;
    that.dataSource.queryName = that.cryptoService.decryptString(that.queryParams.localName);
    that.dataSource.init(that.pbRoute.grid.vwName, that.gridOptions, 'LastAccessDate', 'desc', '', '');
    that.gridOptions.context = { gridComponent: that };
    that.gridOptions.onGridReady = (params) => {
      that.gridReady(params);
    }
    new agGrid.Grid(that.vw_InvalidEmailLogGrid.nativeElement, that.gridOptions);
    that.setBreadcrumbs();
  }

  gridReady(params?) {
    var that = this;
    let colDefs = that.createColumnDefs();
    that.dataSource.restoreColumnDefs(colDefs);

  }

  private createColumnDefs() {
    let that = this;
    return [
      AgGridHelper.numberTemplate('Id', 'Id', { hide: true }),
      AgGridHelper.stringTemplate('Email', 'Email'),
      AgGridHelper.numberTemplate('Access Failed Count', 'AccessFailedCount'),
      AgGridHelper.dateTimeTemplate('Access Failed Date', 'LastAccessDate'),
      AgGridHelper.dateTimeTemplate('Created Date', 'CreatedDate')
    ];
  }


  reload() {
    let that = this;
    that.gridReady();
  }

  setBreadcrumbs() {
    let that = this;
    let breadcrumbs: Breadcrumb[] = [];
    breadcrumbs.push({
      label: that.pbRoute.grid.routeInfo.label,
      url: '',
      params: {}
    });
    that.breadcrumbService.set(breadcrumbs);
  }
}


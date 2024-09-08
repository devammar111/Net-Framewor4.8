import { Component, OnInit, ViewChild, ElementRef, AfterViewInit, ViewContainerRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DateConvertor } from '../../../shared/date-convertor';
//service
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { CryptoService } from '../../../shared/crypto/crypto.service';
//custom
import { AgGridSource } from '../../../shared/ag-grid/ag-grid-source';
import { AgGridHelper } from '../../../shared/ag-grid/ag-grid-helper';
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus } from '../../../shared/all-routes';
declare var agGrid: any;

@Component({
  selector: 'app-login-analysis-grid-detail',
  templateUrl: './login-analysis-detail-grid.component.html',
  styleUrls: ['./login-analysis-detail-grid.component.scss']
})
export class LoginAnalysisDetailGridComponent implements OnInit {

  private gridOptions: any = {};

  @ViewChild('loginAnalysis') loginAnalysis: ElementRef;
  loginAnalysisRoutes: Menu = pBMenus.find(x => x.id == "LoginAnalysis");
  public myDatePickerOptions: {
    dateFormat: 'dd/mm/yyyy',
  };
  StartDateField: any;
  EndDateField: any;
  isSearching: false;
  isReseting: false;
  params: any = {};
  userName: string;
  data: any = {};
  dateConvertor: DateConvertor;
  DoBDate: any = "";

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private breadcrumbService: BreadcrumbService,
    private dataSource: AgGridSource,
    private cryptoService: CryptoService,
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    }
  }

  ngOnInit() {
    let that = this;
    that.userName = that.route.snapshot.queryParams["username"];
    that.data = { startDate: that.route.snapshot.queryParams["startDate"], endDate: that.route.snapshot.queryParams["endDate"], userId: that.route.snapshot.queryParams["userId"] };
    that.dateConvertor = new DateConvertor();
    that.gridOptions.GridLabel = that.loginAnalysisRoutes.editForm.routeInfo.label;
    that.dataSource.init(that.loginAnalysisRoutes.editForm.heading, that.gridOptions, 'AspNetUserLogsKey', 'asc', that.data, '');
    that.gridOptions.context = { gridComponent: that };
      that.gridOptions.onGridReady = (params) => {
         that.gridReady(params);
      }
      new agGrid.Grid(that.loginAnalysis.nativeElement, that.gridOptions);
      that.setBreadcrumbs();
   }

   gridReady(params?) {
      var that = this;
      let colDefs = that.createColumnDefs();
      that.dataSource.restoreColumnDefs(colDefs);

   }

  private createColumnDefs() {
    return [
      AgGridHelper.stringTemplate('Attempt Type', 'RequestType'),
      AgGridHelper.dateTemplate('Date', 'CreatedDate'),
      AgGridHelper.timeTemplate('Time', 'Time'),
      AgGridHelper.booleanTemplate('Status', 'IsStatus'),
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
    breadcrumbs.push({
      label: that.loginAnalysisRoutes.grid.routeInfo.label,
      url: '/' + that.loginAnalysisRoutes.grid.routeInfo.route,
      params: {
        localName: that.cryptoService.encryptString(that.loginAnalysisRoutes.grid.vwName),
        startDate: that.route.snapshot.queryParams["startDate"],
        endDate: that.route.snapshot.queryParams["endDate"]
      }
    });
    breadcrumbs.push({
      label: that.userName,
      url: '',
      params: {}
    });
    that.breadcrumbService.set(breadcrumbs);
  }

}

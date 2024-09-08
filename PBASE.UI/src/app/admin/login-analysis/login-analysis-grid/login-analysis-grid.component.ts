import { Component, OnInit, ViewChild, ElementRef, AfterViewInit, ViewContainerRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
//service
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
//custom
import { AgGridSource } from '../../../shared/ag-grid/ag-grid-source';
import { AgGridHelper } from '../../../shared/ag-grid/ag-grid-helper';
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus } from '../../../shared/all-routes';
import { ToastrService } from 'ngx-toastr';
import { DateConvertor } from '../../../shared/date-convertor';
import { CryptoService } from 'src/app/shared/crypto/crypto.service';
declare var agGrid: any;

@Component({
  selector: 'app-login-analysis-grid',
  templateUrl: './login-analysis-grid.component.html',
  styleUrls: ['./login-analysis-grid.component.scss']
})
export class LoginAnalysisGridComponent implements OnInit {
  gridOptions: any = {};
  loginAnalysisRoutes: Menu = pBMenus.find(x => x.id == "LoginAnalysis");
  @ViewChild('vw_AspNetUserLogsGrid') vw_AspNetUserLogsGrid: ElementRef;
  StartDateField: any = {};
  EndDateField: any = {};
  isSearching: false;
  isReseting: false;
  queryParams: any = {};
  params: any = {};
  dateConvertor: DateConvertor;
  isReadonly: boolean = false;
  constructor(private toastr: ToastrService,
    private route: ActivatedRoute,
    private router: Router,
    private breadcrumbService: BreadcrumbService,
    private cryptoService: CryptoService,
    private dataSource: AgGridSource) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    }
  }

  ngOnInit() {
    let that = this;
    that.dateConvertor = new DateConvertor();
    that.queryParams = that.route.snapshot.queryParams;
    that.isReadonly = JSON.parse(localStorage.getItem('currentUser')).isReadonly;
    that.gridOptions.GridLabel = that.loginAnalysisRoutes.grid.routeInfo.label;
    that.dataSource.queryName = that.cryptoService.decryptString(that.queryParams.localName);
    that.dataSource.init(that.loginAnalysisRoutes.grid.vwName, that.gridOptions, 'username', 'asc', '', '');
    that.gridOptions.context = { gridComponent: that };
      that.gridOptions.onGridReady = (params) => {
         that.gridReady(params);
      }
      new agGrid.Grid(that.vw_AspNetUserLogsGrid.nativeElement, that.gridOptions);
      that.setBreadcrumbs();
   }

   gridReady(params?) {
      var that = this;
      let colDefs = that.createColumnDefs();
      that.dataSource.restoreColumnDefs(colDefs);

      if (that.queryParams.startDate && that.queryParams.endDate) {
         that.restoreState();
      }
   }

  private createColumnDefs() {
    return [
      AgGridHelper.actionTemplate('Action', 'Action', { cellRendererFramework: this.renderActionLink }),
      AgGridHelper.numberTemplate('UserId', 'Id', { hide: true }),
      AgGridHelper.stringTemplate('User', 'FullName'),
      AgGridHelper.stringTemplate('Username', 'username'),
      AgGridHelper.numberTemplate('Total Logins', 'TotalLogin'),
      AgGridHelper.numberTemplate('Successful Logins', 'LoginSuccess'),
      AgGridHelper.numberTemplate('Failed Logins', 'LoginFailure'),
      AgGridHelper.numberTemplate('Password Reset Requests', 'PasswordResetTotal'),
      AgGridHelper.numberTemplate('Successful Password Resets', 'PasswordResetSuccess'),
      AgGridHelper.numberTemplate('Failed Password Resets', 'PasswordResetFailure'),
      AgGridHelper.numberTemplate('Successful Change Passwords', 'ChangePasswordSuccess'),
      AgGridHelper.numberTemplate('Failed Change Passwords', 'ChangePasswordFailure'),
      AgGridHelper.dateTemplate('Last Successful Login', 'SuccessCreatedDate'),
      AgGridHelper.dateTemplate('Last Failed Login', 'FailedCreatedDate'),
      AgGridHelper.stringTemplate('IP Address', 'IPAddress'),
      AgGridHelper.stringTemplate('Location', 'Location'),
    ];
  }

  renderActionLink(params: any) {
    let div: HTMLDivElement = document.createElement('div');
    let link: HTMLAnchorElement = document.createElement('a');
    div.className = "text-center";
    link.className = "anchor-color";
    link.innerHTML = "<i class='fa fa-pencil-alt' title='Edit'></i></a>";
    link.onclick = function () {
      params.context.gridComponent.navigate(params);
    };
    div.appendChild(link);
    return div;
  }

  navigate(params?: any) {
    var that = this;
    let startDate = that.dateConvertor.toGridModel(that.StartDateField);
    let endDate = that.dateConvertor.toGridModel(that.EndDateField);
    that.router.navigate(["/" + that.loginAnalysisRoutes.grid.routeInfo.route + '/' + that.loginAnalysisRoutes.editForm.routeInfo.route],
      {
        queryParams: {
          startDate: startDate,
          endDate: endDate,
          userId: params.data.EncryptedId,
          username: params.data.username
        }
      });
  }

  search() {
    if (this.dateConvertor.compareDates(this.EndDateField, this.StartDateField)) {
      this.toastr.error('End Date needs to be greater than Start Date');
      return;
    }

    let startDate = this.dateConvertor.toGridModel(this.StartDateField);
    let endDate = this.dateConvertor.toGridModel(this.EndDateField);
    this.params.StartDate = startDate;
    this.params.EndDate = endDate;
    let that = this;
    that.dataSource.init(that.loginAnalysisRoutes.grid.vwName, that.gridOptions, 'username', 'asc', this.params, '');
    that.dataSource.ProcessData();
  }

  reset() {
    let that = this;
    this.StartDateField = null;
    this.EndDateField = null;
    that.dataSource.init(that.loginAnalysisRoutes.grid.vwName, that.gridOptions, 'username', 'asc', '', '');
    that.dataSource.ProcessData();
  }

  restoreState() {
    let that = this;
    that.StartDateField = that.dateConvertor.fromModel(that.queryParams.startDate);
    that.EndDateField = that.dateConvertor.fromModel(that.queryParams.endDate);
    that.search();
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
      url: "",
      params: {}
    });
    that.breadcrumbService.set(breadcrumbs);
  }
}

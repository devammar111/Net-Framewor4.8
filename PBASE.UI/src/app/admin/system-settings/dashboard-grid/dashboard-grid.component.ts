import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AgGridSource } from '../../../shared/ag-grid/ag-grid-source';
import { AgGridHelper } from '../../../shared/ag-grid/ag-grid-helper';
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { CryptoService } from '../../../shared/crypto/crypto.service';
import { Menu, pBMenus } from 'src/app/shared/all-routes';
declare var agGrid: any;

@Component({
  selector: 'app-dashboard-grid',
  templateUrl: './dashboard-grid.component.html',
  styleUrls: ['./dashboard-grid.component.scss']
})
export class DashboardGridComponent implements OnInit {
  gridOptions: any = {};
  queryParams: any = {};
  dashboardRoutes: Menu = pBMenus.find(x => x.id == "DashboardOptions");
  @ViewChild('vw_UserDashboardOptionRoleGrid') vw_UserDashboardOptionRoleGrid: ElementRef;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private breadcrumbService: BreadcrumbService,
    private cryptoService: CryptoService,
    private dataSource: AgGridSource
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    }
  }

  ngOnInit() {
    let that = this;
    that.queryParams = that.route.snapshot.queryParams;
    that.gridOptions.GridLabel = that.dashboardRoutes.grid.routeInfo.label;
    that.dataSource.queryName = that.cryptoService.decryptString(that.queryParams.localName);
    that.dataSource.init(that.dashboardRoutes.grid.vwName, that.gridOptions, 'DashboardOptionId', 'desc', '', '');
    that.gridOptions.context = { gridComponent: that };
      that.gridOptions.onGridReady = (params) => {
         that.gridReady(params);
      }
      new agGrid.Grid(that.vw_UserDashboardOptionRoleGrid.nativeElement, that.gridOptions);
      that.setBreadcrumbs();
   }

   gridReady(params?) {
      var that = this;
      let colDefs = that.createColumnDefs();
      that.dataSource.restoreColumnDefs(colDefs);

   }

  createColumnDefs() {
    return [
      AgGridHelper.actionTemplate('Action', 'Action', { cellRendererFramework: this.renderActionLink }),
      AgGridHelper.numberTemplate('Dashboard Option Id', 'DashboardOptionId', { hide: true }),
      AgGridHelper.stringTemplate('Dashboard Object', 'DashboardOption'),
      AgGridHelper.stringTemplate('Role(s)', 'RoleNameList'),
    ];
  }

  renderActionLink(params: any) {
    let div: HTMLDivElement = document.createElement('div');
    let link: HTMLAnchorElement = document.createElement('a');
    div.className = "text-center";
    link.className = "anchor-color";
    link.innerHTML = "<i class='fa fa-pencil-alt' title='Edit'></i></a>";
    link.onclick = function () {
      params.context.gridComponent.navigate(params.data.EncryptedDashboardOptionId, params.data.DashboardOption);
    };
    div.appendChild(link);
    return div;
  }

  navigate(id?: string, DashboardOption?: string) {
    var that = this;
    if (id) {
      that.router.navigate(['/' + that.dashboardRoutes.grid.routeInfo.route + '/' + that.dashboardRoutes.editForm.routeInfo.route], {
        queryParams: {
          id: id,
          dashboardOption: that.cryptoService.encryptString(DashboardOption)
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
      label: that.dashboardRoutes.grid.routeInfo.label,
      url: '',
      params: {}
    });
    that.breadcrumbService.set(breadcrumbs);
  }
}

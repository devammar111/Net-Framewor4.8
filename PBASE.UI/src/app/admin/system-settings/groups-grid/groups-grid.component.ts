import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AgGridSource } from '../../../shared/ag-grid/ag-grid-source';
import { AgGridHelper } from '../../../shared/ag-grid/ag-grid-helper';
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { CryptoService } from '../../../shared/crypto/crypto.service';
import { Menu, pBMenus } from 'src/app/shared/all-routes';
import { HttpService } from 'src/app/shared/http.service';
declare var agGrid: any;

@Component({
  selector: 'app-groups-grid',
  templateUrl: './groups-grid.component.html',
  styleUrls: ['./groups-grid.component.scss']
})
export class GroupsGridComponent implements OnInit {
  gridOptions: any = {};
   queryParams: any = {};
   data : any;
  groupRoutes: Menu = pBMenus.find(x => x.id == "Groups");
  @ViewChild('vw_UserGroupGrid') vw_UserGroupGrid: ElementRef;

  constructor(
    private router: Router,    
    private route: ActivatedRoute,    
    private breadcrumbService: BreadcrumbService,  
    private cryptoService: CryptoService,
    private httpService: HttpService,
    private dataSource: AgGridSource
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    }
  }

  ngOnInit() {
     let that = this;
     let currentUser = JSON.parse(localStorage.getItem('currentUser')); 
    that.queryParams = that.route.snapshot.queryParams;
    that.gridOptions.GridLabel = that.groupRoutes.grid.routeInfo.label;
     that.dataSource.queryName = that.cryptoService.decryptString(that.queryParams.localName);
     if (currentUser.user.UserTypeId != 3) {
        that.dataSource.init(that.groupRoutes.grid.vwName, that.gridOptions, 'UserGroupId', 'desc', 'AspNetRoleId', 3, 'ne');
     }
     else {
        that.dataSource.init(that.groupRoutes.grid.vwName, that.gridOptions, 'UserGroupId', 'desc', '', '');
     }
    that.gridOptions.context = { gridComponent: that };
    let appInfo = this.httpService.getAppInfo(-9985);
    if (!appInfo.isReadonly) {
      that.gridOptions.addCallBack = function () {
        that.navigate();
      };
     }
     that.gridOptions.onGridReady = (params) => {
        that.gridReady(params);
     }
     new agGrid.Grid(that.vw_UserGroupGrid.nativeElement, that.gridOptions);
    that.setBreadcrumbs();
   }

   private gridReady(params?: any) {
      var that = this;
      let colDefs = that.createColumnDefs();
      that.dataSource.restoreColumnDefs(colDefs);
      

   }
  createColumnDefs() {
    return [
      AgGridHelper.actionTemplate('Action', 'Action', { cellRendererFramework: this.renderActionLink }),
      AgGridHelper.numberTemplate('User Group Id', 'UserGroupId', { hide: true }),
      AgGridHelper.stringTemplate('Group Name', 'UserGroupName'),
      AgGridHelper.stringTemplate('Role', 'RoleName'),
      AgGridHelper.stringTemplate('Menu Option(s)', 'UserMenuOptionList'),
       AgGridHelper.stringTemplate('Dashboard Option(s) ', 'UserDashboardOptionList'),
       AgGridHelper.numberTemplate('AspNet Role Id', 'AspNetRoleId', { hide: true }),
    ];
  }

  renderActionLink(params: any) {
    let div: HTMLDivElement = document.createElement('div');
    let link: HTMLAnchorElement = document.createElement('a');
    div.className = "text-center";
    link.className = "anchor-color";
    link.innerHTML = "<i class='fa fa-pencil-alt' title='Edit'></i></a>";
    link.onclick = function () {
      params.context.gridComponent.navigate(params.data.EncryptedUserGroupId);
    };
    div.appendChild(link);
    return div;
  }

  navigate(id?: string) {
    var that = this;
    if (id) {
      that.router.navigate(['/' + that.groupRoutes.grid.routeInfo.route + '/' + that.groupRoutes.editForm.routeInfo.route], {
        queryParams: {
          id: id,
        }
      });
    }
    else {
      that.router.navigate(['/' + that.groupRoutes.grid.routeInfo.route + '/' + that.groupRoutes.addForm.routeInfo.route]);
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
      label: that.groupRoutes.grid.routeInfo.label,
      url: '',
      params: {}
    });
    that.breadcrumbService.set(breadcrumbs);
  }
}

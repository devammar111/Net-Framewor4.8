import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
//service
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { LookupService } from '../../../shared/lookup.service';
import { CryptoService } from '../../../shared/crypto/crypto.service';
//custom
import { AgGridSource } from '../../../shared/ag-grid/ag-grid-source';
import { AgGridHelper } from '../../../shared/ag-grid/ag-grid-helper';
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus } from '../../../shared/all-routes';
import { forkJoin } from 'rxjs';
import { HttpService } from 'src/app/shared/http.service';
declare var agGrid: any;

@Component({
  selector: 'app-user-grid',
  templateUrl: './user-grid.component.html',
  styleUrls: ['./user-grid.component.scss']
})
export class UserGridComponent implements OnInit {
  gridOptions: any = {};
  queryParams: any = {};
  users: Menu = pBMenus.find(x => x.id == "Users");
  @ViewChild('vw_UserGrid') vw_UserGrid: ElementRef;

  constructor(
    private route: ActivatedRoute,
    private dataSource: AgGridSource,
    private lookupService: LookupService,
    private router: Router,
    private cryptoService: CryptoService,
    private httpService: HttpService,
    private breadcrumbService: BreadcrumbService
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    }
  }

  ngOnInit() {
     let that = this;
     let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    that.queryParams = that.route.snapshot.queryParams;
    that.gridOptions.GridLabel = that.users.grid.routeInfo.label;
     that.dataSource.queryName = that.cryptoService.decryptString(that.queryParams.localName);
     if (currentUser.user.UserTypeId != 3) {
        that.dataSource.init(that.users.grid.vwName, that.gridOptions, 'Id', 'desc', 'UserTypeId', 3, "ne");
     }
     else {
        that.dataSource.init(that.users.grid.vwName, that.gridOptions, 'Id', 'desc', '', '');
     }
    that.gridOptions.context = { gridComponent: that };
    let appInfo = this.httpService.getAppInfo(-9987);
    if (!appInfo.isReadonly) {
      that.gridOptions.addCallBack = function () {
        that.navigate();
      };
     }
     that.gridOptions.onGridReady = (params) => {
        that.gridReady(params);
     }
     new agGrid.Grid(that.vw_UserGrid.nativeElement, that.gridOptions);
     that.setBreadcrumbs();
  }
     gridReady(params ?) {
        var that = this;
      let userGroups = that.lookupService.getGridUserGroups();
      let roles = that.lookupService.getGridRoles();

      forkJoin([userGroups, roles])
        .subscribe((results: any) => {

          let UserGroups = results[0].map(function (item) {
            return item['UserGroupName'];
          });
          let Roles = results[1].map(function (item) {
            return item['UserType'];
          });

          let colDefs = that.createColumnDefs(UserGroups, Roles);
          that.dataSource.restoreColumnDefs(colDefs);
          
        });
    }


  private createColumnDefs(UserGroups, Roles) {
    return [
      AgGridHelper.actionTemplate('Action', 'Action', { cellRendererFramework: this.renderActionLink }),
      AgGridHelper.numberTemplate('User Id', 'Id', { hide: true }),
      AgGridHelper.stringTemplate('First Name', 'FirstName'),
      AgGridHelper.stringTemplate('Last Name', 'LastName'),
      AgGridHelper.stringTemplate('Username', 'UserName'),
      AgGridHelper.stringTemplate('Email Address', 'Email'),
      AgGridHelper.dropdownTemplate('User Type', 'AssignedRole', { items: Roles }),
      AgGridHelper.dropdownTemplate('Group Name', 'UserGroupName', { items: UserGroups }),
      AgGridHelper.booleanTemplate('Account Closed', 'LockoutEnabled'),
      AgGridHelper.booleanTemplate('Read Only', 'IsReadOnly'),
       AgGridHelper.booleanTemplate('Delete Option Disabled', 'IsDeleteDisabled'),
       AgGridHelper.numberTemplate('User Type Id', 'UserTypeId', { hide: true }),
    ];
  }

  renderActionLink(params: any) {
    let div: HTMLDivElement = document.createElement('div');
    let link: HTMLAnchorElement = document.createElement('a');
    div.className = "text-center";
    link.className = "anchor-color";
    link.innerHTML = "<i class='fa fa-pencil-alt' title='Edit'></i></a>";
    link.onclick = function () {
      params.context.gridComponent.navigate(params.data.EncryptedUserId);
    };
    div.appendChild(link);
    return div;
  }

  navigate(id?: string) {
    let that = this;
    if (id) {
      that.router.navigate(['/' + that.users.grid.routeInfo.route + '/' + that.users.editForm.routeInfo.route], {
        queryParams: {
          id: id,
        }
      });
    }
    else {
      that.router.navigate(['/' + that.users.grid.routeInfo.route + '/' + that.users.addForm.routeInfo.route]);
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
      label: that.users.grid.routeInfo.label,
      url: '',
      params: {}
    });
    this.breadcrumbService.set(breadcrumbs);
  }
}

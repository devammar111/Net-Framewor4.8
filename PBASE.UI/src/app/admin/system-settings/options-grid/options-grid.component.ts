import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AgGridSource } from '../../../shared/ag-grid/ag-grid-source';
import { AgGridHelper } from '../../../shared/ag-grid/ag-grid-helper';
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { CryptoService } from '../../../shared/crypto/crypto.service';
import { Menu, pBMenus } from 'src/app/shared/all-routes';
import { LookupService } from 'src/app/shared/lookup.service';
import { forkJoin } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { NGXLogger } from 'ngx-logger';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationDialogService } from '../../../shared/confirmation-dialog/confirmation-dialog.service';
declare var agGrid: any;

@Component({
  selector: 'app-options-grid',
  templateUrl: './options-grid.component.html',
  styleUrls: ['./options-grid.component.scss']
})
export class OptionsGridComponent implements OnInit {
  gridOptions: any = {};
  queryParams: any = {};
  optionsRoutes: Menu = pBMenus.find(x => x.id == "Options");
  @ViewChild('vw_UserMenuOptionRoleGrid') vw_UserMenuOptionRoleGrid: ElementRef;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private breadcrumbService: BreadcrumbService,
    private lookupService: LookupService,
    private cryptoService: CryptoService,
     private dataSource: AgGridSource, private toastr: ToastrService,
     private http: HttpClient,
     private logger: NGXLogger, private confirmationDialogService: ConfirmationDialogService,

  ) {
     this.dataSource = new AgGridSource(http,toastr, confirmationDialogService, logger);
     this.router.routeReuseStrategy.shouldReuseRoute = function () {
        return false;
     }
  }

  ngOnInit() {
    let that = this;
    that.queryParams = that.route.snapshot.queryParams;
    that.gridOptions.GridLabel = that.optionsRoutes.grid.routeInfo.label;
    that.dataSource.queryName = that.cryptoService.decryptString(that.queryParams.localName);
    that.dataSource.init(that.optionsRoutes.grid.vwName, that.gridOptions, 'MenuOptionId', 'desc', '', '');
    that.gridOptions.context = { gridComponent: that };
      that.gridOptions.onGridReady = (params) => {
         that.gridReady(params);
      }
     new agGrid.Grid(that.vw_UserMenuOptionRoleGrid.nativeElement, that.gridOptions);
      that.setBreadcrumbs();
   }

   gridReady(params?) {
      var that = this;
      let ObjectTypes = that.lookupService.getGridObjectTypes();
      forkJoin([ObjectTypes])
         .subscribe((results: any) => {
            let ObjectTypes = results[0].map(function (item) {
               return item['ObjectType'];
            });
            let colDefs = that.createColumnDefs(ObjectTypes);
            that.dataSource.restoreColumnDefs(colDefs);

         });
   }

  createColumnDefs(ObjectTypes) {
    return [
      AgGridHelper.actionTemplate('Action', 'Action', { cellRendererFramework: this.renderActionLink }),
      AgGridHelper.numberTemplate('Menu Option Id', 'MenuOptionId', { hide: true }),
      AgGridHelper.stringTemplate('Object Name', 'MenuOption'),
      AgGridHelper.stringTemplate('Role(s)', 'RoleNameList'),
      AgGridHelper.dropdownTemplate('Object Type', 'ObjectType', { items: ObjectTypes }),
      AgGridHelper.stringTemplate('Group(s)', 'GroupList'),
    ];
  }

  renderActionLink(params: any) {
    let div: HTMLDivElement = document.createElement('div');
    let link: HTMLAnchorElement = document.createElement('a');
    div.className = "text-center";
    link.className = "anchor-color";
    link.innerHTML = "<i class='fa fa-pencil-alt' title='Edit'></i></a>";
    link.onclick = function () {
      params.context.gridComponent.navigate(params.data.EncryptedMenuOptionId, params.data.ObjectName);
    };
    div.appendChild(link);
    return div;
  }

  navigate(encId?: string, ObjectName?: string) {
    var that = this;
    if (encId) {
      that.router.navigate(['/' + that.optionsRoutes.grid.routeInfo.route + '/' + that.optionsRoutes.editForm.routeInfo.route], {
        queryParams: {
          id: encId,
          menuOption: that.cryptoService.encryptString(ObjectName)
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
      label: that.optionsRoutes.grid.routeInfo.label,
      url: '',
      params: {}
    });
    that.breadcrumbService.set(breadcrumbs);
  }
}

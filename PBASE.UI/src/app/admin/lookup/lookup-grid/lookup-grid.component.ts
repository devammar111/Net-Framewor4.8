import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
//service
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { CryptoService } from '../../../shared/crypto/crypto.service';
//custom
import { AgGridSource } from '../../../shared/ag-grid/ag-grid-source';
import { AgGridHelper } from '../../../shared/ag-grid/ag-grid-helper';
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus } from '../../../shared/all-routes';
import { HttpService } from 'src/app/shared/http.service';
declare var agGrid: any;

@Component({
  selector: 'app-lookup-grid',
  templateUrl: './lookup-grid.component.html',
  styleUrls: ['./lookup-grid.component.scss']
})
export class LookupGridComponent implements OnInit {
  gridOptions: any = {};
  queryParams: any = {};
  lookupTypes: Menu = pBMenus.find(x => x.id == "LookupType");
  lookups: Menu = pBMenus.find(x => x.id == "Lookups");
  lookupName: string;
  @ViewChild('vw_LookupGrid') vw_LookupGrid: ElementRef

  constructor(
    private route: ActivatedRoute,
    private dataSource: AgGridSource,
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
    that.queryParams = that.route.snapshot.queryParams;
    that.gridOptions.GridLabel = that.lookups.grid.routeInfo.label;
    that.lookupName = that.cryptoService.decryptString(that.queryParams.lookupName);
    that.dataSource.queryName = that.cryptoService.decryptString(that.queryParams.localName);
    that.dataSource.init(that.lookups.grid.vwName, that.gridOptions, 'LookupId', 'desc', 'LookupTypeId', that.queryParams.id);
    that.gridOptions.context = { gridComponent: that };
    let appInfo = this.httpService.getAppInfo(-9988);
    if (!appInfo.isReadonly) {
      that.gridOptions.addCallBack = function () {
        that.navigate();
      };
    }
      that.gridOptions.onGridReady = (params) => {
         that.gridReady(params);
      }
      new agGrid.Grid(that.vw_LookupGrid.nativeElement, that.gridOptions);
      that.setBreadcrumbs();
   }

   gridReady(params?) {
      var that = this;
      let colDefs = that.createColumnDefs();
      that.dataSource.restoreColumnDefs(colDefs);

   }

  private createColumnDefs() {
    return [
      AgGridHelper.actionTemplate('Action', 'Action', { cellRendererFramework: this.actionCellRenderer }),
      AgGridHelper.numberTemplate('Lookup Id', 'LookupId', { hide: true }),
      AgGridHelper.stringTemplate('Name', 'LookupName'),
      AgGridHelper.stringTemplate('Short Name', 'LookupNameShort'),
      AgGridHelper.numberTemplate('Sort Order', 'SortOrder'),
      AgGridHelper.stringTemplate('Lookup Group', 'LookupNameType', { headerValueGetter: this.headerCellRendererFunc }),
      AgGridHelper.stringTemplate('Lookup Group 2', 'LookupNameType2', { headerValueGetter: this.headerCellRendererFunc }),
      AgGridHelper.stringTemplate('Lookup Group 3', 'LookupNameType3', { headerValueGetter: this.headerCellRendererFunc }),
      AgGridHelper.stringTemplate('Lookup Group 4', 'LookupNameType4', { headerValueGetter: this.headerCellRendererFunc }),
      AgGridHelper.booleanTemplate('Archived', 'IsArchived'),
    ];
  }

  headerCellRendererFunc(params: any) {
    let that = params.context.gridComponent;
    var label1 = that.cryptoService.decryptString(that.queryParams.label1);
    var label2 = that.cryptoService.decryptString(that.queryParams.label2);
    var label3 = that.cryptoService.decryptString(that.queryParams.label3);
    var label4 = that.cryptoService.decryptString(that.queryParams.label4);
    if (params.colDef.field == 'LookupNameType') {
      return label1;
    }
    if (params.colDef.field == 'LookupNameType2') {
      return label2;
    }
    if (params.colDef.field == 'LookupNameType3') {
      return label3;
    }
    if (params.colDef.field == 'LookupNameType4') {
      return label4;
    }
  }

  actionCellRenderer(params: any) {
    let div: HTMLDivElement = AgGridHelper.createDiv();
    let link: HTMLAnchorElement = AgGridHelper.createLink('fa-pencil-alt', 'Edit');
    link.onclick = function () {
      params.context.gridComponent.navigate(params.data.EncryptedLookupId);
    };
    div.appendChild(link);
    return div;
  }

  navigate(id?: number) {
    var that = this;
    if (id) {
      that.router
        .navigate([that.lookupTypes.grid.routeInfo.route + '/' + that.lookups.grid.routeInfo.route + '/' + that.lookups.editForm.routeInfo.route], {
          queryParams: {
            id: that.queryParams.id,
            lookupName: that.queryParams.lookupName,
            label1: that.queryParams.label1,
            label2: that.queryParams.label2,
            label3: that.queryParams.label3,
            label4: that.queryParams.label4,
            lookupId: id
          }
        });
    }
    else {
      that.router
        .navigate([that.lookupTypes.grid.routeInfo.route + '/' + that.lookups.grid.routeInfo.route + '/' + that.lookups.addForm.routeInfo.route], {
          queryParams: {
            id: that.queryParams.id,
            lookupName: that.queryParams.lookupName,
            label1: that.queryParams.label1,
            label2: that.queryParams.label2,
            label3: that.queryParams.label3,
            label4: that.queryParams.label4,
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
      label: that.lookupTypes.grid.routeInfo.label,
      url: '/' + that.lookupTypes.grid.routeInfo.route,
      params: {
        localName: that.cryptoService.encryptString(that.lookupTypes.grid.vwName)
      }
    });
    breadcrumbs.push({
      label: that.lookupName,
      url: '',
      params: {}
    });
    that.breadcrumbService.set(breadcrumbs);
  }
}

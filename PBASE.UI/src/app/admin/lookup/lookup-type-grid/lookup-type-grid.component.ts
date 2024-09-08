import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
//service
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { CryptoService } from '../../../shared/crypto/crypto.service';
//custom
import { AgGridSource } from '../../../shared/ag-grid/ag-grid-source'
import { AgGridHelper } from '../../../shared/ag-grid/ag-grid-helper'
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus } from '../../../shared/all-routes';
import { HttpService } from '../../../shared/http.service';
declare var agGrid: any;

@Component({
  selector: 'app-lookup-type-grid',
  templateUrl: './lookup-type-grid.component.html',
  styleUrls: ['./lookup-type-grid.component.scss']
})
export class LookupTypeGridComponent implements OnInit {
  gridOptions: any = {};
  queryParams: any = {};
  lookupTypes: Menu = pBMenus.find(x => x.id == "LookupType");
  lookups: Menu = pBMenus.find(x => x.id == "Lookups");
  isNotSystemAdmin: boolean = false;
  @ViewChild('vw_LookupTypeGrid') vw_LookupTypeGrid: ElementRef;

  constructor(
    private route: ActivatedRoute,
    private dataSource: AgGridSource,
    private router: Router,
    private cryptoService: CryptoService,
    private breadcrumbService: BreadcrumbService,
    private httpService: HttpService,
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    }
   }

   ngOnInit() {
      let that = this;
      that.get();
      that.setBreadcrumbs();
   }

   get() {
      let that = this;
      that.httpService
         .get('checkIsNotSystemAdmin')
         .subscribe(data => {
            if (data == "yes") {
               that.isNotSystemAdmin = true;
            }
            else {
               that.isNotSystemAdmin = false;
            }
            that.queryParams = that.route.snapshot.queryParams;
            that.gridOptions.GridLabel = that.lookupTypes.grid.routeInfo.label;
            that.dataSource.queryName = that.cryptoService.decryptString(that.queryParams.localName);
            if (that.isNotSystemAdmin) {
               that.dataSource.init(that.lookupTypes.grid.vwName, that.gridOptions, 'LookupTypeId', 'desc', 'IsLocked', false);
            }
            else {
               that.dataSource.init(that.lookupTypes.grid.vwName, that.gridOptions, 'LookupTypeId', 'desc', '', '');
            }
            that.gridOptions.context = { gridComponent: that };
            that.gridOptions.onGridReady = (params) => {
               that.gridReady(params);
            }
            new agGrid.Grid(that.vw_LookupTypeGrid.nativeElement, that.gridOptions);
         },
)}
   gridReady(params?) {
      var that = this;
      let colDefs = that.createColumnDefs();
      that.dataSource.restoreColumnDefs(colDefs);

   }

  private createColumnDefs() {
    return [
      AgGridHelper.actionTemplate('Action', 'Action', { cellRendererFramework: this.actionCellRenderer }),
      AgGridHelper.numberTemplate('Lookup Type Id', 'LookupTypeId', { hide: true }),
      AgGridHelper.stringTemplate('Lookup Type', 'LookupTypeText'),
      AgGridHelper.stringTemplate('Lookup Items', 'LookupList'),
    ];
  }

  actionCellRenderer(params: any) {
    let div: HTMLDivElement = AgGridHelper.createDiv();
    let link: HTMLAnchorElement = AgGridHelper.createLink('fa-pencil-alt', 'Edit');
    link.onclick = function () {
      params.context.gridComponent.navigate(params.data);
    };
    div.appendChild(link);
    return div;
  }

  navigate(data?) {
    var that = this;
    if (data) {
      that.router
        .navigate([that.lookupTypes.grid.routeInfo.route + '/' + that.lookups.grid.routeInfo.route], {
          queryParams: {
            id: data.EncryptedLookupTypeId,
            lookupName: that.cryptoService.encryptString(data.LookupTypeText),
            label1: that.cryptoService.encryptString(data.LookupViewLabel ? data.LookupViewLabel : 'Lookup Group'),
            label2: that.cryptoService.encryptString(data.LookupViewLabel2 ? data.LookupViewLabel2 : 'Lookup Group 2'),
            label3: that.cryptoService.encryptString(data.LookupViewLabel3 ? data.LookupViewLabel3 : 'Lookup Group 3'),
            label4: that.cryptoService.encryptString(data.LookupViewLabel4 ? data.LookupViewLabel4 : 'Lookup Group 4'),
          }
        });
    }
  }

   reload() {
      let that = this;
      that.gridReady();
   }

  setBreadcrumbs() {
    var that = this;
    let breadcrumbs: Breadcrumb[] = [];
    breadcrumbs.push({
      label: that.lookupTypes.grid.routeInfo.label,
      url: '',
      params: {}
    });
    that.breadcrumbService.set(breadcrumbs);
  }
}

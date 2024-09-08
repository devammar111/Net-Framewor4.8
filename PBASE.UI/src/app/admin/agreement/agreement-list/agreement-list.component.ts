import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { AgGridHelper } from '../../../shared/ag-grid/ag-grid-helper';
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { LookupService } from '../../../shared/lookup.service';
import { AgGridSource } from '../../../shared/ag-grid/ag-grid-source';
import { Router, ActivatedRoute } from '@angular/router';
import { forkJoin } from 'rxjs';
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { pBMenus, Menu } from 'src/app/shared/all-routes';
import { CryptoService } from 'src/app/shared/crypto/crypto.service';
import { HttpService } from 'src/app/shared/http.service';
import { StateManagementService } from 'src/app/shared/state-management/state-management.service';
declare var agGrid: any;

@Component({
   selector: 'app-agreement-list',
   templateUrl: './agreement-list.component.html',
   styles: []
})
export class AgreementListComponent implements OnInit {
   gridOptions: any = {};
   queryParams: any = {};
   @ViewChild('vw_AgreementGrid') vw_AgreementGrid: ElementRef
   agreement: Menu = pBMenus.find(x => x.id == "Agreement");
   api: string = 'vw_AgreementGrid/';
   constructor(
      private router: Router,
      private route: ActivatedRoute,
      private breadcrumbService: BreadcrumbService,
      private lookupService: LookupService,
      private cryptoService: CryptoService,
      private httpService: HttpService,
      private stateManagementService: StateManagementService,
      private dataSource: AgGridSource) {
      this.router.routeReuseStrategy.shouldReuseRoute = function () {
         return false;
      }

   }

   ngOnInit() {
      let that = this;
    that.removeStateLocalStroage();
     that.queryParams = that.route.snapshot.queryParams;
      that.gridOptions.GridLabel = that.agreement.grid.routeInfo.label;
      that.dataSource.queryName = that.cryptoService.decryptString(that.queryParams.localName);
      that.dataSource.init(that.agreement.grid.vwName, that.gridOptions, 'AgreementId', 'desc', '', '');
      that.gridOptions.context = { gridComponent: that };
      let appInfo = this.httpService.getAppInfo(-9978);
      if (!appInfo.isReadonly) {
         that.gridOptions.addCallBack = function () {
            that.navigate();
         };
      }
      that.gridOptions.onGridReady = (params) => {
         that.gridReady(params);
      }
      new agGrid.Grid(that.vw_AgreementGrid.nativeElement, that.gridOptions);
      that.setBreadcrumbs();
   }
       private gridReady(params?: any) {
      let that = this;
         let colDefs = that.createColumnDefs();
         that.dataSource.restoreColumnDefs(colDefs);
         
      }



  removeStateLocalStroage() {
    let that = this;
    that.stateManagementService.remove(that.agreement.id);
  }

   createColumnDefs() {
      return [
         AgGridHelper.actionTemplate('Action', 'Action', { cellRendererFramework: this.renderActionLink }),
         AgGridHelper.numberTemplate('Agreement Id', 'AgreementId', { hide: true }),
         AgGridHelper.numberTemplate('Original Agreement Id', 'OriginalAgreementId', { hide: true }),
         AgGridHelper.stringTemplate('Agreement Name', 'AgreementHeader'),
         AgGridHelper.dateTemplate('Agreement Date', 'AgreementDate'),
         AgGridHelper.stringTemplate('User Type', 'UserTypeList'),
         AgGridHelper.numberTemplate('Accepted', 'AcceptedCount'),
         AgGridHelper.numberTemplate('Declined', 'DeclinedCount'),
         AgGridHelper.numberTemplate('Outstanding', 'OutstandingCount'),
         AgGridHelper.numberTemplate('Version No', 'VersionNo'),
         AgGridHelper.booleanTemplate('Locked?', 'IsLocked'),
         AgGridHelper.booleanTemplate('Latest Version?', 'IsLatest'),
         AgGridHelper.booleanTemplate('Archived?', 'IsArchived'),

      ];
   }
   renderActionLink(params: any) {

      let div: HTMLDivElement = document.createElement('div');
      div.className = "action-panel";
     let link: HTMLAnchorElement = AgGridHelper.createLink('fa fa-pencil-alt', 'Edit');
      link.style.marginRight = '5px';
      link.onclick = function () {
         params.context.gridComponent.navigate(params.data);
      };
      div.appendChild(link);
      if (params.data && params.data.IsLatest) {
          let companylink: HTMLAnchorElement = AgGridHelper.createLink('fa fa-copy', 'Copy');
         companylink.onclick = function () {
             params.context.gridComponent.navigateToNewAgreement(params.data);
         };
         div.appendChild(companylink);
      }
      return div;
   }

   navigate(data?: any) {
      var that = this;
      if (data) {
         that.router.navigate(['/' + that.agreement.grid.routeInfo.route + '/' + that.agreement.editForm.routeInfo.route], {
            queryParams: {
               id: data.EncryptedAgreementId,
               IsLocked: data.IsLocked
            }
         });
      }
      else {
         that.router.navigate(['/' + that.agreement.grid.routeInfo.route + '/' + that.agreement.addForm.routeInfo.route]);
      }
  }

   navigateToNewAgreement(data?: any) {
     let that = this;
     that.httpService
       .get("sp_AgreementVersionNew/" + data.EncryptedAgreementId)
       .subscribe(data => {
         that.router.navigate(['/' + that.agreement.grid.routeInfo.route + '/' + that.agreement.editForm.routeInfo.route], {
           queryParams: {
             id: data.AgreementId,
             IsLocked: data.IsLocked
           }
         });
       });
  }

   reload() {
      let that = this;
      that.gridReady();
   }

   setBreadcrumbs() {
      let that = this;
      let breadcrumbs: Breadcrumb[] = [];
      breadcrumbs.push({ label: 'Agreements', url: '', params: {} });
      that.breadcrumbService.set(breadcrumbs);
   }
}

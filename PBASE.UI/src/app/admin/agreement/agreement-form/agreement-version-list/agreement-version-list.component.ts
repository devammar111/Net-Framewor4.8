import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { AgGridHelper } from '../../../../shared/ag-grid/ag-grid-helper';
import { BreadcrumbService } from '../../../../shared/breadcrumb/breadcrumb.service';
import { LookupService } from '../../../../shared/lookup.service';
import { AgGridSource } from '../../../../shared/ag-grid/ag-grid-source';
import { Router, ActivatedRoute } from '@angular/router';
import { forkJoin } from 'rxjs';
import { Breadcrumb } from '../../../../shared/breadcrumb/breadcrumb.model';
import { pBMenus, Menu } from 'src/app/shared/all-routes';
import { CryptoService } from 'src/app/shared/crypto/crypto.service';
import { NGXLogger } from 'ngx-logger';
import { ToastrService } from 'ngx-toastr';
import { HttpClient } from '@angular/common/http';
import { ConfirmationDialogService } from '../../../../shared/confirmation-dialog/confirmation-dialog.service';
declare var agGrid: any;

@Component({
   selector: 'app-agreement-version-list',
   templateUrl: './agreement-version-list.component.html',
   styles: []
})
export class AgreementVersionListComponent implements OnInit {
   AgreementId: string;
   gridOptions: any = {};
   queryParams: any = {};
   @ViewChild('vw_AgreementVersionSubGrid') vw_AgreementVersionSubGrid: ElementRef
   agreementversion: Menu = pBMenus.find(x => x.id == "AgreementVersion");
   api: string = 'vw_AgreementVersionSubGrid/';
   collapsed: any = [true];
   constructor(
      private router: Router,
      private route: ActivatedRoute,
      private breadcrumbService: BreadcrumbService,
      private lookupService: LookupService,
      private cryptoService: CryptoService,
      private confirmationDialogService: ConfirmationDialogService,
      private toastr: ToastrService,
      private http: HttpClient,
      private logger: NGXLogger,
      private dataSource: AgGridSource) {
      this.dataSource = new AgGridSource(http, toastr, confirmationDialogService, logger);
      this.router.routeReuseStrategy.shouldReuseRoute = function () {
         return false;
      }

   }

   ngOnInit() {
      let that = this;
      that.queryParams = that.route.snapshot.queryParams;
      that.AgreementId = that.route.snapshot.queryParams['AgreementId'];
      that.gridOptions.GridLabel = "Agreements";
      that.dataSource.init('vw_AgreementVersionSubGrid', that.gridOptions, 'AgreementId', 'desc', 'AgreementId', that.queryParams.id);
      that.gridOptions.context = { gridComponent: that };
      that.gridOptions.onGridReady = (params) => {
         that.gridReady(params);
      }
      new agGrid.Grid(that.vw_AgreementVersionSubGrid.nativeElement, that.gridOptions);
   }

   gridReady(params?) {
      var that = this;
      let colDefs = that.createColumnDefs();
      that.dataSource.restoreColumnDefs(colDefs);

   }


   createColumnDefs() {
      let that = this;
      return [
         AgGridHelper.actionTemplate('Action', 'Action', { cellRendererFramework: that.actionCellRenderer }),
         AgGridHelper.numberTemplate('Agreement Id', 'AgreementId', { hide: true }),
         AgGridHelper.numberTemplate('Original Agreement Id', 'OriginalAgreementId', { hide: true }),
         AgGridHelper.numberTemplate('Version No', 'VersionNo'),
         AgGridHelper.dateTemplate('Agreement Date', 'AgreementDate'),
         AgGridHelper.stringTemplate('Created By', 'CreatedBy'),
         AgGridHelper.dateTemplate('Created Date', 'CreatedDate'),
         AgGridHelper.stringTemplate('Updated By', 'UpdatedBy'),
         AgGridHelper.dateTemplate('Updated Date', 'UpdatedDate'),
      ];
   }

   actionCellRenderer(params: any) {
      let div: HTMLDivElement = document.createElement('div');
      let link: HTMLAnchorElement = document.createElement('a');
      div.className = "text-center";
      link.className = "anchor-color";
     link.innerHTML = "<i class='fa fa-pencil-alt' title='Edit'></i></a>";
      link.onclick = function () {
         params.context.gridComponent.navigate(params.data.EncryptedAgreementId);
      };
      div.appendChild(link);
      return div;
   }

   navigate(data?: any) {
      var that = this;
      if (data) {
         that.router.navigate(['/' + that.agreementversion.grid.routeInfo.route + '/' + that.agreementversion.editForm.routeInfo.route], {
            queryParams: {
               id: data,
               IsLocked: that.queryParams.IsLocked,
            }
         });
      }
      else {
         that.router.navigate(['/' + that.agreementversion.grid.routeInfo.route + '/' + that.agreementversion.addForm.routeInfo.route]);
      }
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

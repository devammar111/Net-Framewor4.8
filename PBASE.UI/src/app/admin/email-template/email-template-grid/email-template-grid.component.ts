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
  selector: 'app-email-template-grid',
  templateUrl: './email-template-grid.component.html',
  styleUrls: ['./email-template-grid.component.css']
})
export class EmailTemplateGridComponent implements OnInit {
  gridOptions: any = {};
  queryParams: any = {};
  emailtemplate: Menu = pBMenus.find(x => x.id == "EmailTemplate");
  @ViewChild('vw_EmailTemplateGrid') vw_EmailTemplateGrid : ElementRef
    
  constructor(
    private route: ActivatedRoute,
    private dataSource: AgGridSource,
    private cryptoService: CryptoService,
    private lookupService: LookupService,
    private httpService: HttpService,
    private router: Router,
    private breadcrumbService: BreadcrumbService) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    }
  }

  ngOnInit() {
    let that = this;
    that.queryParams = that.route.snapshot.queryParams;
    that.gridOptions.GridLabel = that.emailtemplate.grid.routeInfo.label;
    that.dataSource.queryName = that.cryptoService.decryptString(that.queryParams.localName);
    that.dataSource.init(that.emailtemplate.grid.vwName, that.gridOptions, 'EmailTemplateId', 'desc', '', '');
    that.gridOptions.context = { gridComponent: that };
    let appInfo = this.httpService.getAppInfo(-9982);
    if (!appInfo.isReadonly) {
      that.gridOptions.addCallBack = function () {
        that.navigate();
      };
    }
      that.gridOptions.onGridReady = (params) => {
         that.gridReady(params)
      }
      new agGrid.Grid(that.vw_EmailTemplateGrid.nativeElement, that.gridOptions);
      that.setBreadcrumbs();
   }

   gridReady(params?) {
      var that = this;
      let EmailTemplateTypes = that.lookupService.getGridEmailTemplateTypes();
      let EmailTypes = that.lookupService.getGridEmailTypes();
      let TemplateAllowedTypes = that.lookupService.getGridTemplateAllowedTypes();

      forkJoin([EmailTemplateTypes, EmailTypes, TemplateAllowedTypes])
         .subscribe((results: any) => {

            let EmailTemplateTypes = results[0].map(function (item) {
               return item['EmailTemplateType'];
            });
            let EmailTypes = results[1].map(function (item) {
               return item['EmailType'];
            });
            let TemplateAllowedTypes = results[2].map(function (item) {
               return item['TemplateAllowedType'];
            });

            let colDefs = that.createColumnDefs(EmailTemplateTypes, EmailTypes, TemplateAllowedTypes);
            that.dataSource.restoreColumnDefs(colDefs);

         });
   }

  private createColumnDefs(EmailTemplateTypes, EmailTypes, TemplateAllowedTypes) {
    return [
      AgGridHelper.actionTemplate('Action', 'Action', { cellRendererFramework: this.renderActionLink }),
      AgGridHelper.numberTemplate('Email Template Id', 'EmailTemplateId', { hide: true }),
      AgGridHelper.dropdownTemplate('Template Type', 'EmailTemplateType', { items: EmailTemplateTypes }),
      AgGridHelper.dropdownTemplate('Email Type', 'EmailType', { items: EmailTypes }),
      AgGridHelper.stringTemplate('Template Name', 'EmailTemplateName'),
      AgGridHelper.stringTemplate('Email Subject', 'EmailSubject'),
      AgGridHelper.stringTemplate('From', 'FromEmailAddress'),
      AgGridHelper.dropdownTemplate('Templates Allowed', 'TemplateAllowedType', { items: TemplateAllowedTypes}),
      AgGridHelper.stringTemplate('Last Update By', 'LastUpdateBy'),
      AgGridHelper.dateTemplate('Last Update Date', 'LastUpdateDate'),
     
    ];
  }

  renderActionLink(params: any) {
    let div: HTMLDivElement = document.createElement('div');
    let link: HTMLAnchorElement = document.createElement('a');
    div.className = "text-center";
    link.className = "anchor-color";
    link.innerHTML = "<i class='fa fa-pencil-alt' title='Edit'></i></a>";
    link.onclick = function () {
      params.context.gridComponent.navigate(params.data.EncryptedEmailTemplateId);
    };
    div.appendChild(link);
    return div;
  }

  navigate(id?: string) {
    let that = this;
    if (id) {
      that.router.navigate(['/' + that.emailtemplate.grid.routeInfo.route + '/' + that.emailtemplate.editForm.routeInfo.route], {
        queryParams: {
          id: id,
        }
      });
    }
    else {
      that.router.navigate(['/' + that.emailtemplate.grid.routeInfo.route + '/' + that.emailtemplate.addForm.routeInfo.route]);
    }
  }

   reload() {
      let that = this;
      that.gridReady();
   }

  setBreadcrumbs() {
    let that = this;
    let breadcrumbs: Breadcrumb[] = [];
    breadcrumbs.push({ label: that.emailtemplate.grid.routeInfo.label, url: '', params: {} });
    this.breadcrumbService.set(breadcrumbs);
  }
}

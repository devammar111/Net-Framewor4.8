import { Component, ViewChild, OnInit, AfterViewInit, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
//service
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { CryptoService } from '../../../shared/crypto/crypto.service';
//custom
import { AgGridSource } from '../../../shared/ag-grid/ag-grid-source';
import { AgGridHelper } from '../../../shared/ag-grid/ag-grid-helper';
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus } from '../../../shared/all-routes';
declare var agGrid: any;

@Component({
  selector: 'app-email-grid',
  templateUrl: './email-grid.component.html',
  styleUrls: ['./email-grid.component.scss']
})
export class EmailGridComponent implements OnInit {
  gridOptions: any = {};
  queryParams: any = {};
  emails: Menu = pBMenus.find(x => x.id == "Email");
  @ViewChild('vw_EmailGrid') vw_EmailGrid: ElementRef;

  constructor(
    private dataSource: AgGridSource,
    private router: Router,
    private cryptoService: CryptoService,
    private route: ActivatedRoute,
    private readonly breadcrumbService: BreadcrumbService
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    }
  }

  ngOnInit() {
    let that = this;
    that.queryParams = that.route.snapshot.queryParams;
    that.gridOptions.GridLabel = that.emails.grid.routeInfo.label;
    that.dataSource.queryName = that.cryptoService.decryptString(that.queryParams.localName);
    that.dataSource.init(that.emails.grid.vwName, that.gridOptions, 'RequestedDate', 'desc', '', '');
    that.gridOptions.context = { gridComponent: that };
      that.gridOptions.onGridReady = (params) => {
         that.gridReady(params);
      }
      new agGrid.Grid(that.vw_EmailGrid.nativeElement, that.gridOptions);
      that.setBreadcrumbs();
   }

   gridReady(params?) {
      var that = this;
      let colDefs = that.createColumnDefs();
      that.dataSource.restoreColumnDefs(colDefs);

   }

  private createColumnDefs() {
    let that = this;
    return [
      AgGridHelper.actionTemplate('Action', 'Action', { cellRendererFramework: that.actionCellRenderer }),
      AgGridHelper.numberTemplate('Email Id', 'EmailId', { hide: true }),
      AgGridHelper.stringTemplate('Subject', 'Subject'),
      AgGridHelper.stringTemplate('To', 'ToAddress'),
      AgGridHelper.stringTemplate('From', 'FromAddress'),
      AgGridHelper.dateTemplate('Requested Date', 'RequestedDate'),
      AgGridHelper.dateTemplate('Sent Date', 'SentDate'),
      AgGridHelper.stringTemplate('Status', 'Status'),
      AgGridHelper.stringTemplate('Email Type', 'EmailType'),
      AgGridHelper.stringTemplate('Created By', 'UserFullName'),
      AgGridHelper.numberTemplate('<i class="fa fa-paperclip clip-font" title="No. of Attachments"></i>', 'AttachmentCount'),
    ];
  }

  actionCellRenderer(params: any) {
    if (params.data) {
      let div: HTMLDivElement = AgGridHelper.createDiv();
      let link: HTMLAnchorElement = AgGridHelper.createLink(params.data && params.data.SentDate ? 'fa-envelope-open' : 'fa-envelope', 'Open');
      link.onclick = function () {
        params.context.gridComponent.navigate(params.data.EncryptedEmailId);
      };
      div.appendChild(link);
      return div;
    }
  }

  navigate(id?: string) {
    var that = this;
    if (id) {
      that.router.navigate(['/' + that.emails.grid.routeInfo.route + '/' + that.emails.editForm.routeInfo.route], {
        queryParams: {
          id: id,
        }
      });
    }
    else {
      that.router.navigate(['/' + that.emails.grid.routeInfo.route + '/' + that.emails.addForm.routeInfo.route]);
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
      label: that.emails.grid.routeInfo.label,
      url: '',
      params: {}
    });
    that.breadcrumbService.set(breadcrumbs);
  }
}

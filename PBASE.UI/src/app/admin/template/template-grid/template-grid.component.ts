import { Component, ViewChild, OnInit, AfterViewInit, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
//service
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
//custom
import { AgGridSource } from '../../../shared/ag-grid/ag-grid-source';
import { AgGridHelper } from '../../../shared/ag-grid/ag-grid-helper';
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { Menu, pBMenus } from '../../../shared/all-routes';
import { CryptoService } from 'src/app/shared/crypto/crypto.service';
import { HttpService } from 'src/app/shared/http.service';
import { FilePickerComponent } from 'src/app/shared/file-picker/file-picker.component';
declare var agGrid: any;

@Component({
  selector: 'app-template-grid',
  templateUrl: './template-grid.component.html',
  styleUrls: ['./template-grid.component.scss']
})
export class TemplateGridComponent implements OnInit {
  gridOptions: any = {};
  queryParams: any = {};
  templates: Menu = pBMenus.find(x => x.id == "Templates");
  @ViewChild('vw_TemplateGrid') vw_TemplateGrid: ElementRef;

  constructor(
    private dataSource: AgGridSource,
    private router: Router,
    private route: ActivatedRoute,
    private cryptoService: CryptoService,
    private httpService: HttpService,
    private readonly breadcrumbService: BreadcrumbService,
    private filePickerComponent: FilePickerComponent
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    }
  }

  ngOnInit() {
    let that = this;
    that.queryParams = that.route.snapshot.queryParams;
    that.gridOptions.GridLabel = that.templates.grid.routeInfo.label;
    that.dataSource.queryName = that.cryptoService.decryptString(that.queryParams.localName);
    that.dataSource.init(that.templates.grid.vwName, that.gridOptions, 'TemplateId', 'desc', '', '');
    that.gridOptions.context = { gridComponent: that };
    let appInfo = this.httpService.getAppInfo(-9989);
    if (!appInfo.isReadonly) {
      that.gridOptions.addCallBack = function () {
        that.navigate();
      };
    }
      that.gridOptions.onGridReady = (params) => {
         that.gridReady(params);
      }
      new agGrid.Grid(that.vw_TemplateGrid.nativeElement, that.gridOptions);
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
      AgGridHelper.numberTemplate('Template Id', 'TemplateId', { hide: true }),
      AgGridHelper.numberTemplate('Templates Allowed Id', 'LookupExtraInt', { hide: true }),
      AgGridHelper.stringTemplate('Template Type', 'TemplateType'),
      AgGridHelper.stringTemplate('Description', 'Description'),
      AgGridHelper.stringTemplate('Templates Allowed', 'TemplateAllowedType'),
    ];
  }

  actionCellRenderer(params: any) {
    let div: HTMLDivElement = AgGridHelper.createDiv();
    let link: HTMLAnchorElement = AgGridHelper.createLink('fa-pencil-alt', 'Edit');
    link.onclick = function () {
      params.context.gridComponent.navigate(params.data.EncryptedTemplateId);

    };
    div.appendChild(link);
    if (params.data && params.data.AttachmentFileHandle != null) {
    }
    if (params.data && params.data.EncryptedAttachmentId != null) {
      let downloadLink: HTMLAnchorElement = AgGridHelper.createLink('fas fa-file-download', 'Download');
      downloadLink.onclick = function () {
        params.context.gridComponent.filePickerComponent.downloadAttachment(params.data.EncryptedAttachmentId);
      };
      div.appendChild(downloadLink);
      let previewLink: HTMLAnchorElement = AgGridHelper.createLink('fa fa-file', 'Preview');
      previewLink.onclick = function () {
        params.context.gridComponent.filePickerComponent.openAttachmentPreviewModel(params.data.EncryptedAttachmentId);
      };
      div.appendChild(previewLink);
    }
    return div;
  }

  navigate(id?: string) {
    var that = this;
    if (id) {
      that.router.navigate(['/' + that.templates.grid.routeInfo.route + '/' + that.templates.editForm.routeInfo.route], {
        queryParams: {
          id: id,
        }
      });
    }
    else {
      that.router.navigate(['/' + that.templates.grid.routeInfo.route + '/' + that.templates.addForm.routeInfo.route]);
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
      label: that.templates.grid.routeInfo.label,
      url: '',
      params: {}
    });
    that.breadcrumbService.set(breadcrumbs);
  }
}

import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { AgGridHelper } from '../../../shared/ag-grid/ag-grid-helper';
import { AgGridSource } from '../../../shared/ag-grid/ag-grid-source';
import { Router } from '@angular/router';
import { pBMenus, Menu } from 'src/app/shared/all-routes';
declare var agGrid: any;

@Component({
  selector: 'app-agreement-pt2-grid',
  templateUrl: './agreement-pt2-grid.component.html',
  styleUrls: ['./agreement-pt2-grid.component.css']
})
export class AgreementPt2GridComponent implements OnInit {
  private gridOptions: any = {};
  agreement: Menu = pBMenus.find(x => x.id == "Agreement");
  @ViewChild('vw_UserAgreementSubGrid') vw_UserAgreementSubGrid : ElementRef
  api: string = 'vw_UserAgreementSubGrid/';
  isCollapsed = false;

  constructor(
    private router: Router,
    private dataSource: AgGridSource) {
  }

  ngOnInit() {
    let that = this;
    //get current User's id
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));  

    that.gridOptions.GridLabel = "Agreements";
    that.dataSource.init('vw_UserAgreementSubGrid', that.gridOptions, 'AgreementId', 'desc', 'UserId', currentUser.user.Id);
    that.gridOptions.context = { gridComponent: that };
      that.gridOptions.onGridReady = (params) => {
         that.gridReady(params);
      }
      new agGrid.Grid(that.vw_UserAgreementSubGrid.nativeElement, that.gridOptions);
   }

   gridReady(params?) {
      var that = this;
      let colDefs = that.createColumnDefs();
      that.dataSource.restoreColumnDefs(colDefs);

   }

  createColumnDefs() {
    return [
      AgGridHelper.actionTemplate('Action', 'Action', { cellRendererFramework: this.renderActionLink }),
      AgGridHelper.numberTemplate('User Agreement Id', 'UserAgreementId', { hide: true }),
      AgGridHelper.numberTemplate('User Id', 'UserId', { hide: true }),
      AgGridHelper.numberTemplate('Agreement Id', 'AgreementId', { hide: true }),
      AgGridHelper.numberTemplate('Original Agreement Id', 'OriginalAgreementId', { hide: true }),
      AgGridHelper.stringTemplate('Agreement Name', 'AgreementHeader'),
      AgGridHelper.decimalTemplate('Version No', 'VersionNo'),
      AgGridHelper.dateTemplate('Agreement Date', 'AgreementDate'),
      AgGridHelper.booleanTemplate('Accepted', 'IsAcceptDecline'),
      AgGridHelper.dateTemplate('Response Date', 'AcceptDeclineDate'),
    ];
  }

  renderActionLink(params: any) {
    let div: HTMLDivElement = document.createElement('div');
    let link: HTMLAnchorElement = document.createElement('a');
    div.className = "text-center";
    link.className = "anchor-color";
    link.innerHTML = "<i class='fa fa-pencil-alt' title='Edit'></i></a>";
    link.onclick = function () {
      if (params.data && params.data.AgreementId) {
        params.context.gridComponent.navigate(params.data.EncryptedAgreementId);
      }
    };
    div.appendChild(link);
    return div;
  }

   reload() {
      let that = this;
      that.gridReady();
   }

  navigate(id?: string) {
    var that = this;
    that.router.navigate(["/profile/view-agreement"],
      {
        queryParams: {    
          id: id
        }
      });
  }

}

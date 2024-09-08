import { Component, OnInit, ElementRef, ViewChild, Input, Output, EventEmitter } from '@angular/core';
import { DatatableComponent } from '@swimlane/ngx-datatable';
//service
import { ToastrService } from 'ngx-toastr';
import { HttpService } from 'src/app/shared/http.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
//custom
import { InfoMessage } from 'src/app/shared/all-routes';
import { ConfirmationDialogService } from 'src/app/shared/confirmation-dialog/confirmation-dialog.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-group-dashboard-table',
  templateUrl: './user-group-dashboard-table.component.html',
  styles: []
})
export class UserGroupDashboardTableComponent implements OnInit {
  api: string = 'UserGroupDashboardOptionTable/';
  data: any = {};
  @Input() isEditClickedDashboard = false;
  @Output() isClickedDashboard = new EventEmitter();
  IsEdited: boolean = false;
  updatedRows: any[] = [];
  queryParams: any = {};
  rows: any[] = [];
  tempRows: any[] = [];
  tableOptions: any = {
    HeaderHeight: '50',
    FooterHeight: '50',
    Limit: '4',
    ColumnMode: 'force',
    RowHeight: 'auto',
  };
  public config: PerfectScrollbarConfigInterface = {};
  @ViewChild(DatatableComponent) pbTable: DatatableComponent;
  appInfo: any = {};

  constructor(private httpService: HttpService,
    private route: ActivatedRoute,
    private modalService: NgbModal,
    private confirmationDialogService: ConfirmationDialogService,
    private toastr: ToastrService) {
  }

  ngOnInit() {
    let that = this;
    that.appInfo = this.httpService.getAppInfo(-9985);
    that.queryParams = that.route.snapshot.queryParams;
    that.get();
  }
  //api functions
  get() {
    let that = this;
    that.httpService.get(that.api + that.queryParams.id)
      .subscribe((res: any) => {
        that.data = res;
        if (res.Rows != null) {
          res.Rows.forEach((record, index) => {
            record.IsEdit = false;
          });
        }
        else {
          res.Rows = [];
        }
        that.tempRows = that.copy(res.Rows);

        that.rows = res.Rows;
      });
  }

  //table functions
  onCellChanged(row: any) {
    let that = this;
    let record = that.updatedRows.find(item => item.UniqueId == row.UniqueId);
    record ? Object.assign(record, row) : that.updatedRows.push(row);
  }

  onBulkEdit(value: boolean) {
    var table = document.getElementById("dashboardTable");
    table.classList.add("custom-table");
    let that = this;
    if (that.isEditClickedDashboard) {
      that.isEditClickedDashboard = false;
    }
    else {
      that.isEditClickedDashboard = true;
    }
    this.isClickedDashboard.emit({ isEditClickedDashboard: that.isEditClickedDashboard });
    that.IsEdited = value;
    that.updatedRows = [];
    that.rows = that.copy(that.tempRows);
  }

  onUpdateRows() {
    let that = this;
    var table = document.getElementById("dashboardTable");
    table.classList.remove("custom-table");
    that.updateRows(that.updatedRows).subscribe(
      (res: any) => {
        that.toastr.success(InfoMessage.SaveMessage);
        that.onBulkEdit(false);
        that.get();
      }, error => {
        that.toastr.error(InfoMessage.ErrorMessage);
      });

  }

  updateRows(updatedRows: any) {
    let that = this;
    that.data.UpdatedRows = updatedRows;
    return that.httpService.put(that.api, that.data);
  }

  onActivate(event: any) {
    if (event.type == 'click' && event.column.name != 'Actions')
      event.row.IsEdit = true;
  }

  onSave(row: any) {
    let that = this;
    if (row) {
      that.onUpdateRows();
    } else {
      that.toastr.success(InfoMessage.ErrorMessage);
    }
  }

  onCancel() {
    let that = this;
    var table = document.getElementById("dashboardTable");
    table.classList.remove("custom-table");
    if (that.updatedRows.length > 0) {
      that.confirmationDialogService
        .confirm('Warning - Unsaved Changes',
          '<p>You have made changes to Dashboard Objects table which have not been saved!</p><p>Do you want to discard your changes?</p>',
          'Yes', 'No').then((confirmed) => {
            if (confirmed === true) {
              that.IsEdited = false;
              this.isClickedDashboard.emit({ isEditClickedDashboard: false });
            }
          });
    }
    else {
      that.IsEdited = false;
      this.isClickedDashboard.emit({ isEditClickedDashboard: false });
    }
  }

  copy(records: any[]) {
    let result = [];
    for (var i = 0, len = records.length; i < len; i++) {
      result[i] = {}; // empty object to hold properties added below
      for (var prop in records[i]) {
        result[i][prop] = records[i][prop]; // copy properties from arObj to ar2
      }
    }
    return result;
  }

  getRowClass(row: any) {
    if (row.DashboardObjectType == 'Grid*') {
      return {
        'white-bg': true
      };
    }
  }
}






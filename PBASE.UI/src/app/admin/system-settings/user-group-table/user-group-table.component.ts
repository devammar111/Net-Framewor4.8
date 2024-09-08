import { Component, OnInit, ElementRef, ViewChild, Input, Output, EventEmitter } from '@angular/core';
import { DatatableComponent } from '@swimlane/ngx-datatable';
//service
import { ToastrService } from 'ngx-toastr';
import { HttpService } from 'src/app/shared/http.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
//custom
import { InfoMessage } from 'src/app/shared/all-routes';
import { ActivatedRoute } from '@angular/router';
import { ConfirmationDialogService } from 'src/app/shared/confirmation-dialog/confirmation-dialog.service';

@Component({
  selector: 'app-user-group-table',
  templateUrl: './user-group-table.component.html',
  styles: []
})
export class UserGroupTableComponent implements OnInit {
  api: string = 'UserGroupMenuOptionTable/';
  @Input() isEditClicked = false;
  @Output() isClicked = new EventEmitter();
  data: any = {};
  columns = [];
  IsEdited: boolean = false;
  updatedRows: any[] = [];
  queryParams: any = {};
  rows: any[] = [];
  originalData: any[] = [];
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
        that.data.vw_LookupAccessTypeMenue = that.filterItems(res.vw_LookupAccessType, -9977);
        that.data.vw_LookupAccessTypeTab = that.filterItems(res.vw_LookupAccessType, -9976);
        that.data.vw_LookupAccessTypeButton = that.filterItems(res.vw_LookupAccessType, -9973);
        if (that.tempRows.length > 0) {
          let rows = res.Rows.filter(item => that.tempRows.find(x => x.ObjectName == item.ObjectName) ? true : false);
          that.tempRows = that.copy(rows);
          that.rows = rows;
        }
        else {
          that.rows = res.Rows;
        }
        that.originalData = that.copy(res.Rows);
      });
  }
  filterItems(arr, query) {
    var resultArray = [];
    for (let i = 0; i < arr.length; i++) {
      if (query == -9977) {
        if (arr[i].LookupExtraInt == 1) {
          resultArray.push(arr[i]);
        }
        else if (arr[i].LookupExtraInt == 3) {
          resultArray.push(arr[i]);
        }
      }
      else if (query == -9976 || query == -9975 || query == -9974) {
        if (arr[i].LookupExtraInt == 2) {
          resultArray.push(arr[i]);
        }
        else if (arr[i].LookupExtraInt == 3) {
          resultArray.push(arr[i]);
        }
      }
      else if (query == -9973) {
        if (arr[i].LookupExtraInt == 2) {
          resultArray.push(arr[i]);
        }
      }

    }
    return resultArray;
  }

  //table functions
  onCellChanged(row: any) {
    let that = this;
    let record = that.updatedRows.find(item => item.UniqueId == row.UniqueId);
    record ? Object.assign(record, row) : that.updatedRows.push(row);
  }

  onBulkEdit(value: boolean) {
    var table = document.getElementById("objectTable");
    table.classList.add("custom-table");
    let that = this;
    if (that.isEditClicked) {
      that.isEditClicked = false;
    }
    else {
      that.isEditClicked = true;
    }
    this.isClicked.emit({ isEditClicked: this.isEditClicked });
    that.IsEdited = value;
    that.updatedRows = [];
  }

  onUpdateRows() {
    let that = this;
    var table = document.getElementById("objectTable");
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
    var table = document.getElementById("objectTable");
    table.classList.remove("custom-table");
    if (that.updatedRows.length > 0) {
      that.confirmationDialogService
        .confirm('Warning - Unsaved Changes',
          '<p>You have made changes to Objects table which have not been saved!</p><p>Do you want to discard your changes?</p>',
          'Yes', 'No').then((confirmed) => {
            if (confirmed === true) {
              if (that.tempRows.length > 0) {
                that.rows = that.copy(that.tempRows);
              }
              else {
                that.rows = that.copy(that.originalData);
              }
              that.IsEdited = false;
              this.isClicked.emit({ isEditClicked: false });
            }
          });
    }
    else {
      if (that.tempRows.length > 0) {
        that.rows = that.copy(that.tempRows);
      }
      else {
        that.rows = that.copy(that.originalData);
      }
      that.IsEdited = false;
      this.isClicked.emit({ isEditClicked: false });
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
    if (row.ObjectType == 'Tab/Form') {
      return {
        'light-gray-bg': true
      };
    }
    else if (row.ObjectType == 'Button') {
      return {
        'dark-gray-bg': true
      };
    }
    else if (row.ObjectType === 'Section') {
      return {
        'mid-gray-bg': true
      };
    }
    else if (row.ObjectType === 'Field') {
      return {
        'gray-bg': true
      };
    }
    else if (row.ObjectType === 'Menu Option') {
      return {
        'white-bg': true
      };
    }
  }

  updateFilter(event) {
    let that = this;
    const val = event.target.value.toLowerCase();

    // filter our data
    that.tempRows = that.originalData.filter(function (rows) {
      if ((rows.ObjectType && rows.ObjectType.toLowerCase().indexOf(val) !== -1) ||
        (rows.ObjectName && rows.ObjectName.toLowerCase().indexOf(val) !== -1) ||
        (rows.MenuName && rows.MenuName.toLowerCase().indexOf(val) !== -1) ||
        (rows.AccessType && rows.AccessType.toLowerCase().indexOf(val) !== -1) ||
        !val) {
        return true;
      }
      return false;
    });

    // update the rows
    that.rows = that.copy(that.tempRows);
    that.pbTable.offset = 0;
  }

}






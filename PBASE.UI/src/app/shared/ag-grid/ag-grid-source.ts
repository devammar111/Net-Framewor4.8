import { Injectable } from '@angular/core';
import { formatDate } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { RequestOptions } from '@angular/http';
import { NGXLogger } from 'ngx-logger';
import { ConfirmationDialogService } from 'src/app/shared/confirmation-dialog/confirmation-dialog.service';
import { Observable } from 'rxjs/Rx';
import 'rxjs/Rx'; //get everything from Rx  
import 'rxjs/add/operator/toPromise';
import { ToastrService } from 'ngx-toastr';
declare var saveAs: any;

@Injectable()
export class AgGridSource {
  private viewName: string = "";
  public queryName: string = "";
  private gridLabel: string = "";
  private sortDefaultColumn: string = "";
  private firstTime: boolean = false;
  private gridOptions: any = {};
  private excelfilterModel: any;
  private defaultFilterByFieldName: any;
  private defaultFilterByFieldValue: string = "";
  private defaultFilterType: any;
  private filters: any = {};
  private data1: any = {};
  private columnDefs: any;
  private gs_state: any = {};
  private sortOptions: any = {};
  private pagingOptions: any = {
    pageSizes: [5, 10, 20, 30, 50, 100, 250, 500],
    pageSize: 20,
    currentPage: 1
  };
  public http: HttpClient;

  constructor(http: HttpClient,
    private toastr: ToastrService,
    private confirmationDialogService: ConfirmationDialogService,
    private logger: NGXLogger) {
    this.http = http;
  }

  init(viewName: string, gridOptions: any, sortColumn: string, sortOrder: string, defaultFilterByFieldName: any, defaultFilterByFieldValue: any, defaultFilterType?: string) {
    let that = this;
    that.firstTime = true;
    that.viewName = viewName;
    that.gridLabel = gridOptions.GridLabel;
    that.sortOptions.colId = sortColumn;
    that.sortDefaultColumn = sortColumn;
    that.sortOptions.sort = sortOrder;
    if (defaultFilterByFieldName) {
      that.defaultFilterByFieldName = defaultFilterByFieldName;
      that.defaultFilterByFieldValue = defaultFilterByFieldValue;
      that.defaultFilterType = defaultFilterType;
    }
    else {
      that.defaultFilterByFieldName = null;
      that.defaultFilterByFieldValue = null;
      that.defaultFilterType = "eq";
    }
    if (!that.queryName || that.queryName != that.viewName) {
      that.deleteLocalState();
    }
    let localState = JSON.parse(that.getLocalState());
    if (localState) {
      that.pagingOptions.currentPage = localState.page;
      that.pagingOptions.pageSize = localState.pageSize;
      gridOptions.namedState = localState.stateId;
    }
    else {
      that.pagingOptions.currentPage = 1;
      that.pagingOptions.pageSize = 20;
    }
    //Properties
    gridOptions.enableServerSideFilter = true;
    gridOptions.enableServerSideSorting = true;
    gridOptions.enableColResize = true;
    gridOptions.rowModelType = 'infinite';
    gridOptions.pagination = true;
    gridOptions.paginationAutoPageSize = false;
    gridOptions.paginationPageSize = that.pagingOptions.pageSize;
    gridOptions.cacheBlockSize = gridOptions.paginationPageSize;
    gridOptions.maxBlocksInCache = 0;
    gridOptions.domLayout = 'autoHeight';
    gridOptions.overlayNoRowsTemplate = '<span>No rows to show</span>';
    gridOptions.gridLabel = gridOptions.gridLabel;
    gridOptions.isShowFullMenu = gridOptions.isShowFullMenu == false ? gridOptions.isShowFullMenu : true;
    gridOptions.toolPanelSuppressPivotMode = true;
    gridOptions.toolPanelSuppressRowGroups = true;
    gridOptions.toolPanelSuppressValues = true;
    gridOptions.suppressCsvExport = true;
    gridOptions.suppressExcelExport = true;
    gridOptions.rowHeight = 30;
    var currentUser = JSON.parse(localStorage.getItem('currentUser'));
    if (currentUser.user.UserTypeId == 3 || currentUser.user.UserTypeId == 1) {
      gridOptions.isGlobalBtn = true;
    }
    else {
      gridOptions.isGlobalBtn = false;
    }
    //Grid Functions
    gridOptions.onFilterChanged = function () {
      that.setLocalState(false, false, true, null);
    };
    gridOptions.onSortChanged = function () {
      that.sortOptions.sort = that.gridOptions.api.getSortModel().length == 0 ? "asc" : that.gridOptions.api.getSortModel()[0].sort;
      if (that.sortOptions.sort == "asc") {
        that.sortOptions.colId = that.sortDefaultColumn;
      }
      that.setLocalState(false, true, false);
    };
    gridOptions.onPaginationChanged = function () {
      that.setLocalState(true, false, false);
    }
    //Custom Functions
    gridOptions.reloadGridCallback = function () {
      that.deleteLocalState();
      that.reload();
    };
    gridOptions.loadNamedState = function () {
      return that.loadNamedState();
    };
    gridOptions.exportCallback = function (val, paginationElement) {
      var currentDate = formatDate(new Date(), 'yyyyMMddhhmmss', 'en');
      that.exportExcel(val, paginationElement, currentDate)
        .then(function (data) {
          let b64toBlob = function (b64Data, contentType, sliceSize?) {
            contentType = contentType || '';
            sliceSize = sliceSize || 512;

            let byteCharacters = atob(b64Data);
            let byteArrays = [];

            for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
              let slice = byteCharacters.slice(offset, offset + sliceSize);

              let byteNumbers = new Array(slice.length);
              for (let i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
              }

              let byteArray = new Uint8Array(byteNumbers);

              byteArrays.push(byteArray);
            }

            let blob = new Blob(byteArrays, { type: contentType });
            return blob;
          }
          let blob = b64toBlob(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
          saveAs(blob, that.gridLabel + "_GRID_" + currentDate + '.xlsx');
        });
    }
    gridOptions.saveStateCallback = function () {
      that.saveState();
    };
    gridOptions.clearStateCallback = function () {
      that.clearState()
        .then(function () {
          that.reload();
        });
    };
    gridOptions.saveNamedStateCallback = function (val, paginationElement) {
      if (val || paginationElement.drNamedState.value) {
        that.saveNameState(val, paginationElement);
      }

    };
    gridOptions.clearNamedStateCallback = function (val, paginationElement) {
      val ? that.confirmationDialogService
        .confirm('Warning - Delete Grid Settings', "Are you sure you want to delete grid settings?")
        .then((confirmed) => {
          if (confirmed === true) {
            that.clearNameState(val, paginationElement)
              .then(function () {
                that.reload();
              });
          }
          else {

          }
        }) : true;
    };
    gridOptions.defaultGridSettingCallback = function (val, paginationElement) {
      that.defaultGridSetting(val, paginationElement)
        .then(function () {
          that.reload();
        });
    };
    gridOptions.globalCallback = function (val, paginationElement) {
      that.globalSetting(val, paginationElement)
        .then(function () {
          that.reload();
        });
    };
    gridOptions.namedStateCallback = function (val, paginationElement) {
      if (val) {
        that.gridOptions.DropVal = val;
        that.nameState(val, paginationElement);
      }
    };
    that.gridOptions = gridOptions;
  }

  reload() {
    let that = this;
    if (that.gridOptions.context && that.gridOptions.context.gridComponent && that.gridOptions.context.gridComponent.reload) {
      that.gridOptions.context.gridComponent.reload();
    }
  }

  ProcessData(isStateChanged?: boolean) {
    let that = this;
    let filter = [];
    let sort = [];
    let localState = JSON.parse(that.getLocalState());
    if (localState && that.queryName && that.queryName == that.viewName && !isStateChanged) {
      Object.keys(localState.sortModel).map(function (key) {
        sort.push({ [key]: localState.sortModel[key] })
        return sort;
      });
      if (sort.length != 0) {
        that.gridOptions.api.setSortModel(localState.sortModel);
      }
      Object.keys(localState.filterModel).map(function (key) {
        filter.push({ [key]: localState.filterModel[key] })
        return filter;
      });
      if (filter.length != 0) {
        that.gridOptions.api.setFilterModel(localState.filterModel);
      }
    }
    let dataSource = {
      rowCount: null,
      pageSize: that.gridOptions.api.paginationGetCurrentPage() + 1,
      getRows: (params: any) => {
        let sortModel = params.sortModel;
        let filterModel = params.filterModel;
        that.excelfilterModel = params.filterModel;
        if (sortModel && sortModel.length > 0) {
          that.sortOptions = sortModel[0];
        }
        if (filterModel) {
          that.filters.groupOp = "AND";
          that.filters.rules = [];
          for (let columnName in filterModel) {

            let column = that.gridOptions.columnApi.getColumn(columnName).getColDef();
            let type = filterModel[columnName].type;
            let data = filterModel[columnName].filter;

            if (column.filter == 'agTextColumnFilter') {
              if (type == 'blanks') {
                that.filters.rules.push({
                  field: columnName,
                  op: that.getNumberOperator(type),
                  data: null
                });
              }
              else {
                that.filters.rules.push({
                  field: columnName,
                  op: that.getTextOperator(type),
                  data: data
                });
              }
            }
            else if (column.filter == 'agNumberColumnFilter') {
              if (type !== 'inRange' && type != 'blanks') {
                that.filters.rules.push({
                  field: columnName,
                  op: that.getNumberOperator(type),
                  data: data
                });
              }
              else if (type == 'blanks') {
                that.filters.rules.push({
                  field: columnName,
                  op: that.getNumberOperator(type),
                  data: null
                });
              }
              else if (type == 'inRange') {
                that.filters.rules.push({
                  field: columnName,
                  op: 'ge',
                  data: filterModel[columnName].filter
                });
                that.filters.rules.push({
                  field: columnName,
                  op: 'le',
                  data: filterModel[columnName].filterTo
                });
              }
            }
            else if (column.filter == 'agDateColumnFilter') {
              if (type !== 'inRange' && type != 'blanks') {
                that.filters.rules.push({
                  field: columnName,
                  op: that.getNumberOperator(type),
                  data: filterModel[columnName].dateFrom
                });
              }
              else if (type == 'blanks') {
                that.filters.rules.push({
                  field: columnName,
                  op: that.getNumberOperator(type),
                  data: null
                });
              }
              else if (type == 'inRange') {
                that.filters.rules.push({
                  field: columnName,
                  op: 'ge',
                  data: filterModel[columnName].dateFrom
                });
                that.filters.rules.push({
                  field: columnName,
                  op: 'le',
                  data: filterModel[columnName].dateTo
                });
              }
            }
            else if (column.filter == 'agTimeColumnFilter') {
              if (type !== 'inRange' && type != 'blanks') {
                that.filters.rules.push({
                  field: columnName,
                  op: that.getNumberOperator(type),
                  data: filterModel[columnName].timeFrom
                });
              }
              else if (type == 'blanks') {
                that.filters.rules.push({
                  field: columnName,
                  op: that.getNumberOperator(type),
                  data: null
                });
              }
              else if (type == 'inRange') {
                that.filters.rules.push({
                  field: columnName,
                  op: 'ge',
                  data: filterModel[columnName].timeFrom
                });
                that.filters.rules.push({
                  field: columnName,
                  op: 'le',
                  data: filterModel[columnName].timeTo
                });
              }
            }
            else if (column.filter == 'agSetColumnFilter') {
              let availableItems = column.filterParams.values;
              let selectedItems = params.filterModel[columnName];
              for (let i = 0; i < availableItems.length; i++) {
                if (selectedItems.indexOf(availableItems[i]) == -1) {
                  let value = availableItems[i];
                  if (column.columnType == 'Boolean') {
                    if (value == 'Yes') {
                      value = true;
                    }
                    else if (value == 'No') {
                      value = false;
                    }
                    else {
                      value = null;
                    }
                  }
                  else if (column.columnType == 'Dropdown') {
                    value = value == 'Blanks' ? null : value;
                  }
                  that.filters.rules.push({
                    field: columnName,
                    op: 'ne',
                    data: value
                  });
                }
              }
            }
          }
        }
        if (that.defaultFilterByFieldName !== undefined && that.defaultFilterByFieldName != null) {
          const filterExistingTypes = ["eq", "ne", "ge", "le", "cn"];
          const isFilterExists = filterExistingTypes.indexOf(that.defaultFilterType);
          if (typeof that.defaultFilterByFieldName == 'string') {
            that.filters.rules.push({
              field: that.defaultFilterByFieldName,
              op: (isFilterExists > -1 ? that.defaultFilterType : 'eq'),
              data: that.defaultFilterByFieldValue
            });
          }
          else {
            for (let key in that.defaultFilterByFieldName) {
              that.filters.rules.push({
                field: key.toString(),
                op: 'eq',
                data: that.defaultFilterByFieldName[key]
              });
            }
          }
        }
        let gridParams = {
          page: that.gridOptions.api.paginationGetCurrentPage() + 1,
          pageSize: that.gridOptions.api.paginationGetPageSize(),
          colId: that.sortOptions.colId,
          sort: that.sortOptions.sort,
          filters: JSON.stringify(that.filters)
        };
        that.gridOptions.api.showLoadingOverlay();
        that.find(gridParams)
          .subscribe(data => {
            let rowsThisPage = data.Results;
            params.successCallback(rowsThisPage, data.RowCount);
            that.gridOptions.api.hideOverlay();
            if (data.Results.length == 0) {
              that.gridOptions.api.showNoRowsOverlay()
            }
            if (that.firstTime && that.pagingOptions.currentPage != 1 && that.pagingOptions.currentPage != that.gridOptions.api.paginationGetCurrentPage() + 1) {
              that.gridOptions.api.paginationGoToPage(that.pagingOptions.currentPage - 1);
              that.firstTime = false;
            }
          });
      }
    };
    that.gridOptions.api.setDatasource(dataSource);
  };

  find(data: any): Observable<any> {
    let that = this;
    return that.http.get(that.viewName, { params: data })
      .map((res) => res)
      .catch(that.handleError);
  }

  getTextOperator(type: string) {
    let op = 'cn';
    switch (type) {
      case 'contains':
        op = 'cn';
        break;
      case 'equals':
        op = 'eq';
        break;
      case 'blanks':
        op = 'eq';
        break;
      default:
        op = 'cn';
    }
    return op;
  };

  getNumberOperator(type: string) {
    let op = 'eq';
    switch (type) {
      case 'equals':
        op = 'eq';
        break;
      case 'lessThanOrEqual':
        op = 'le';
        break;
      case 'greaterThanOrEqual':
        op = 'ge';
        break;
      default:
        op = 'eq';
    }
    return op;
  };

  restoreColumnDefs(columnDefs: any) {
    let isAction = false;
    let that = this;
    that.columnDefs = columnDefs;
    let localState = that.getLocalState();
    let gridState;
    that.gridOptions.api.setColumnDefs(columnDefs);
    that.getState(localState)
      .then(function (data: any) {
        if (data != null) {
          that.gridOptions.IsDefalut = data.IsDefalut;
          if (data.InternalGridSettingId) {
            that.gridOptions.namedState = data.InternalGridSettingId;
          }
          var currentgridState = JSON.parse(localState);
          let filter_model = [];
          let sort_model = [];
          if (currentgridState) {
            Object.keys(currentgridState.filterModel).map(function (key) {
              filter_model.push({ [key]: currentgridState.filterModel[key] })
              return filter_model;
            });

            Object.keys(currentgridState.sortModel).map(function (key) {
              sort_model.push({ [key]: currentgridState.filterModel[key] })
              return sort_model;
            });
          }
          if ((currentgridState && currentgridState.page == 1 && sort_model.length == 0 && filter_model.length == 0 && currentgridState.stateId || data.InternalGridSettingId) && that.gridOptions.isShowFullMenu != false) {
            let sdata = ((data.StorageData) ? JSON.parse(data.StorageData) : data);
            if (sdata) {
              try {
                gridState = JSON.parse(sdata);
              } catch (e) {
                gridState = sdata;
              }
              if (gridState.Sort.length > 0) {
                that.sortOptions.colId = gridState.Sort[0].colId;
                that.sortOptions.sort = gridState.Sort[0].sort;
                that.gridOptions.api.setSortModel(gridState.Sort);
              }
              if (gridState.pageSize) {
                that.gridOptions.paginationAutoPageSize = true;
                that.gridOptions.paginationPageSize = gridState.pageSize;
                that.gridOptions.cacheBlockSize = gridState.pageSize;
              }
              that.filters = JSON.parse(gridState.AppliedFilters);
              that.gridOptions.api.setFilterModel(gridState.Filter);
            }
          }
          else {
            try {
              gridState = JSON.parse(data);
            } catch (e) {
              gridState = JSON.parse(data.StorageData);
            }

          }
          if (gridState.Column[0].colId != "Action") {
            for (let i = 0; i < gridState.Column.length; i++) {
              if (gridState.Column[i].colId == "Action") {
                for (let j = i; j > 0; j--) {
                  gridState.Column[j] = gridState.Column[j - 1];
                }
                gridState.Column[0] = { "colId": "Action", "hide": false, "aggFunc": null, "width": 55, "pivotIndex": null, "pinned": "left", "rowGroupIndex": null };
                isAction = true;
              }
              if (isAction) {
                break;
              }
            }
          }
          if (gridState.Column.length != that.columnDefs.length) {
            if (gridState.Column.length < that.columnDefs.length)
              for (let i = 0; i < that.columnDefs.length; i++) {
                let column = gridState.Column.find(x => x.colId == that.columnDefs[i].field);
                if (!column) {
                  gridState.Column.push({ "colId": that.columnDefs[i].field, "hide": true, "aggFunc": null, "width": 160, "pivotIndex": null, "pinned": null, "rowGroupIndex": null });
                }
              }
          }
          that.gridOptions.columnApi.setColumnState(gridState.Column);
        }
        else {
          that.gridOptions.IsDefalut = false;
          that.gridOptions.namedState = null;
        }
        that.ProcessData();
      });
  };

  saveState() {
    let isAction = false;
    let that = this;
    let dataStorage;
    let data = {
      StorageKey: "gs_" + that.viewName,
      StorageData: {
        page: that.gridOptions.api.paginationGetCurrentPage() + 1,
        pageSize: that.gridOptions.api.paginationGetPageSize(),
        Column: that.gridOptions.columnApi.getColumnState(),
        Sort: that.gridOptions.api.getSortModel(),
        Filter: that.gridOptions.api.getFilterModel(),
        AppliedFilters: JSON.stringify(that.filters)
      }
    };
    if (data.StorageData.Column[0].colId != "Action") {
      for (let i = 0; i < data.StorageData.Column.length; i++) {
        if (data.StorageData.Column[i].colId == "Action") {
          for (let j = i; j > 0; j--) {
            data.StorageData.Column[j] = data.StorageData.Column[j - 1];
          }
          data.StorageData.Column[0] = { "colId": "Action", "hide": false, "aggFunc": null, "width": 55, "pivotIndex": null, "pinned": "left", "rowGroupIndex": null };
          isAction = true;
        }
        if (isAction) {
          break;
        }
      }
    }
    dataStorage = JSON.stringify(data.StorageData);
    data.StorageData = dataStorage;
    let options = new RequestOptions({ params: data });
    that.http.post("GridState/Save", JSON.stringify(data))
      .toPromise()
      .then(
        response => {   //Success
          that.logger.log('Grid state saved.');
        },
        msg => {    //Error
          that.logger.log(msg.statusText || 'Server error');
        });
  }

  clearState() {
    let that = this;
    let storageKey = "gs_" + that.viewName;
    const promise = new Promise((resolve) => {
      that.http.delete("GridState/Delete?id=" + storageKey)
        .toPromise()
        .then(
          response => {   //Success
            that.logger.log('Grid state cleared.');
            resolve();
          },
          msg => {    //Error
            that.logger.log(msg.statusText || 'Server error');
          });
    });
    return promise;
  }

  getState(localState) {
    let that = this;
    let url = "";
    var storageKey = "gs_" + that.viewName;
    var localGridStateObject = JSON.parse(localState);
    if (that.gridOptions.isShowFullMenu != false) {
      if (localState != null && localGridStateObject.stateId) {
        url = url + "GridState/GetSingleFirst?id=" + localGridStateObject.stateId;
      }
      else if (that.gridOptions.DropVal == -1) {
        url = url + "GridState/GetSingleFirst?id=" + -1;
      }
      else {
        url = url + "GridState/GetDefaultFirst?id=" + storageKey;
      }
    }
    else {
      if (that.gridOptions.DropVal == -1 && that.gridOptions.isShowFullMenu != false) {
        url = url + "GridState/GetSingleFirst?id=" + -1;
      }
      else {
        url = url + "GridState/Get?id=" + storageKey;
      }

    }
    //let storageKey = "gs_" + that.viewName;
    const promise = new Promise((resolve) => {
      that.http.get(url)
        .toPromise()
        .then(
          response => {   //Success
            that.logger.log('Grid state loaded for ' + storageKey);
            let data = response;
            resolve(data);
          },
          msg => {    //Error
            that.logger.log(msg.statusText || 'Server error');
          });
    });
    return promise;
  }

  loadNamedState() {
    let that = this;
    let promise = new Promise((resolve, reject) => {
      let storageKey = "gs_" + that.viewName;
      that.http.get("GridState/GetAll?id=" + storageKey)
        .toPromise()
        .then(
          res => { // Success
            var data = { data: res, isShowFullMenu: that.gridOptions.isShowFullMenu, isGlobalBtn: that.gridOptions.isGlobalBtn, IsDefalut: that.gridOptions.IsDefalut }
            resolve(data);
          },
          msg => { // Error
            reject(msg);
          }
        );
    });
    return promise;
  }

  saveNameState(val, paginationElement) {
    let isAction = false;
    let url = "";
    let that = this;
    let dataStorage;
    let data = {
      StorageKey: "gs_" + that.viewName,
      StorageData: {
        page: that.gridOptions.api.paginationGetCurrentPage() + 1,
        pageSize: that.gridOptions.api.paginationGetPageSize(),
        Column: that.gridOptions.columnApi.getColumnState(),
        Sort: that.gridOptions.api.getSortModel(),
        Filter: that.gridOptions.api.getFilterModel(),
        AppliedFilters: JSON.stringify(that.filters)
      },
      StateName: "",
      StateId: "",
      IsDefault: "",
      IsGlobal: "",
    };
    if (data.StorageData.Column[0].colId != "Action") {
      for (let i = 0; i < data.StorageData.Column.length; i++) {
        if (data.StorageData.Column[i].colId == "Action") {
          for (let j = i; j > 0; j--) {
            data.StorageData.Column[j] = data.StorageData.Column[j - 1];
          }
          data.StorageData.Column[0] = { "colId": "Action", "hide": false, "aggFunc": null, "width": 55, "pivotIndex": null, "pinned": "left", "rowGroupIndex": null };
          isAction = true;
        }
        if (isAction) {
          break;
        }
      }
    }
    dataStorage = JSON.stringify(data.StorageData);
    data.StorageData = dataStorage;
    if (val) {
      url = "GridState/Save";
      data.StateName = val;
      data.IsDefault = paginationElement.isDefaultCheckbox.checked;
      data.IsGlobal = paginationElement.isGlobalCheckbox.checked;
    }
    else if (paginationElement.drNamedState.value) {
      url = "GridState/Update";
      data.StateId = paginationElement.drNamedState.value;
    }
    let bodyString = JSON.stringify(data);
    let promise = new Promise((resolve) => {
      that.http.post(url, bodyString)
        .toPromise()
        .then(
          response => {   //Success
            that.data1 = response;
            var data = ((that.data1._body) ? JSON.parse(that.data1._body) : that.data1);
            if (data.InternalGridSettingId != null) {
              that.gridOptions.namedState = data.InternalGridSettingId;
              var gridState = JSON.parse(data.StorageData);
              that.gridOptions.columnApi.setColumnState(gridState.Column);
              if (gridState.Sort.length > 0) {
                that.sortOptions.colId = gridState.Sort[0].colId;
                that.sortOptions.sort = gridState.Sort[0].sort;
                that.gridOptions.api.setSortModel(gridState.Sort);
              }
              if (gridState.pageSize) {
                that.gridOptions.paginationAutoPageSize = true;
                that.gridOptions.paginationPageSize = gridState.pageSize;
                that.gridOptions.cacheBlockSize = gridState.pageSize;
              }
              that.filters = JSON.parse(gridState.AppliedFilters);
              that.gridOptions.api.setFilterModel(gridState.Filter);
              that.logger.log(that.viewName + ' grid settings saved.');
              that.setLocalState(false, false, false, data.InternalGridSettingId);
              paginationElement.namedValue.value = null;
              paginationElement.isDefaultCheckbox.checked = false;
              paginationElement.isGlobalCheckbox.checked = false;
              that.reload();
            }
            else {
              paginationElement.btSaveNamedState.className = 'ag-paging-button show';
              paginationElement.btClearNamedState.className = 'ag-paging-button show';
              paginationElement.refGlobal.className = '';
              paginationElement.btDefaultGridSetting.className = 'ag-paging-button show';
              alert(data);
            }
          },
          msg => {
            that.toastr.error(msg.error.Message, "Error");//Error
            that.logger.log(msg.statusText || 'Server error');
            paginationElement.btSaveNamedState.className = 'ag-paging-button show';
            paginationElement.btClearNamedState.className = 'ag-paging-button show';
            paginationElement.refGlobal.className = '';
            paginationElement.btDefaultGridSetting.className = 'ag-paging-button show';
          });
    });
    return promise;
  }

  clearNameState(val, paginationElement) {
    let that = this;
    let storageKey = "gs_" + that.viewName;
    const promise = new Promise((resolve) => {
      that.http.delete("GridState/Delete?id=" + val)
        .toPromise()
        .then(
          response => {   //Success
            this.deleteLocalState();
            that.logger.log('Grid state loaded for ' + storageKey);
            that.reload();
          },
          msg => {    //Error
            if (msg.status = "404") {
              that.toastr.error(msg.error.Message, "Error");
            }
            else {
              that.toastr.error(msg.error.Message, "Error");
            }
            that.logger.log(msg.statusText || 'Server error');
          });
    });
    return promise;
  }

  defaultGridSetting(val, paginationElement) {
    let that = this;
    let storageKey = "gs_" + that.viewName;
    const promise = new Promise((resolve) => {
      that.http.delete("GridState/DefaultGridSetting?id=" + val + "&storageKey=" + storageKey)
        .toPromise()
        .then(
          response => {   //Success
            //this.deleteLocalState();
            //that.logger.log('Grid state loaded for ' + storageKey);
            that.reload();
          },
          msg => {    //Error
            if (msg.status = "404") {
              that.toastr.error(msg.error.Message, "Error");
            }
            else {
              that.toastr.error(msg.error.Message, "Error");
            }
            that.logger.log(msg.statusText || 'Server error');
          });
    });
    return promise;
  }

  globalSetting(val, paginationElement) {
    let that = this;
    let storageKey = "gs_" + that.viewName;
    const promise = new Promise((resolve) => {
      that.http.delete("GridState/GlobalSetting?id=" + val + "&storageKey=" + storageKey)
        .toPromise()
        .then(
          response => {   //Success
            that.reload();
          },
          msg => {    //Error
            if (msg.status = "404") {
              that.toastr.error(msg.error.Message, "Error");
            }
            else {
              that.toastr.error(msg.error.Message, "Error");
            }
            that.logger.log(msg.statusText || 'Server error');
          });
    });
    return promise;
  }

  nameState(val, paginationElement) {
    let isAction = false;
    let that = this;
    const promise = new Promise((resolve) => {
      that.http.get("GridState/GetSingle?id=" + val)
        .toPromise()
        .then(
          response => {
            if (response != null) {
              that.gs_state = response;
              let data = ((that.gs_state._body) ? JSON.parse(that.gs_state._body) : that.gs_state);
              var gridState = data.StorageData ? JSON.parse(data.StorageData) : data;
              that.gridOptions.IsDefalut = data.IsDefalut;
              if (gridState.Column[0].colId != "Action") {
                for (let i = 0; i < gridState.Column.length; i++) {
                  if (gridState.Column[i].colId == "Action") {
                    for (let j = i; j > 0; j--) {
                      gridState.Column[j] = gridState.Column[j - 1];
                    }
                    gridState.Column[0] = { "colId": "Action", "hide": false, "aggFunc": null, "width": 55, "pivotIndex": null, "pinned": "left", "rowGroupIndex": null };
                    isAction = true;
                  }
                  if (isAction) {
                    break;
                  }
                }
              }
              if (gridState.Column.length != that.columnDefs.length) {
                if (gridState.Column.length < that.columnDefs.length)
                  for (let i = 0; i < that.columnDefs.length; i++) {
                    let column = gridState.Column.find(x => x.colId == that.columnDefs[i].field);
                    if (!column) {
                      gridState.Column.push({ "colId": that.columnDefs[i].field, "hide": true, "aggFunc": null, "width": 160, "pivotIndex": null, "pinned": null, "rowGroupIndex": null });
                    }
                  }
              }
              that.gridOptions.columnApi.setColumnState(gridState.Column);
              if (gridState.Sort) {
                that.sortOptions.colId = gridState.Sort[0] ? gridState.Sort[0].colId : '';
                that.sortOptions.sort = gridState.Sort[0] ? gridState.Sort[0].sort : '';
                var result: any = [];
                result.push(that.sortOptions);
                that.gridOptions.api.setSortModel(result);
              }
              if (gridState.pageSize) {
                that.gridOptions.paginationAutoPageSize = true;
                that.gridOptions.paginationPageSize = gridState.pageSize;
                that.gridOptions.cacheBlockSize = gridState.pageSize;
              }
              that.filters = JSON.parse(gridState.AppliedFilters);
              that.gridOptions.api.setFilterModel(gridState.Filter);
              that.gridOptions.namedState = val;
              that.ProcessData(true);
            }
            else {
              if (that.gridOptions.DropVal == -1) {
                that.gridOptions.api.setFilterModel([]);
                that.gridOptions.api.setSortModel([]);
                if (typeof that.gridOptions.columnApi.getAllColumns().filter(x => x.visible === false)[0] !== 'undefined') {
                  var columnName = that.gridOptions.columnApi.getAllColumns().filter(x => x.visible === false)[0].colId;
                  that.init(that.viewName, that.gridOptions, columnName, "desc", that.defaultFilterByFieldName, that.defaultFilterByFieldValue);
                }
                let columnDefs1 = that.gridOptions.columnApi.getAllColumns();
                let colDefs = [];
                for (let i = 0; i < columnDefs1.length; i++) {
                  colDefs.push(columnDefs1[i].colDef);
                }
                that.gridOptions.namedState = null;
                that.setLocalState(false, false, false, -1);
                that.gridOptions.api.setColumnDefs([]);
                that.restoreColumnDefs(colDefs);
              }
            }
          },
          msg => {    //Error
            that.logger.log(msg.statusText || 'Server error');
          });
    });
    return promise;
  }

  exportExcel(val, paginationElement, currentDate) {
    let agGridSource = this;
    let exportColumnIds = [];
    let exportColumnLabels = [];
    let rules = [];
    let columnDefs = agGridSource.gridOptions.columnApi.getAllDisplayedColumns();
    let includeDefaultFilter = true;
    //Columns
    for (let i = 0; i < columnDefs.length; i++) {
      let column = columnDefs[i].colDef;
      if (column.headerName != 'Action' && !column.checkboxSelection) {
        if (column.headerName.includes('<i')) {
          column.headerName = column.headerName.replace('"></i>', '')
          column.headerName = column.headerName.replace('"></i>', '')
          column.headerName = column.headerName.split('title="')
          column.headerName = column.headerName[1];
        }
        exportColumnIds.push(column.field);
        exportColumnLabels.push(column.headerName);
      }
    }


    if (agGridSource.excelfilterModel) {

      for (let columnName in agGridSource.excelfilterModel) {

        let column = agGridSource.gridOptions.columnApi.getColumn(columnName).getColDef();
        let type = agGridSource.excelfilterModel[columnName].type;
        let data = agGridSource.excelfilterModel[columnName].filter;

        if (column.filter == 'agTextColumnFilter') {
          if (type == 'blanks') {
            rules.push({
              field: columnName,
              op: agGridSource.getNumberOperator(type),
              data: null
            });
          }
          else {
            rules.push({
              field: columnName,
              op: agGridSource.getTextOperator(type),
              data: data
            });
          }
        }
        else if (column.filter == 'agNumberColumnFilter') {
          if (type !== 'inRange' && type != 'blanks') {
            rules.push({
              field: columnName,
              op: agGridSource.getNumberOperator(type),
              data: data
            });
          }
          else if (type == 'blanks') {
            rules.push({
              field: columnName,
              op: agGridSource.getNumberOperator(type),
              data: null
            });
          }
          else if (type == 'inRange') {
            rules.push({
              field: columnName,
              op: 'ge',
              data: agGridSource.excelfilterModel[columnName].filter
            });
            rules.push({
              field: columnName,
              op: 'le',
              data: agGridSource.excelfilterModel[columnName].filterTo
            });
          }
        }
        else if (column.filter == 'agDateColumnFilter') {
          if (type !== 'inRange' && type != 'blanks') {
            rules.push({
              field: columnName,
              op: agGridSource.getNumberOperator(type),
              data: agGridSource.excelfilterModel[columnName].dateFrom
            });
          }
          else if (type == 'blanks') {
            rules.push({
              field: columnName,
              op: agGridSource.getNumberOperator(type),
              data: null
            });
          }
          else if (type == 'inRange') {
            rules.push({
              field: columnName,
              op: 'ge',
              data: agGridSource.excelfilterModel[columnName].dateFrom
            });
            rules.push({
              field: columnName,
              op: 'le',
              data: agGridSource.excelfilterModel[columnName].dateTo
            });
          }
        }
        else if (column.filter == 'agTimeColumnFilter') {
          if (type !== 'inRange' && type != 'blanks') {
            rules.push({
              field: columnName,
              op: agGridSource.getNumberOperator(type),
              data: agGridSource.excelfilterModel[columnName].timeFrom
            });
          }
          else if (type == 'blanks') {
            rules.push({
              field: columnName,
              op: agGridSource.getNumberOperator(type),
              data: null
            });
          }
          else if (type == 'inRange') {
            rules.push({
              field: columnName,
              op: 'ge',
              data: agGridSource.excelfilterModel[columnName].timeFrom
            });
            rules.push({
              field: columnName,
              op: 'le',
              data: agGridSource.excelfilterModel[columnName].timeTo
            });
          }
        }
        else if (column.filter == 'agSetColumnFilter') {
          let availableItems = column.filterParams.values;
          let selectedItems = agGridSource.excelfilterModel[columnName];
          for (let i = 0; i < selectedItems.length; i++) {
            //if (selectedItems.indexOf(availableItems[i]) > 0) {
            let value = selectedItems[i];
            if (column.columnType == 'Boolean') {
              if (value == 'Yes') {
                value = "Yes";
              }
              else if (value == 'No') {
                value = 'No';
              }
              else {
                value = '';
              }
            }
            rules.push({
              field: columnName,
              op: 'eq',
              data: value
            });
            //}
          }
        }
      }
    }

    //Filters
    for (let i = 0; i < agGridSource.filters.rules.length; i++) {
      if (agGridSource.filters.rules[i].field === agGridSource.defaultFilterByFieldName) {
        includeDefaultFilter = false;
        break;
      }
    }
    if (typeof agGridSource.gridOptions.defaultFilterByFieldName == 'string' && includeDefaultFilter) {
      agGridSource.filters.rules.push({
        field: agGridSource.gridOptions.defaultFilterByFieldName,
        op: 'eq',
        data: agGridSource.gridOptions.defaultFilterByFieldValue
      });
    }
    else if (agGridSource.gridOptions.defaultFilterByFieldName != undefined) {
      for (let key of agGridSource.gridOptions.defaultFilterByFieldName) {
        agGridSource.filters.rules.push({
          field: key.toString(),
          op: 'eq',
          data: agGridSource.gridOptions.defaultFilterByFieldName[key]
        });
      }
    }

    let exportColumnIdsJson = JSON.stringify(exportColumnIds);
    let exportColumnLabelsJson = JSON.stringify(exportColumnLabels);
    let rulesJson = JSON.stringify(rules);
    let params = "?exportGridId=" + encodeURIComponent(agGridSource.viewName);
    params += "&page" + "=" + encodeURIComponent(agGridSource.gridOptions.api.paginationGetCurrentPage() + 1);
    params += "&pageSize" + "=" + encodeURIComponent(agGridSource.gridOptions.api.paginationGetPageSize());
    params += "&colId" + "=" + encodeURIComponent(agGridSource.sortOptions.colId);
    params += "&sort" + "=" + encodeURIComponent(agGridSource.sortOptions.sort);
    params += "&filters" + "=" + JSON.stringify(agGridSource.filters);
    params += "&exportColumnIds" + "=" + exportColumnIdsJson;
    params += "&exportColumnLabels" + "=" + encodeURIComponent(exportColumnLabelsJson);
    params += "&savedFilterId" + "=" + encodeURIComponent(val);
    params += "&gridLabel" + "=" + encodeURIComponent(agGridSource.gridLabel);
    params += "&filteRules" + "=" + encodeURIComponent(rulesJson);
    params += "&current_date" + "=" + encodeURIComponent(currentDate);
    const promise = new Promise((resolve, reject) => {
      agGridSource.http.get("ExcelExport" + encodeURI(params))
        .toPromise()
        .then(
          response => {   //Success
            let data = response;
            resolve(data);
          },
          msg => {    //Error
            reject(msg);
          });
    });
    return promise;
  }

  setLocalState(isPagechange?: boolean, isSortChanged?: boolean, isFilterChanged?: boolean, val?: any) {
    let that = this;
    var currentUser = JSON.parse(localStorage.getItem('currentUser'));
    let localState = JSON.parse(that.getLocalState());
    let uniqueId = that.defaultFilterByFieldValue ? that.defaultFilterByFieldValue : "";
    let cuserId = currentUser.user.Id;
    if (!localState && !val) {
      let params = {
        page: that.gridOptions.api.paginationGetCurrentPage() + 1,
        pageSize: that.gridOptions.api.paginationGetPageSize(),
        sortModel: that.gridOptions.api.getSortModel(),
        filterModel: that.gridOptions.api.getFilterModel(),
        stateId: that.gridOptions.namedState,
        filters: JSON.stringify(that.filters),
        colId: that.sortOptions.colId,
        sort: that.sortOptions.sort,
        cuser: currentUser.user.Id
      };
      localStorage.setItem(that.viewName + uniqueId + cuserId, JSON.stringify(params));
    }
    else {
      if (!val) {
        if (isPagechange) {
          localState.page = that.gridOptions.api.paginationGetCurrentPage() + 1;
          localState.pageSize = that.gridOptions.api.paginationGetPageSize();
        }
        else if (isSortChanged) {
          localState.sortModel = that.gridOptions.api.getSortModel() != [] ? that.gridOptions.api.getSortModel() : [];
        }
        else if (isFilterChanged) {
          localState.filterModel = that.gridOptions.api.getFilterModel() != [] ? that.gridOptions.api.getFilterModel() : [];
        }
        if (that.gridOptions && that.gridOptions.namedState) {
          localState.stateId = that.gridOptions.namedState;
        }
        localState.cuser = currentUser.user.Id;
        localStorage.setItem(that.viewName + uniqueId + cuserId, JSON.stringify(localState));
      }
      else {
        let params = {
          page: 1,
          pageSize: 20,
          sortModel: [],
          filterModel: [],
          stateId: val,
          cuser: currentUser.user.Id
        };
        localStorage.setItem(that.viewName + uniqueId + cuserId, JSON.stringify(params));
      }
    }
  }

  getLocalState() {
    let that = this;
    var currentUser = JSON.parse(localStorage.getItem('currentUser'));
    let cuserId = currentUser.user.Id;
    let uniqueId = that.defaultFilterByFieldValue ? that.defaultFilterByFieldValue : "";
    return localStorage.getItem(that.viewName + uniqueId + cuserId);
  }

  deleteLocalState() {
    let that = this;
    var currentUser = JSON.parse(localStorage.getItem('currentUser'));
    let cuserId = currentUser.user.Id;
    let uniqueId = that.defaultFilterByFieldValue ? that.defaultFilterByFieldValue : "";
    localStorage.removeItem(that.viewName + uniqueId + cuserId);
  }

  handleError(error: any) {
    console.error(error);
    return Observable.throw(error.error || 'Server error');
  };

}

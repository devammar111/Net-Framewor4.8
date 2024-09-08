import { Injectable } from '@angular/core';
import 'rxjs/Rx'; //get everything from Rx  
import 'rxjs/add/operator/toPromise';

@Injectable()
export class AgGridHelper {

  constructor() {
  }

  static actionTemplate = function (headerName: string, field: string, options) {
    var options = options || {};
    var width = options.width || 70;
    var cellRendererFramework = options.cellRendererFramework || null;
    return {
      headerName: headerName,
      field: field,
      width: width,
      suppressMenu: true,
      suppressSorting: true,
      suppressFilter: true,
      cellRenderer: cellRendererFramework,
      lockPosition: true,
      pinned: 'left'
    }
  };

  static stringTemplate = function (headerName: string, field: string, optionsParam?) {
    var options = optionsParam || {};
    var cellRendererFramework = options.cellRendererFramework || AgGridHelper.defaultRenderer;
    var hide = options.hide || false;
    var width = options.width || 160;
    var cellStyle = options.cellStyle || '';
    var headerValueGetter = options.headerValueGetter || '';
    return {
      headerName: headerName,
      headerValueGetter: headerValueGetter,
      field: field,
      hide: hide,
      width: width,
      cellRenderer: cellRendererFramework,
      cellStyle: cellStyle,
      filter: 'agTextColumnFilter',
      menuTabs: ['filterMenuTab', 'generalMenuTab'],
      filterParams: { apply: true, clearButton: true, newRowsAction: 'keep' },
      icons: {
        filter: '<i class="fa fa-filter"/>',
        sortAscending: '<i class="fa fa-sort-alpha-down"/>',
        sortDescending: '<i class="fa fa-sort-alpha-up"/>'
      }
    }
  };

  static currencyTemplate = function (headerName: string, field: string, optionsParam?) {
    var options = optionsParam || {};
    var cellRendererFramework = options.cellRendererFramework || AgGridHelper.currencyRenderer;
    var pinnedRowCellRenderer = options.pinnedRowCellRenderer || "";
    var hide = options.hide || false;
    return {
      headerName: headerName,
      field: field,
      hide: hide,
      width: 160,
      cellRenderer: cellRendererFramework,
      pinnedRowCellRenderer: pinnedRowCellRenderer,
      filter: 'agNumberColumnFilter',
      menuTabs: ['filterMenuTab', 'generalMenuTab'],
      filterParams: { apply: true, clearButton: true, newRowsAction: 'keep' },
      cellStyle: { "text-align": "right" },
      icons: {
        filter: '<i class="fa fa-filter"/>',
        sortAscending: '<i class="fa fa-sort-amount-down"/>',
        sortDescending: '<i class="fa fa-sort-amount-up"/>'
      }
    }
  };

  static decimalTemplate = function (headerName: string, field: string, optionsParam?) {
    var options = optionsParam || {};
    var cellRendererFramework = options.cellRendererFramework || AgGridHelper.decimalRenderer;
    var hide = options.hide || false;
    return {
      headerName: headerName,
      field: field,
      hide: hide,
      width: 160,
      cellRenderer: cellRendererFramework,
      filter: 'agNumberColumnFilter',
      menuTabs: ['filterMenuTab', 'generalMenuTab'],
      filterParams: { apply: true, clearButton: true, newRowsAction: 'keep' },
      cellStyle: { "text-align": "right" },
      icons: {
        filter: '<i class="fa fa-filter"/>',
        sortAscending: '<i class="fa fa-sort-amount-down"/>',
        sortDescending: '<i class="fa fa-sort-amount-up"/>'
      }
    }
  };

  static numberTemplate = function (headerName: string, field: string, optionsParam?) {
    var options = optionsParam || {};
    var cellRendererFramework = options.cellRendererFramework || AgGridHelper.numberRenderer;
    var hide = options.hide || false;
    var width = options.width || 160;
    return {
      headerName: headerName,
      field: field,
      hide: hide,
      width: width,
      cellRenderer: cellRendererFramework,
      filter: 'agNumberColumnFilter',
      menuTabs: ['filterMenuTab', 'generalMenuTab'],
      filterParams: { apply: true, clearButton: true, newRowsAction: 'keep' },
      cellStyle: { "text-align": "right" },
      icons: {
        filter: '<i class="fa fa-filter"/>',
        sortAscending: '<i class="fa fa-sort-amount-down"/>',
        sortDescending: '<i class="fa fa-sort-amount-up"/>'
      }
    }
  };

  static dateTemplate = function (headerName: string, field: string, optionsParam?) {
    var options = optionsParam || {};
    var cellRendererFramework = options.cellRendererFramework || AgGridHelper.dateRenderer;
    var hide = options.hide || false;
    return {
      headerName: headerName,
      field: field,
      hide: hide,
      width: 160,
      cellRenderer: cellRendererFramework,
      filter: 'agDateColumnFilter',
      menuTabs: ['filterMenuTab', 'generalMenuTab'],
      filterParams: { apply: true, clearButton: true, newRowsAction: 'keep' },
      icons: {
        filter: '<i class="fa fa-filter"/>',
        sortAscending: '<i class="fa fa-sort-amount-down"/>',
        sortDescending: '<i class="fa fa-sort-amount-up"/>'
      }
    }
  };

  static dateTimeTemplate = function (headerName: string, field: string, optionsParam?) {
    var options = optionsParam || {};
    var cellRendererFramework = options.cellRendererFramework || AgGridHelper.dateTimeRenderer;
    var hide = options.hide || false;
    return {
      headerName: headerName,
      field: field,
      hide: hide,
      width: 160,
      cellRenderer: cellRendererFramework,
      filter: 'agDateColumnFilter',
      menuTabs: ['filterMenuTab', 'generalMenuTab'],
      filterParams: { apply: true, clearButton: true, newRowsAction: 'keep' },
      icons: {
        filter: '<i class="fa fa-filter"/>',
        sortAscending: '<i class="fa fa-sort-amount-down"/>',
        sortDescending: '<i class="fa fa-sort-amount-up"/>'
      }
    }
  };

  static timeTemplate = function (headerName: string, field: string, optionsParam?) {
    var options = optionsParam || {};
    var cellRendererFramework = options.cellRendererFramework || AgGridHelper.timeRenderer;
    var hide = options.hide || false;
    var width = options.width || 160;
    var cellStyle = options.cellStyle || '';
    var headerValueGetter = options.headerValueGetter || '';
    return {
      headerName: headerName,
      headerValueGetter: headerValueGetter,
      field: field,
      hide: hide,
      width: width,
      cellRenderer: cellRendererFramework,
      cellStyle: cellStyle,
      filter: 'agTimeColumnFilter',
      menuTabs: ['filterMenuTab', 'generalMenuTab'],
      filterParams: { apply: true, clearButton: true, newRowsAction: 'keep' },
      icons: {
        filter: '<i class="fa fa-filter"/>',
        sortAscending: '<i class="fa fa-sort-alpha-down"/>',
        sortDescending: '<i class="fa fa-sort-alpha-up"/>'
      }
    }
  };

  static booleanTemplate = function (headerName: string, field: string, optionsParam?) {
    var options = optionsParam || {};
    var cellRendererFramework = options.cellRendererFramework || AgGridHelper.booleanRenderer;
    var cellClass = options.cellClass || '';
    var hide = options.hide || false;
    var items = ['Blanks', 'Yes', 'No'];
    return {
      headerName: headerName,
      field: field,
      hide: hide,
      width: 160,
      columnType: 'Boolean',
      cellClass: cellClass,
      cellRenderer: cellRendererFramework,
      filter: 'agSetColumnFilter',
      menuTabs: ['filterMenuTab', 'generalMenuTab'],
      filterParams: { values: items, apply: true, clearButton: true, newRowsAction: 'keep' },
      icons: {
        filter: '<i class="fa fa-filter"/>',
        sortAscending: '<i class="fa fa-sort-alpha-down"/>',
        sortDescending: '<i class="fa fa-sort-alpha-up"/>'
      }
    }
  };

  static dropdownTemplate = function (headerName: string, field: string, optionsParam?) {
    var options = optionsParam || {};
    var cellRendererFramework = options.cellRendererFramework || AgGridHelper.defaultRenderer;
    var hide = options.hide || false;
    var items = options.items || [];
    items.unshift("Blanks");
    return {
      headerName: headerName,
      field: field,
      hide: hide,
      width: 160,
      columnType: 'Dropdown',
      cellRenderer: cellRendererFramework,
      filter: 'agSetColumnFilter',
      menuTabs: ['filterMenuTab', 'generalMenuTab'],
      filterParams: { values: items, apply: true, clearButton: true, newRowsAction: 'keep' },
      icons: {
        filter: '<i class="fa fa-filter"/>',
        sortAscending: '<i class="fa fa-sort-alpha-down"/>',
        sortDescending: '<i class="fa fa-sort-alpha-up"/>'
      }
    }
  };

  static defaultRenderer = function (params) {
    var value = " ";
    if (params.value != null) {
      value = params.value.replace('<', '&lt;');
      value = value.replace('>', '&gt;');
      return '<span title="' + params.value + '">' + value + '</span>';
    }
    else {
      return " ";
    }
  };

  static booleanRenderer = function (params) {
    if (params.value == false)
      return 'No'
    else if (params.value == true)
      return 'Yes';
    else
      return '';
  };

  static decimalRenderer = function (params) {
    if (params.value === null || params.value === undefined) {
      return null;
    } else if (isNaN(params.value)) {
      return 'NaN';
    } else {
      return new Intl.NumberFormat('en-GB', { minimumFractionDigits: 2 }).format(params.value);
    }
  };

  static currencyRenderer = function (params) {
    if (!params || !params.data) {
      return " ";
    }
    if (params.data.Ccy === undefined) {
      return AgGridHelper.decimalRenderer(params);
    }
    else {
      if (params.data.Ccy == null) {
        return AgGridHelper.decimalRenderer(params);
      }
      else {
        return params.data.Ccy + ' ' + AgGridHelper.decimalRenderer(params);
      }
    }
  };

  static numberRenderer = function (params) {
    if (params.value != null) {
      return params.value.toString(undefined);
    }
  };

  static dateRenderer = function (params) {
    if (params.value) {
      return AgGridHelper.formatDate(params.value);

    } else {
      return null;
    }
  };

  static dateTimeRenderer = function (params) {
    if (params.value) {
      return AgGridHelper.formatDateTime(params.value);

    } else {
      return null;
    }
  };

  static timeRenderer = function (params) {
    if (params.value) {
      return AgGridHelper.formatTime24Hours(params.value);

    } else {
      return null;
    }
  };

  static formatDate = function (date) {
    var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
      "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
    ];

    var newDateTime = date.split(' ');
    var newDate = newDateTime[0].split(/\//);
    newDate = newDate[0] + "-" + months[+newDate[1] - 1] + "-" + newDate[2];
    return newDate;
  };

  static formatDateTime = function (date) {
    var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
      "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
    ];

    var newDateTime = date.split(' ');
    var newDate = newDateTime[0].split(/\//);
    newDate = newDate[0] + "-" + months[+newDate[1] - 1] + "-" + newDate[2];

    var time = date.substr(date.indexOf(' ') + 1);

    time = time.toString().match(/^([01]\d|2[0-3])(:)([0-5]\d)(:[0-5]\d)?$/) || [time];

    if (time.length > 1) { // If time format correct
      time = time.slice(1);  // Remove full string match value
      time[5] = +time[0] < 12 ? ' AM' : ' PM'; // Set AM/PM
      time[0] = +time[0] % 12 || 12; // Adjust hours
      time[0] = time[0] < 10 ? "0" + time[0] : time[0];
    }
    return newDate + ' ' + time.join(''); //
  };

  static formatTime24Hours = function (date) {
    let time = date.substr(date.indexOf(' ') + 1);

    time = time.toString().match(/^([01]\d|2[0-3])(:)([0-5]\d)(:[0-5]\d)?$/) || [time];

    if (time.length > 1) { // If time format correct
      time = time.slice(1);  // Remove full string match value
      time.pop();
    }
    return time.join(''); //
  };

  static formatTime = function (date) {
    var time = date.substr(date.indexOf(' ') + 1);

    time = time.toString().match(/^([01]\d|2[0-3])(:)([0-5]\d)(:[0-5]\d)?$/) || [time];

    if (time.length > 1) { // If time format correct
      time = time.slice(1);  // Remove full string match value
      time[5] = +time[0] < 12 ? ' AM' : ' PM'; // Set AM/PM
      time[0] = +time[0] % 12 || 12; // Adjust hours
      time[0] = time[0] < 10 ? "0" + time[0] : time[0];
    }
    return time.join(''); //
  };

  static createDiv = function () {
    let div: HTMLDivElement = document.createElement('div');
    div.className = 'text-center';
    return div;
  };

  static createLink = function (className: string, title: string) {
    let link: HTMLAnchorElement = document.createElement('a');
    link.className = 'anchor-color';
    link.innerHTML = '<i class="fa ' + className + '" title="' + title + '"></i></a>';
    return link;
  };
}

export class DateConvertor {
  fromModel(data: any): any {
    if (data == null) {
      return data;
    }
    let date;
    if (typeof (data) === "string") {
      let separators: string[] = ["/", "\\s","T"];
      let dateParts: string[] = data.split(new RegExp(separators.join('|'), 'g'));
      date = { year: +dateParts[2], month: +dateParts[1], day: +dateParts[0] };
    }
    else {
      date = { year: data.year, month: data.month, day: data.day };
    }
    return date;
  }
  DateTimefromModel(data: any): any {
    if (data == null) {
      return data;
    }
    let time;
    if (typeof (data) === "string") {
      let separators: string[] = ["/", "\\s", "T"];
      let dateParts: string[] = data.split(new RegExp(separators.join('|'), 'g'));
      time = dateParts[3];
    }
    else {
      time = data;
    }
    return time;
  }

  toModel(data: any): string {
    if (data == null) {
      return null;
    }
    let date: string = data.day + "/" + (data.month < 10 ? ("0" + data.month) : data.month) + "/" + data.year;
    return date;
  }

  getDateTimeFromModel(data: string): Date {
    if (data == null) {
      return null;
    }
    var new_date = data.split('/');
    var swap_element = new_date[0];
    new_date[0] = new_date[1];
    new_date[1] = swap_element;

    var new_date1 = new_date[0] + '/' + new_date[1] + '/' + new_date[2];
    //return date format >> Mon Jan 07 2019 06:10:00 GMT+0500 (Pakistan Standard Time)
    var dateTime = new Date(new_date1);    
    return dateTime;
  }

  dateTimeToModel(data: any): string {
    if (data == null) {
      return null;
    }
    var dd = String(data.getDate()).padStart(2, '0');
    var mm = String(data.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = data.getFullYear();
    var hours = data.getHours();
    var mints = data.getMinutes();
    let date: string = dd + '/' + mm + '/' + yyyy + ' ' + hours + ':' + mints;
    return date;
  }

  compareDates(date1: any, date2: any): any {
    let fromModel = this.fromModel(date1);
    let d1: Date = new Date(fromModel.year, fromModel.month, fromModel.day);
    fromModel = this.fromModel(date2);
    let d2: Date = new Date(fromModel.year, fromModel.month, fromModel.day);

    if (d1 < d2) {
      return true;
    }
    return false;
  }

  toGridModel(data: any): string {

    if (data == null) {
      return null;
    }
    let date: string = (data.day >= 10 ? data.day : "0" + data.day) + "/" + (data.month < 10 ? ("0" + data.month) : data.month) + "/" + data.year;
    return date;
  }
}


export class DatePickerConvertor {


    fromModel(data: any): any {
        if (data == null) {
            return data;
        }
        let date;
        if (typeof (data) === "string") {
            let separators: string[] = ["/", "\\s"];
            let dateParts: string[] = data.split(new RegExp(separators.join('|'), 'g'));
            date = { date: { year: +dateParts[2], month: +dateParts[1], day: +dateParts[0] } };
        }
        else {
            date = { date: { year: data.date.year, month: data.date.month, day: data.date.day } };
        }
        return date;
    }

    toModel(data: any): string {
        if (data == null) {
            return null;
        }

        let date: string = data.date.day + "/" + data.date.month + "/" + data.date.year;
        return date;
    }

    toTimeModel(data: any): string {
        if (data == null) {
            return null;
        }
        return data;
    }

    compareDates(date1: any, date2: any): any {
        let d1, d2: Date;
        let fromModel;
        if (typeof (date1) != "string" && !date1.date) {
            d1 = new Date(date1.getFullYear(), date1.getMonth(), date1.getDate());
        }
        else {
            fromModel = this.fromModel(date1);
            d1 = new Date(fromModel.date.year, fromModel.date.month -1, fromModel.date.day);
        }
        if (typeof (date2) != "string" && !date2.date) {
            d1 = new Date(date2.getFullYear(), date2.getMonth(), date2.getDate());
        }
        else {
            fromModel = this.fromModel(date2);
            d2 = new Date(fromModel.date.year, fromModel.date.month -1, fromModel.date.day);
        }

        if (d1 < d2) {
            return true;
        }
        return false;
    }

    forGrid(data: any): string {
        if (data == null) {
            return null;
        }
        let date: string = (data.date.day >= 10 ? data.date.day : "0" + data.date.day) + "/"
            + (data.date.month >= 10 ? data.date.month : "0" + data.date.month) + "/" + data.date.year;
        return date;
    }
}
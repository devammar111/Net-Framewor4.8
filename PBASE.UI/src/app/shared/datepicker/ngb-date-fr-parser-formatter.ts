import { Injectable } from "@angular/core";
import { NgbDateParserFormatter, NgbDateStruct } from "@ng-bootstrap/ng-bootstrap";
import { DatePipe } from '@angular/common';

function padNumber(value: number) {
    if (isNumber(value)) {
        return `0${value}`.slice(-2);
    } else {
        return "";
    }
}

function isNumber(value: any): boolean {
    return !isNaN(toInteger(value));
}

function toInteger(value: any): number {
    return parseInt(`${value}`, 10);
}


@Injectable()
export class NgbDateFRParserFormatter extends NgbDateParserFormatter {
    datePipe = new DatePipe('en-US');
    parse(value: string): NgbDateStruct {
        if (value) {
            const dateParts = value.trim().split('/');
            if (dateParts.length === 1 && isNumber(dateParts[0])) {
                return { year: toInteger(dateParts[0]), month: null, day: null };
            } else if (dateParts.length === 2 && isNumber(dateParts[0]) && isNumber(dateParts[1])) {
                return { year: toInteger(dateParts[1]), month: toInteger(dateParts[0]), day: null };
            } else if (dateParts.length === 3 && isNumber(dateParts[0]) && isNumber(dateParts[1]) && isNumber(dateParts[2])) {
                return { year: toInteger(dateParts[2]), month: toInteger(dateParts[1]), day: toInteger(dateParts[0]) };
            }
        }
        return null;
    }

    format(date: NgbDateStruct): string {
        let stringDate: string = "";
        if (date) {
            stringDate += isNumber(date.day) ? padNumber(date.day) + "/" : "";
            stringDate += isNumber(date.month) ? padNumber(date.month) + "/" : "";
            stringDate += date.year;
        }
        return stringDate;
    }

    formatForServer(date: NgbDateStruct): string {
        if (date === null) {
            return '';
        }
        try {
            return this.datePipe.transform(new Date(date.year, date.month - 1, date.day), 'dd/MM/yyyy');
        } catch (e) {
            return '';
        }
    }
}
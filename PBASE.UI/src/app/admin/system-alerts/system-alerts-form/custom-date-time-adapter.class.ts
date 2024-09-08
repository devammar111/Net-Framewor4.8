import { Component, OnInit, ViewChild, Injectable } from '@angular/core';
import { DateTimeAdapter } from 'ng-pick-datetime';
export const CUSTOM_DATE_TIME_FORMATS = {
  //parseInput: 'your custom value',
  fullPickerInput: 'dd/mm/YYYY HH:mm',
  //datePickerInput: 'your custom value',
  //timePickerInput: 'your custom value',
  //monthYearLabel: 'your custom value',
  //dateA11yLabel: 'your custom value',
  //monthYearA11yLabel: 'your custom value',
};

@Injectable()
export class CustomDateTimeAdapter extends DateTimeAdapter<any> {
    getYear(date: any): number {
        throw new Error("Method not implemented.");
    }    getMonth(date: any): number {
        throw new Error("Method not implemented.");
    }
    getDay(date: any): number {
        throw new Error("Method not implemented.");
    }
    getDate(date: any): number {
        throw new Error("Method not implemented.");
    }
    getHours(date: any): number {
        throw new Error("Method not implemented.");
    }
    getMinutes(date: any): number {
        throw new Error("Method not implemented.");
    }
    getSeconds(date: any): number {
        throw new Error("Method not implemented.");
    }
    getTime(date: any): number {
        throw new Error("Method not implemented.");
    }
    getNumDaysInMonth(date: any): number {
        throw new Error("Method not implemented.");
    }
    differenceInCalendarDays(dateLeft: any, dateRight: any): number {
        throw new Error("Method not implemented.");
    }
    getYearName(date: any): string {
        throw new Error("Method not implemented.");
    }
    getMonthNames(style: "long" | "short" | "narrow"): string[] {
        throw new Error("Method not implemented.");
    }
    getDayOfWeekNames(style: "long" | "short" | "narrow"): string[] {
        throw new Error("Method not implemented.");
    }
    toIso8601(date: any): string {
        throw new Error("Method not implemented.");
    }
    isEqual(dateLeft: any, dateRight: any): boolean {
        throw new Error("Method not implemented.");
    }
    isSameDay(dateLeft: any, dateRight: any): boolean {
        throw new Error("Method not implemented.");
    }
    isValid(date: any): boolean {
        throw new Error("Method not implemented.");
    }
    invalid() {
        throw new Error("Method not implemented.");
    }
    isDateInstance(obj: any): boolean {
        throw new Error("Method not implemented.");
    }
    addCalendarYears(date: any, amount: number) {
        throw new Error("Method not implemented.");
    }
    addCalendarMonths(date: any, amount: number) {
        throw new Error("Method not implemented.");
    }
    addCalendarDays(date: any, amount: number) {
        throw new Error("Method not implemented.");
    }
    setHours(date: any, amount: number) {
        throw new Error("Method not implemented.");
    }
    setMinutes(date: any, amount: number) {
        throw new Error("Method not implemented.");
    }
    setSeconds(date: any, amount: number) {
        throw new Error("Method not implemented.");
    }
    createDate(year: number, month: number, date: number);
    createDate(year: number, month: number, date: number, hours: number, minutes: number, seconds: number);
    createDate(year: any, month: any, date: any, hours?: any, minutes?: any, seconds?: any) {
        throw new Error("Method not implemented.");
    }
    clone(date: any) {
        throw new Error("Method not implemented.");
    }
    now() {
        throw new Error("Method not implemented.");
    }
    format(date: any, displayFormat: any): string {
        throw new Error("Method not implemented.");
    }
    parse(value: any, parseFormat: any) {
        throw new Error("Method not implemented.");
    }
}



import { Injectable } from "@angular/core";
import { Breadcrumb } from "./breadcrumb.model";
import { BehaviorSubject } from "rxjs";

@Injectable()
export class BreadcrumbService {
  public breadcrumbItem: BehaviorSubject<Breadcrumb[]> = new BehaviorSubject<Breadcrumb[]>([]);
  private breadcrumbs: Breadcrumb[];
  constructor() {
    this.breadcrumbs = [];
  }

  set(items: Breadcrumb[]) {
    this.clear();
    this.add(items);
    this.breadcrumbItem.next(this.breadcrumbs);
  }

  add(items: Breadcrumb[]) {
    this.breadcrumbs = items;
  }

  clear() {
    this.breadcrumbs = [];
  }
}

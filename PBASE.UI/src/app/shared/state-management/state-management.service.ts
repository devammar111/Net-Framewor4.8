import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';

@Injectable()
export class StateManagementService {
  constructor(private http: HttpClient) {
  }

  set(name: string, collapsable?: any) {
      //get local storage
      var localstorage = this.getLocalStorage(name);

      //set scroll
    this.setScroll(null, localstorage);

      //set collapsable
      if (localstorage && localstorage.collapsable) {
          collapsable = localstorage.collapsable;
          return collapsable;
      }
      else {
          return collapsable;
      }
  }

  save(name: string, collapsable?: any) {
      var item = this.getLocalStorage(name);
      if (item) {
          item.collapsable = collapsable;
      }
      else {
          item = {
              collapsable: collapsable,
          };
      }
      localStorage.setItem(name, JSON.stringify(item));
  }

  remove(name: string) {
      localStorage.removeItem(name);
  }

  setScroll(name?: string, storage? : any) {
      let localstorage = name ? this.getLocalStorage(name) : storage;
      if (localstorage && localstorage.scrollPosition) {
          setTimeout(function () { window.scrollTo(0, localstorage.scrollPosition); }, 0);
      }
  }

  getLocalStorage(name) {
      return JSON.parse(localStorage.getItem(name));
  }
  
} 

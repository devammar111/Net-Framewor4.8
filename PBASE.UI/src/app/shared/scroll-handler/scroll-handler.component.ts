import { Component, HostListener, Input } from '@angular/core';

@Component({
  selector: 'app-scroll-handler',
  templateUrl: './scroll-handler.component.html',
  styles: []
})

export class ScrollHandlerComponent {
  constructor() { }
  @Input() name: any;

  @HostListener('window:scroll', ['$event'])
  onWindowScroll() {
    let that = this;
    var item = JSON.parse(localStorage.getItem(that.name));
    if (item) {
      item.scrollPosition = window.pageYOffset;
    }
    else {
      item = {
        scrollPosition: window.pageYOffset
      }
    }
    localStorage.setItem(that.name, JSON.stringify(item));
  }
}

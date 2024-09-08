import { Component, OnInit, OnDestroy, ElementRef } from '@angular/core';
import { Tab } from './tab';
import { RouterOutlet } from '@angular/router';
import * as JQuery from 'jquery';

@Component({
    selector: '[tabLink]',
    inputs: ['tab'],
    templateUrl: './tab-link.component.html',
    styleUrls: ['./tab-link.component.scss']
})
export class TabLinkComponent implements OnInit, OnDestroy {
    tab: Tab;
    private _observer: MutationObserver;
    constructor(private _el: ElementRef) {
    }

    ngOnInit() {
        let $anchor: JQuery = this.getAnchor();
        let anchor: HTMLElement = $anchor[0];

        this._observer = new MutationObserver((mutations: MutationRecord[]) => {
            mutations.forEach((mutation: MutationRecord) => {
                if (mutation.attributeName === "class") {
                    this.tab.active = $anchor.hasClass("router-link-active");
                }
            });
        });
        this._observer.observe(anchor, { attributes: true });
    }

    ngOnDestroy() {
        this._observer.disconnect();
    }

    getAnchor(): JQuery {
        return JQuery(this._el.nativeElement).find("a");
    }
}
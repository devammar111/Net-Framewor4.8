import { Component, OnInit, Input } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { Tab } from './tab';

@Component({
    selector: 'app-tabs',
    templateUrl: './tabs.component.html',
    styleUrls: ['./tabs.component.scss']
})
export class TabsComponent implements OnInit {

    @Input() tabs: Tab[];
    constructor(private _router: Router) {

    }

    ngOnInit() {

    }
}
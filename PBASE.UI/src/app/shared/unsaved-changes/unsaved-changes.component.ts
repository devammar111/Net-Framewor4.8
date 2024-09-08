import { Component, OnInit, ViewContainerRef, ViewChild, HostListener, Input } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Observable } from 'rxjs/Rx';

@Component({
    selector: 'app-unsaved-changes',
    templateUrl: './unsaved-changes.component.html',
})

export class UnsavedChanges {

    constructor() { }

    @Input() formName: any
    @HostListener('window:beforeunload')
    canDeactivate(): Observable<boolean> | boolean {
        return !this.formName.dirty;
    }
}



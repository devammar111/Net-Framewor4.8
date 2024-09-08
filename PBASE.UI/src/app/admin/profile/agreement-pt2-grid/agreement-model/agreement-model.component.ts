import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { HttpService } from 'src/app/shared/http.service';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { ActivatedRoute, Router } from '@angular/router';
import { BreadcrumbService } from '../../../../shared/breadcrumb/breadcrumb.service';
import { Breadcrumb } from '../../../../shared/breadcrumb/breadcrumb.model';

@Component({
  selector: 'app-agreement-model',
  templateUrl: './agreement-model.component.html',
  styleUrls: ['./agreement-model.component.css']
})
export class AgreementModelComponent implements OnInit {
  id: string;
  data: any;
  editorConfig: AngularEditorConfig = {
    editable: false,
    showToolbar: false,
    spellcheck: true,
    minHeight: '5rem',
    placeholder: 'Enter text here...',
    translate: 'no',
    //uploadUrl: 'v1/images', // if needed
    customClasses: [ // optional
      {
        name: "quote",
        class: "quote",
      },
      {
        name: 'redText',
        class: 'redText'
      },
      {
        name: "titleText",
        class: "titleText",
        tag: "h1",
      },
    ]
  };

  constructor(private sanitizer: DomSanitizer,
    private router: Router,
    private route: ActivatedRoute,
    private breadcrumbService: BreadcrumbService,
    private httpService: HttpService, ) { }

  ngOnInit() {
    let that = this;
    that.id = that.route.snapshot.queryParams['id'];
    that.get();
    that.setBreadcrumbs();
  }

  get() {
    let that = this;
    that.httpService
      .get('GetAgreementData/' + (that.id ? that.id : '0'))
      .subscribe((res: any) => {
        that.data = res;
      });
  }

  close() {
    let that = this;
    that.router.navigate(["/profile"]);
  }

  setBreadcrumbs() {
    let that = this;
    let breadcrumbs: Breadcrumb[] = [];
    breadcrumbs.push({
      label: "Profile",
      url: '/profile' ,
      params: {        
      }
    });
    breadcrumbs.push({
      label: "View Agreement",
      url: '',
      params: {}
    });
    that.breadcrumbService.set(breadcrumbs);
  }
}

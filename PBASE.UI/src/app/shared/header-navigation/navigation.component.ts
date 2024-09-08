import { Component, OnInit, AfterViewInit, EventEmitter, Output, Input } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import {
  NgbModal,
  ModalDismissReasons,
  NgbPanelChangeEvent,
  NgbCarouselConfig
} from '@ng-bootstrap/ng-bootstrap';
import { LoginService } from '../../authentication/login/login.service';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { environment } from '../../../environments/environment';
import { FilePickerService } from '../file-picker/file-picker.service';
declare var $: any;

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html'
})
export class NavigationComponent implements OnInit, AfterViewInit {
  @Input() name;
  @Input() ProfileImageHandler;
  isReadonly: boolean = false;
  safeSrc: SafeResourceUrl;
  environment: any = environment;
  @Output() toggleSidebar = new EventEmitter<void>();
  public config: PerfectScrollbarConfigInterface = {};
  public showSearch = false;
  constructor(private modalService: NgbModal,
    private filePickerService: FilePickerService,
    private sanitizer: DomSanitizer,
    private loginService: LoginService
  ) { }

  // This is for Notifications
  notifications: Object[] = [
    {
      btn: 'btn-danger',
      icon: 'ti-link',
      title: 'Luanch Admin',
      subject: 'Just see the my new admin!',
      time: '9:30 AM'
    },
    {
      btn: 'btn-success',
      icon: 'ti-calendar',
      title: 'Event today',
      subject: 'Just a reminder that you have event',
      time: '9:10 AM'
    }
  ];

  mymessages: Object[] = [
  ];
  ngOnInit() {
    let that = this;
    let user = JSON.parse(localStorage.getItem('currentUser'));
    that.isReadonly = user ? user.isReadonly: false;    
    that.safeSrc = that.ProfileImageHandler ? that.sanitizer.bypassSecurityTrustResourceUrl(that.filePickerService.generateLink(that.ProfileImageHandler)) : 'assets/images/users/user-icon.png';
  }

  ngAfterViewInit() { }

  logout(): void {
    this.loginService.logout();
  }
}

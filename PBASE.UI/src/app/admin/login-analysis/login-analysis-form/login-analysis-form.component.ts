import { Component, OnInit, Input } from '@angular/core';
import { LoginAnalysisFormService } from './login-analysis-form.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationDialogService } from 'src/app/shared/confirmation-dialog/confirmation-dialog.service';


@Component({
  selector: 'app-login-analysis',
  templateUrl: './login-analysis-form.component.html',
  styleUrls: ['./login-analysis-form.component.scss']
})

export class LoginAnalysisFormComponent implements OnInit {

  @Input() data: any = {};
  AllLookupLoginAnalysis: any[];
  loginAnalysis: any = {};
  page: any;
  constructor
    (
    private loginAnalysisFormService: LoginAnalysisFormService,
    public activeModal: NgbActiveModal,
    private confirmationDialogService: ConfirmationDialogService
    ) {
  }

  ngOnInit() {
    let that = this;
    this.loginAnalysis.Id = this.data.params.data.Id;
    this.loginAnalysis.StartDate = this.data.startDate;
    this.loginAnalysis.EndDate = this.data.endDate;
    that.loginAnalysisFormService.addLoginAnalysis(this.loginAnalysis).subscribe(
      loginAnalysis => {
        this.data = loginAnalysis;
        this.AllLookupLoginAnalysis = this.data;
      });

  }

  getMyStyles(IsStatus) {
    let myStyles = {
      'background-color': IsStatus ? '#dff0d8' : '#f2dede',
    };
    return myStyles;
  }
}

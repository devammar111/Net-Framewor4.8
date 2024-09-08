import { Component, OnInit, Input } from '@angular/core';
import { RequestValidationService } from './request-validation.service';

@Component({
  selector: 'request-validation',
  template: `<input  id="__requestValidation" value="{{entity.RequestValidation}}"
            name="requestValidation" type="hidden" />`,
})
export class RequestValidationComponent implements OnInit {
  @Input() entity: any
  constructor(private requestValidationService: RequestValidationService) {
  }

  ngOnInit() {
    var that = this;
    that.requestValidationService
      .getToken()
      .subscribe((data: string) => {
        that.entity.RequestValidation = data;
      });
  }
}

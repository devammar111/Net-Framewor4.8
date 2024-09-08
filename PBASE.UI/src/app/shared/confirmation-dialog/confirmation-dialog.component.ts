import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-confirmation-dialog',
  templateUrl: './confirmation-dialog.component.html',
})
export class ConfirmationDialogComponent implements OnInit {
  @Input() title: string;
  @Input() message: string;
  @Input() btnOkText: string;
  @Input() btnCancelText: string;
  @Input() isButtonShow: boolean;
  @Input() closeTime: number;
  constructor(private activeModal: NgbActiveModal) { }

  ngOnInit() {
    this.autoClose();
  }

  private autoClose() {
    if (this.closeTime > 999) {
      setTimeout(() => {
        this.accept();
      }, this.closeTime);
    }
  }

  public decline() {
    this.activeModal.close(false);
  }

  public accept() {
    this.activeModal.close(true);
  }

  public dismiss() {
    this.activeModal.close(false);
  }
}

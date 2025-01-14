import { Component } from '@angular/core';
import { NgbDateParserFormatter } from '@ng-bootstrap/ng-bootstrap';
import { NgbDateFRParserFormatter } from './shared/ngb-date-fr-parser-formatter';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [{
    provide: NgbDateParserFormatter,
    useClass: NgbDateFRParserFormatter
  }]
})
export class AppComponent {
}

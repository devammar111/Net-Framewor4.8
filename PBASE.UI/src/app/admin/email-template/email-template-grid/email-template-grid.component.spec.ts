import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmailTemplateGridComponent } from './email-template-grid.component';

describe('EmailTemplateGridComponent', () => {
  let component: EmailTemplateGridComponent;
  let fixture: ComponentFixture<EmailTemplateGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmailTemplateGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmailTemplateGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

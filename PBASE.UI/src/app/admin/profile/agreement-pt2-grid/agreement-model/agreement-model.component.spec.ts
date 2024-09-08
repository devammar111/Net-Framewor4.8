import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AgreementModelComponent } from './agreement-model.component';

describe('AgreementModelComponent', () => {
  let component: AgreementModelComponent;
  let fixture: ComponentFixture<AgreementModelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AgreementModelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AgreementModelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

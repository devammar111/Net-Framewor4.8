import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AgreementPt2GridComponent } from './agreement-pt2-grid.component';

describe('AgreementPt2GridComponent', () => {
  let component: AgreementPt2GridComponent;
  let fixture: ComponentFixture<AgreementPt2GridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AgreementPt2GridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AgreementPt2GridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

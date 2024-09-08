import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AgreementUserListComponent } from './agreement-user-list.component';

describe('AgreementUserListComponent', () => {
  let component: AgreementUserListComponent;
  let fixture: ComponentFixture<AgreementUserListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AgreementUserListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AgreementUserListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

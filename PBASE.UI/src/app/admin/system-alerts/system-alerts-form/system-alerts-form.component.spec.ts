import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemAlertsFormComponent } from './system-alerts-form.component';

describe('SystemAlertsFormComponent', () => {
  let component: SystemAlertsFormComponent;
  let fixture: ComponentFixture<SystemAlertsFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SystemAlertsFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SystemAlertsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

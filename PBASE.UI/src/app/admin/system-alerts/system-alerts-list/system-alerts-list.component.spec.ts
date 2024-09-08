import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemAlertsListComponent } from './system-alerts-list.component';

describe('SystemAlertsListComponent', () => {
  let component: SystemAlertsListComponent;
  let fixture: ComponentFixture<SystemAlertsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SystemAlertsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SystemAlertsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginAnalysisGridComponent } from './login-analysis-grid.component';

describe('LoginAnalysisGridComponent', () => {
  let component: LoginAnalysisGridComponent;
  let fixture: ComponentFixture<LoginAnalysisGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LoginAnalysisGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginAnalysisGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});

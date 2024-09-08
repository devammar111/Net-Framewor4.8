import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginAnalysisDetailGridComponent } from './login-analysis-detail-grid.component';

describe('LoginAnalysisDetailGridComponent', () => {
  let component: LoginAnalysisDetailGridComponent;
  let fixture: ComponentFixture<LoginAnalysisDetailGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [LoginAnalysisDetailGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginAnalysisDetailGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});

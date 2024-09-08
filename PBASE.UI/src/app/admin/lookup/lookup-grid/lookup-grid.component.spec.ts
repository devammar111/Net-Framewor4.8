import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LookupGridComponent } from './lookup-grid.component';

describe('LookupGridComponent', () => {
  let component: LookupGridComponent;
  let fixture: ComponentFixture<LookupGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LookupGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LookupGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});

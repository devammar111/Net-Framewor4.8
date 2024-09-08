import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LookupTypeGridComponent } from './lookup-type-grid.component';

describe('LookupTypeGridComponent', () => {
  let component: LookupTypeGridComponent;
  let fixture: ComponentFixture<LookupTypeGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LookupTypeGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LookupTypeGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});

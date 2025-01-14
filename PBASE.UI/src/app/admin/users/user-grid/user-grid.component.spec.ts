import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserGridComponent } from './user-grid.component';

describe('UserGridComponent', () => {
  let component: UserGridComponent;
  let fixture: ComponentFixture<UserGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});

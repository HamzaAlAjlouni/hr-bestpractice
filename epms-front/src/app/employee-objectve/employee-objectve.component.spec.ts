import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeObjectveComponent } from './employee-objectve.component';

describe('EmployeeObjectveComponent', () => {
  let component: EmployeeObjectveComponent;
  let fixture: ComponentFixture<EmployeeObjectveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeObjectveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeObjectveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

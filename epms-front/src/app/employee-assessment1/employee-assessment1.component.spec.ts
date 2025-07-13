import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeAssessment1Component } from './employee-assessment1.component';

describe('EmployeeAssessment1Component', () => {
  let component: EmployeeAssessment1Component;
  let fixture: ComponentFixture<EmployeeAssessment1Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeAssessment1Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeAssessment1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

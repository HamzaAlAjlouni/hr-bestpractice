import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeEducationPlanComponent } from './employee-education-plan.component';

describe('EmployeeEducationPlanComponent', () => {
  let component: EmployeeEducationPlanComponent;
  let fixture: ComponentFixture<EmployeeEducationPlanComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeEducationPlanComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeEducationPlanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeDevelopmentPlanComponent } from './employee-development-plan.component';

describe('EmployeeDevelopmentPlanComponent', () => {
  let component: EmployeeDevelopmentPlanComponent;
  let fixture: ComponentFixture<EmployeeDevelopmentPlanComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeDevelopmentPlanComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeDevelopmentPlanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

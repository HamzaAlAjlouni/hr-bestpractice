import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeProjectCompetencyComponent } from './employee-project-competency.component';

describe('EmployeeProjectCompetencyComponent', () => {
  let component: EmployeeProjectCompetencyComponent;
  let fixture: ComponentFixture<EmployeeProjectCompetencyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeProjectCompetencyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeProjectCompetencyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

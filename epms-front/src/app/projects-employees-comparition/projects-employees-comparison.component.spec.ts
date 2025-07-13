import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectsEmployeesComparisonComponent } from './projects-employees-comparison.component';

describe('ProjectsEmployeesComparitionComponent', () => {
  let component: ProjectsEmployeesComparisonComponent;
  let fixture: ComponentFixture<ProjectsEmployeesComparisonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectsEmployeesComparisonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectsEmployeesComparisonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

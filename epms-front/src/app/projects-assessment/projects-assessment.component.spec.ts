import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectsAssessmentComponent } from './projects-assessment.component';

describe('ProjectsAssessmentComponent', () => {
  let component: ProjectsAssessmentComponent;
  let fixture: ComponentFixture<ProjectsAssessmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectsAssessmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectsAssessmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

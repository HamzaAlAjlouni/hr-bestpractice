import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectEvaluationComponent } from './project-evaluation.component';

describe('ProjectEvaluationComponent', () => {
  let component: ProjectEvaluationComponent;
  let fixture: ComponentFixture<ProjectEvaluationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectEvaluationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectEvaluationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

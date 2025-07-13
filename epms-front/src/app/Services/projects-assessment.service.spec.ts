import { TestBed } from '@angular/core/testing';

import { ProjectsAssessmentService } from './projects-assessment.service';

describe('ProjectsAssessmentService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProjectsAssessmentService = TestBed.get(ProjectsAssessmentService);
    expect(service).toBeTruthy();
  });
});

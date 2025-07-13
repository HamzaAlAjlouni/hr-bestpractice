import { TestBed } from '@angular/core/testing';

import {ProjectEvaluationService} from "./project-evaluation.service";

describe('ProjectEvaluationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProjectEvaluationService = TestBed.get(ProjectEvaluationService);
    expect(service).toBeTruthy();
  });
});

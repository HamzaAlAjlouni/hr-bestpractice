import { TestBed } from '@angular/core/testing';
import {ProjectEvaluationValuesService} from "./project-evaluation-values.service";

describe('ProjectEvaluationValuesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProjectEvaluationValuesService = TestBed.get(ProjectEvaluationValuesService);
    expect(service).toBeTruthy();
  });
});

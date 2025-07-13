import { TestBed } from '@angular/core/testing';

import { EmployeeAssesmentService } from './employee-assesment.service';

describe('EmployeeAssesmentService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EmployeeAssesmentService = TestBed.get(EmployeeAssesmentService);
    expect(service).toBeTruthy();
  });
});



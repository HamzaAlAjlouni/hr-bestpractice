import { TestBed } from '@angular/core/testing';

import { AuthExcludedActionsService } from './auth-excluded-actions.service';

describe('AuthExcludedActionsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AuthExcludedActionsService = TestBed.get(AuthExcludedActionsService);
    expect(service).toBeTruthy();
  });
});

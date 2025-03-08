import { TestBed } from '@angular/core/testing';

import { UseAuthService } from './use-auth.service';

describe('UseAuthService', () => {
  let service: UseAuthService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UseAuthService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

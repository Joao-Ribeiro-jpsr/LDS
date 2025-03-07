import { TestBed } from '@angular/core/testing';

import { RateRestService } from './rate-rest.service';

describe('RateRestService', () => {
  let service: RateRestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RateRestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { FakeApiRestService } from './fake-api-rest.service';

describe('FakeApiRestService', () => {
  let service: FakeApiRestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FakeApiRestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

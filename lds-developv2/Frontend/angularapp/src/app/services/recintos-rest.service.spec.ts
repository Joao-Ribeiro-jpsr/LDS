import { TestBed } from '@angular/core/testing';

import { RecintosRestService } from './recintos-rest.service';

describe('RecintosRestService', () => {
  let service: RecintosRestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RecintosRestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

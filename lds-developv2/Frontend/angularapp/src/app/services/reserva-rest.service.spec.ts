import { TestBed } from '@angular/core/testing';

import { ReservaRestService } from './reserva-rest.service';

describe('ReservaRestService', () => {
  let service: ReservaRestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReservaRestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

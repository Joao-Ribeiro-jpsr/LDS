import { TestBed } from '@angular/core/testing';

import { PagamentosRestService } from './pagamentos-rest.service';

describe('PagamentosRestService', () => {
  let service: PagamentosRestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PagamentosRestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

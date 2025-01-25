import { TestBed } from '@angular/core/testing';

import { MagazinesServiceService } from './magazines-service.service';

describe('MagazinesServiceService', () => {
  let service: MagazinesServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MagazinesServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { CategoriesServiceService } from './categories-service.service';

describe('CategoriesServiceService', () => {
  let service: CategoriesServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CategoriesServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

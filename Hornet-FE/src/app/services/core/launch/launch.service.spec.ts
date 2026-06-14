import { TestBed } from '@angular/core/testing';

import { LaunchesService } from './launch.service';

describe('LaunchesService', () => {
  let service: LaunchesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LaunchesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

import { TestBed, inject } from '@angular/core/testing';

import { DiagnoseSharedService } from './diagnose-shared.service';

describe('DiagnoseSharedService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DiagnoseSharedService]
    });
  });

  it('should be created', inject([DiagnoseSharedService], (service: DiagnoseSharedService) => {
    expect(service).toBeTruthy();
  }));
});

import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { isletmeGuard } from './isletme.guard';

describe('isletmeGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => isletmeGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { FeatureTogglesService } from './feature-toggles.service';

describe('FeatureTogglesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FeatureTogglesService = TestBed.get(FeatureTogglesService);
    expect(service).toBeTruthy();
  });
});

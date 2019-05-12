import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FeatureTogglesComponent } from './feature-toggles.component';

describe('FeatureTogglesComponent', () => {
  let component: FeatureTogglesComponent;
  let fixture: ComponentFixture<FeatureTogglesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FeatureTogglesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FeatureTogglesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

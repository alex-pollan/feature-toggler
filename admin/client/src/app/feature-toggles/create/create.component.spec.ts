import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FeatureTogglesCreateComponent } from './create.component';

describe('CreateComponent', () => {
  let component: FeatureTogglesCreateComponent;
  let fixture: ComponentFixture<FeatureTogglesCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FeatureTogglesCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FeatureTogglesCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

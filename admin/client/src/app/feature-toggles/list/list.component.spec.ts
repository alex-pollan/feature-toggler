import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FeatureTogglesListComponent } from './list.component';

describe('FeatureTogglesListComponent', () => {
  let component: FeatureTogglesListComponent;
  let fixture: ComponentFixture<FeatureTogglesListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FeatureTogglesListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FeatureTogglesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

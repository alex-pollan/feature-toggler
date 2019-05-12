import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FeatureTogglesComponent } from './feature-toggles/feature-toggles.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    FeatureTogglesComponent
  ],
  exports: [
    FeatureTogglesComponent
  ]
})
export class FeatureTogglesModule { }

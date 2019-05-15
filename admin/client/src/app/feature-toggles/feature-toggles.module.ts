import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { FeatureTogglesListComponent } from './list/list.component';
import { FeatureTogglesCreateComponent } from './create/create.component';
import { FeatureTogglesComponent } from './feature-toggles/feature-toggles.component';

import { FeatureTogglesRoutingModule } from './feature-toggles-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    NgbModule,
    FeatureTogglesRoutingModule
  ],
  declarations: [
    FeatureTogglesListComponent,
    FeatureTogglesCreateComponent,
    FeatureTogglesComponent
  ],
  exports: [
  ]
})
export class FeatureTogglesModule { }

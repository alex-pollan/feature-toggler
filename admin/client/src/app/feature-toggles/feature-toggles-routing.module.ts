import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FeatureTogglesComponent } from './feature-toggles/feature-toggles.component';
import { AuthGuard } from '../auth/auth.guard';
import { FeatureTogglesListComponent } from './list/list.component';
import { FeatureTogglesCreateComponent } from './create/create.component';

const ftRoutes: Routes = [{
    path: 'feature-toggles',
    component: FeatureTogglesComponent,
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
    children: [
        {
            path: 'create',
            component: FeatureTogglesCreateComponent,
        },
        {
            path: '',
            component: FeatureTogglesListComponent
        },
    ]
}];

@NgModule({
    imports: [
        RouterModule.forChild(ftRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class FeatureTogglesRoutingModule { }

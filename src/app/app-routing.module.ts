import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ApplicationsComponent } from './applications/applications.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { FeatureTogglesComponent } from './feature-toggles/feature-toggles/feature-toggles.component';
import { AuthGuard } from './auth/auth.guard';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';

const routes: Routes = [
  {
    path: 'applications',
    component: ApplicationsComponent,
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
  },
  // TODO: use children structure
  {
    path: 'applications/:appId/feature-toggles',
    component: FeatureTogglesComponent,
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
  },
  { path: 'unauthorized', component: UnauthorizedComponent },
  { path: '', redirectTo: '/applications', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      enableTracing: true
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }

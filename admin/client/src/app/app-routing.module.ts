import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { FeatureTogglesComponent } from './feature-toggles/feature-toggles/feature-toggles.component';
import { AuthGuard } from './auth/auth.guard';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';

const routes: Routes = [
  {
    path: 'feature-toggles',
    component: FeatureTogglesComponent,
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
  },
  { path: 'unauthorized', component: UnauthorizedComponent },
  { path: '', redirectTo: '/feature-toggles', pathMatch: 'full' },
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

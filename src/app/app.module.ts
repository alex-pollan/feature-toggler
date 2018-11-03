import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ApplicationsComponent } from './applications/applications.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { FeatureTogglesModule } from './feature-toggles/feature-toggles.module';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';

@NgModule({
  declarations: [
    AppComponent,
    ApplicationsComponent,
    PageNotFoundComponent,
    UnauthorizedComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FeatureTogglesModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

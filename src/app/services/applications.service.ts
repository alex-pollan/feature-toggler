import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { TogglesApplication } from '../models/toggles-application';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApplicationsService {
  togglesApplications: TogglesApplication[] = [];

  constructor(private httpClient: HttpClient) { }

  getAll(): Observable<any> {
    this.togglesApplications.splice(0, this.togglesApplications.length);
    this.httpClient.get<any>('assets/applications.json')
      .subscribe(data => {
        this.togglesApplications.push(...data.applications);
      });

    return of(this.togglesApplications);
  }

  add(application: TogglesApplication) {
    this.togglesApplications.push(application);
  }

  remove(application: TogglesApplication) {
    application.isDeleted = true;
    this.togglesApplications.splice(this.togglesApplications.indexOf(application), 1);
  }
}

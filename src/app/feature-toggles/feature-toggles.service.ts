import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FeatureTogglesService {
  constructor(private httpClient: HttpClient) { }

  getAll(appId: string): Observable<any> {
    return this.httpClient.get<any>(`assets/ft-${appId}.json`);
  }
}

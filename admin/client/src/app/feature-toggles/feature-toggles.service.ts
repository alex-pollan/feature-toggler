import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { Toggle } from '../models/toggle';
import { catchError } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

const apiUrl = 'https://localhost:44314/api/featuretoggle';

@Injectable({
  providedIn: 'root'
})
export class FeatureTogglesService {
  constructor(private httpClient: HttpClient) { }

  getAll(): Observable<any> {
    return this.httpClient.get<any>(apiUrl);
  }

  create(toggle: Toggle) {
    return this.httpClient.post<Toggle>(apiUrl, toggle, httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  enable(toggle: Toggle, enable: boolean) {
    const patchRequest = [{
      operation: 'enable',
      value: enable
    }];

    return this.httpClient.patch(`${apiUrl}/${toggle.id}`, patchRequest, httpOptions);
  }

  isDuplicated(value: any): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.httpClient.get<any>(`${apiUrl}/exists/${value}`)
        .subscribe(
          d => resolve(!d.exists),
          err => {
            reject(err);
          }
        );
    });
  }

  delete(toggle: Toggle) {
    return this.httpClient.delete(`${apiUrl}/${toggle.id}`, httpOptions);
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  }
}

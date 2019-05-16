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

@Injectable({
  providedIn: 'root'
})
export class FeatureTogglesService {
  constructor(private httpClient: HttpClient) { }

  getAll(): Observable<any> {
    return this.httpClient.get<any>('https://localhost:44314/api/featuretoggle');
  }

  create(toggle: Toggle) {
    return this.httpClient.post<Toggle>('https://localhost:44314/api/featuretoggle', toggle, httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  enable(toggle: Toggle, enable: boolean) {
    const patchRequest = {
      id: toggle.id,
      propertyName: 'enable',
      propertyValue: enable
    };

    return this.httpClient.patch('https://localhost:44314/api/featuretoggle', patchRequest, httpOptions);
  }

  isDuplicated(value: any): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.httpClient.get<any>(`https://localhost:44314/api/featuretoggle/exists/${value}`)
        .subscribe(
          d => resolve(!d.exists),
          err => {
            reject(err);
          }
        );
    });
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

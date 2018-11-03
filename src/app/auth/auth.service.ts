import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authorized = null;

  constructor(private httpClient: HttpClient) { }

  isAuthorized(): Observable<boolean> {
    if (this.authorized !== null) {
      return of(this.authorized);
    }

    return Observable.create(observer => {
      // TODO:
      // this.httpClient.get<any>('assets/check-is-authorized.json')
      //   .subscribe(
      //     () => {
      //       this.authorized = true;
      //       observer.next(this.authorized);
      //     },
      //     () => {
      //       this.authorized = true;
      //       observer.next(this.authorized);
      //     }
      //   );
      this.authorized = true;
      observer.next(this.authorized);
    });
  }
}

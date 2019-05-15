import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private subjects: { [index: string]: Subject<any> } = {};

  broadcast(id: string, args: any) {
    this.ensureSubject(id).next({ id, args });
  }

  clear(id: string) {
    if (this.subjects[id]) {
      this.subjects[id].next();
    }
  }

  listen(id: string): Observable<any> {
    return this.ensureSubject(id).asObservable();
  }

  private ensureSubject(id: string): Subject<any> {
    if (!this.subjects[id]) {
      this.subjects[id] = new Subject<any>();
    }

    return this.subjects[id];
  }
}

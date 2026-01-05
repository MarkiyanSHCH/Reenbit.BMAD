import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { EventDto, RequestEvent } from '../models/event.model';

@Injectable({ providedIn: 'root' })
export class EventApiService {
  private readonly http = inject(HttpClient);

  private readonly baseUrl = 'https://localhost:7132';

  createEvent(payload: RequestEvent): Observable<null> {
    return this.http.post<null>(`${this.baseUrl}/api/events`, payload);
  }

  getEvents(): Observable<EventDto[]> {
    return this.http.get<EventDto[]>(`${this.baseUrl}/api/events`)
      .pipe(map((response: any) => response.data));
  }
}

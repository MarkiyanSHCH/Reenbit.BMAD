import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { map } from 'rxjs/operators';

import { EventApiService } from '../../core/api/event-api.service';
import { EventDto, EventType } from '../../core/models/event.model';

@Component({
  standalone: true,
  selector: 'app-event-list',
  imports: [CommonModule, RouterModule],
  templateUrl: './event-list.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EventListComponent {
  private readonly api = inject(EventApiService);

  readonly EVENT_TYPE_LABEL: Record<number, string> = {
    [EventType.PageView]: 'PageView',
    [EventType.Click]: 'Click',
    [EventType.Purchase]: 'Purchase',
  };

  readonly items$ = this.api.getEvents().pipe(
    map((res: any) => (Array.isArray(res) ? res : (res?.items ?? [])) as EventDto[]),
  );

  typeLabel(t: EventType): string {
    return this.EVENT_TYPE_LABEL[t] ?? String(t);
  }
}

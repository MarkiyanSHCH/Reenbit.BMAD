import { EventType } from './event.model';

export const EVENT_TYPE_LABEL: Record<EventType, string> = {
  [EventType.PageView]: 'PageView',
  [EventType.Click]: 'Click',
  [EventType.Purchase]: 'Purchase',
};

export function toEventTypeLabel(t: EventType | null | undefined): string {
  if (!t) return '';
  return EVENT_TYPE_LABEL[t] ?? String(t);
}

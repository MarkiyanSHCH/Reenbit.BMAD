export enum EventType {
  PageView = 1,
  Click = 2,
  Purchase = 3,
}

export interface EventDto {
  id?: string;
  userId: string;
  type: EventType;
  description: string;
  createdAt?: Date;
}

export interface RequestEvent {
  userId: string;
  type: EventType;
  description: string;
}

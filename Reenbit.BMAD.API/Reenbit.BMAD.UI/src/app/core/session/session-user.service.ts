import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class SessionUserService {
  private readonly key = 'eventhub.userId';

  getOrCreateUserId(): string {
    const existing = sessionStorage.getItem(this.key);
    if (existing) return existing;

    const id = this.newGuid();
    sessionStorage.setItem(this.key, id);
    return id;
  }

  reset(): string {
    const id = this.newGuid();
    sessionStorage.setItem(this.key, id);
    return id;
  }

  private newGuid(): string {
    return crypto.randomUUID();
  }
}

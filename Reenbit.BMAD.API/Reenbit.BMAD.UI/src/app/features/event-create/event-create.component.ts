import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { finalize } from 'rxjs/operators';

import { EventApiService } from '../../core/api/event-api.service';
import { SessionUserService } from '../../core/session/session-user.service';
import { EventType } from '../../core/models/event.model';

@Component({
  standalone: true,
  selector: 'app-event-create',
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './event-create.component.html',
})
export class EventCreateComponent {
  private readonly fb = inject(FormBuilder).nonNullable;
  private readonly api = inject(EventApiService);
  private readonly router = inject(Router);
  private readonly sessionUser = inject(SessionUserService);

  readonly userId = this.sessionUser.getOrCreateUserId();

  readonly types = [
    { value: EventType.PageView, label: 'PageView' },
    { value: EventType.Click, label: 'Click' },
    { value: EventType.Purchase, label: 'Purchase' },
  ];

  isSubmitting = false;
  error: string | null = null;

  // userId тут більше немає
  readonly form = this.fb.group({
    type: this.fb.control(EventType.PageView, { validators: [Validators.required] }),
    description: this.fb.control('', { validators: [Validators.required, Validators.maxLength(2000)] }),
  });

  submit(): void {
    this.error = null;

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;

    const payload = {
      userId: this.userId,
      ...this.form.getRawValue(),
    };

    this.api.createEvent(payload)
      .pipe(finalize(() => (this.isSubmitting = false)))
      .subscribe({
        next: () => this.router.navigateByUrl('/events'),
        error: (err) => {
          this.error = err?.error?.message ?? err?.message ?? 'Failed to create event';
        },
      });
  }
}

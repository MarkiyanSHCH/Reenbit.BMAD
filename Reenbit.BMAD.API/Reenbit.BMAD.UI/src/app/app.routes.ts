import { Routes } from '@angular/router';
import { EventCreateComponent } from './features/event-create/event-create.component';
import { EventListComponent } from './features/event-list/event-list.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'events' },
  { path: 'events', component: EventListComponent },
  { path: 'events/new', component: EventCreateComponent },
  { path: '**', redirectTo: 'events' },
];

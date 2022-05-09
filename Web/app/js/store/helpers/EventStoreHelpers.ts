import { EventResponse } from '@/response/EventResponse';

export enum EventStoreMutations {
  SetEventsData = 'event_setEventsData',
  SetSlug = 'event_setSlug',
  AddEvent = 'event_addEvent',
}

export interface EventStoreState {
  _slug: string;
  _events: EventResponse[];
  _eventsReady: boolean;
}

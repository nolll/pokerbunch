import { Player } from '@/models/Player';
import { EventResponse } from '@/response/EventResponse';

export enum EventStoreGetters {
    Slug = 'event_slug',
    Events = 'event_events',
    EventsReady = 'event_eventsReady'
}

export enum EventStoreActions {
    LoadEvents = 'event_loadEvents'
}

export enum EventStoreMutations {
    SetEventsData = 'event_setEventsData',
    SetInitialized = 'event_setInitialized'
}

export interface EventStoreState {
    _slug: string;
    _events: EventResponse[];
    _eventsReady: boolean;
    _initialized: boolean;
}

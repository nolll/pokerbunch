import { EventResponse } from '@/response/EventResponse';

export enum EventStoreGetters {
    Slug = 'event_slug',
    Events = 'event_events',
    EventsReady = 'event_eventsReady'
}

export enum EventStoreActions {
    LoadEvents = 'event_loadEvents',
    AddEvent = 'event_addEvent'
}

export enum EventStoreMutations {
    SetEventsData = 'event_setEventsData',
    SetSlug = 'event_setSlug',
    AddEvent = 'event_addEvent'
}

export interface EventStoreState {
    _slug: string;
    _events: EventResponse[];
    _eventsReady: boolean;
}

export interface AddEventParams{
    bunchId: string;
    name: string;
}

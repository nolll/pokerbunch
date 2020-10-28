import { StoreOptions } from 'vuex';
import api from '@/api';
import { EventStoreGetters, EventStoreActions, EventStoreMutations, EventStoreState, AddEventParams } from '@/store/helpers/EventStoreHelpers';
import { EventResponse } from '@/response/EventResponse';

export default {
    namespaced: false,
    state: {
        _slug: '',
        _events: [],
        _eventsReady: false
    },
    getters: {
        [EventStoreGetters.Slug]: state => state._slug,
        [EventStoreGetters.Events]: state => state._events,
        [EventStoreGetters.EventsReady]: state => state._eventsReady
    },
    actions: {
        async [EventStoreActions.LoadEvents](context, data) {
            if (data.slug !== this.state._slug) {
                context.commit(EventStoreMutations.SetSlug, data.slug);
                const response = await api.getEvents(data.slug);
                context.commit(EventStoreMutations.SetEventsData, response.data);
            }
        },
        async [EventStoreActions.AddEvent](context, data: AddEventParams) {
            if (context.state._eventsReady) {
                const response = await api.addEvent(data.bunchId, { name: data.name });
                context.commit(EventStoreMutations.AddEvent, response.data);
            }
        }
    },
    mutations: {
        [EventStoreMutations.SetEventsData](state, players: EventResponse[]) {
            state._events = players;
            state._eventsReady = true;
        },
        [EventStoreMutations.SetSlug](state, slug: string) {
            state._slug = slug;
        },
        [EventStoreMutations.AddEvent](state, event) {
            state._events.push(event);
        }
    }
} as StoreOptions<EventStoreState>;

import { StoreOptions } from 'vuex';
import api from '@/api';
import { EventStoreGetters, EventStoreActions, EventStoreMutations, EventStoreState } from '@/store/helpers/EventStoreHelpers';
import { EventResponse } from '@/response/EventResponse';

export default {
    namespaced: false,
    state: {
        _slug: '',
        _events: [],
        _eventsReady: false,
        _initialized: false
    },
    getters: {
        [EventStoreGetters.Slug]: state => state._slug,
        [EventStoreGetters.Events]: state => state._events,
        [EventStoreGetters.EventsReady]: state => state._eventsReady
    },
    actions: {
        async [EventStoreActions.LoadEvents](context, data) {
            if (!context.state._initialized) {
                context.commit(EventStoreMutations.SetInitialized);
                const response = await api.getEvents(data.slug);
                context.commit(EventStoreMutations.SetEventsData, response.data);
            }
        }
    },
    mutations: {
        [EventStoreMutations.SetEventsData](state, players: EventResponse[]) {
            state._events = players;
            state._eventsReady = true;
        },
        [EventStoreMutations.SetInitialized](state) {
            state._initialized = true;
        }
    }
} as StoreOptions<EventStoreState>;

import { StoreOptions } from 'vuex';
import { EventStoreMutations, EventStoreState } from '@/store/helpers/EventStoreHelpers';
import { EventResponse } from '@/response/EventResponse';

export default {
  namespaced: false,
  state: {
    _slug: '',
    _events: [],
    _eventsReady: false,
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
    },
  },
} as StoreOptions<EventStoreState>;

import { computed } from 'vue';
import { useStore } from 'vuex';
import { useRoute } from 'vue-router';
import { EventResponse } from '@/response/EventResponse';
import { EventStoreActions, EventStoreGetters } from '@/store/helpers/EventStoreHelpers';

export default function useBunch() {
  const store = useStore();
  const route = useRoute();

  const eventsReady = computed((): boolean => {
    return store.getters[EventStoreGetters.EventsReady];
  });

  const events = computed((): EventResponse[] => {
    return store.getters[EventStoreGetters.Events];
  });

  const getEvent = (id: string): EventResponse | null => {
    return events.value.find((e) => e.id.toString() === id) || null;
  };

  const loadEvents = () => {
    store.dispatch(EventStoreActions.LoadEvents, { slug: route.params.slug });
  };

  const addEvent = (name: string) => {
    store.dispatch(EventStoreActions.AddEvent, { bunchId: route.params.slug, name });
  };

  return {
    eventsReady,
    events,
    getEvent,
    loadEvents,
    addEvent,
  };
}

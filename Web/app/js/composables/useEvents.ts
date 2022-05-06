import { computed } from 'vue';
import { useStore } from 'vuex';
import { useRoute } from 'vue-router';
import { EventResponse } from '@/response/EventResponse';
import { EventStoreMutations } from '@/store/helpers/EventStoreHelpers';
import api from '@/api';

export default function useEvents() {
  const store = useStore();
  const route = useRoute();

  const eventsReady = computed((): boolean => {
    return store.state.event._eventsReady;
  });

  const events = computed((): EventResponse[] => {
    return store.state.event._events;
  });

  const getEvent = (id: string): EventResponse | null => {
    return events.value.find((e) => e.id.toString() === id) || null;
  };

  const loadEvents = async () => {
    const slug = route.params.slug as string;
    if (slug !== store.state.event._slug) {
      store.commit(EventStoreMutations.SetSlug, slug);
      const response = await api.getEvents(slug);
      store.commit(EventStoreMutations.SetEventsData, response.data);
    }
  };

  const addEvent = async (name: string) => {
    const slug = route.params.slug as string;
    if (store.state.event._eventsReady) {
      const response = await api.addEvent(slug, { name: name });
      store.commit(EventStoreMutations.AddEvent, response.data);
    }
  };

  return {
    eventsReady,
    events,
    getEvent,
    loadEvents,
    addEvent,
  };
}

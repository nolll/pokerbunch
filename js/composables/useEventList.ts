import { computed } from 'vue';
import { useEventListQuery } from '@/queries/eventQueries';
import { EventResponse } from '@/response/EventResponse';

export default function usePlayerList(slug: string) {
  const eventListQuery = useEventListQuery(slug);

  const events = computed((): EventResponse[] => {
    return eventListQuery.data.value ?? [];
  });

  const eventsReady = computed((): boolean => {
    return !eventListQuery.isPending.value;
  });

  const getEvent = (id: string): EventResponse => {
    const e = events.value.find((o) => o.id === id);
    if (!e) throw new Error(`event not found: ${id}`);
    return e;
  };

  return {
    eventsReady,
    events,
    getEvent,
  };
}

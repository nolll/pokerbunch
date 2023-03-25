import api from '@/api';
import { useQuery } from 'vue-query';

export const eventsQueryKey = (slug: string) => ['events', slug];

export const useEventsQuery = (slug: string) => {
  return useQuery(eventsQueryKey(slug), () => api.getEvents(slug), {
    select: (response) => response.data,
  });
};

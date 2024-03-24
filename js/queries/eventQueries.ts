import api from '@/api';
import { useQuery } from '@tanstack/vue-query';
import { eventListKey } from './queryKeys';
import { EventResponse } from '@/response/EventResponse';
import { fiveMinuteStaleTime } from './staleTimes';

export const useEventListQuery = (slug: string) => {
  return useQuery({
    queryKey: eventListKey(slug),
    queryFn: async (): Promise<EventResponse[]> => {
      const response = await api.getEvents(slug);
      return response.data;
    },
    staleTime: fiveMinuteStaleTime,
  });
};

import api from '@/api';
import { useQuery } from '@tanstack/vue-query';
import { eventListKey } from './queryKeys';
import { EventResponse } from '@/response/EventResponse';

const fetchEvents = async (slug: string): Promise<EventResponse[]> => {
  const response = await api.getEvents(slug);
  return response.data;
};

export const useEventListQuery = (slug: string) => {
  return useQuery({ queryKey: eventListKey(slug), queryFn: () => fetchEvents(slug) });
};

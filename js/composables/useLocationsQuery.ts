import api from '@/api';
import { useQuery } from 'vue-query';

export function useLocationsQuery(slug: string) {
  return useQuery(['locations', slug], () => api.getLocations(slug), {
    select: (response) => response.data,
  });
}

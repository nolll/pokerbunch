import api from '@/api';
import { useQuery } from 'vue-query';

export function useLocationQuery(slug: string, id: string) {
  return useQuery(['location', slug, id], () => api.getLocation(slug, id), {
    select: (response) => response.data,
  });
}

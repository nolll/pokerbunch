import api from '@/api';
import { useQuery } from 'vue-query';

const locationQueryKey = (id: string) => ['location', id];
export const locationsQueryKey = (slug: string) => ['locations', slug];

export const useLocationQuery = (id: string) => {
  return useQuery(locationQueryKey(id), () => api.getLocation(id), {
    select: (response) => response.data,
  });
};

export const useLocationsQuery = (slug: string) => {
  return useQuery(locationsQueryKey(slug), () => api.getLocations(slug), {
    select: (response) => response.data,
  });
};

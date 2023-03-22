import api from '@/api';
import { useQuery, useMutation } from 'vue-query';

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

export const useAddLocationMutation = (slug: string, onSuccess: () => void | Promise<unknown>) => {
  return useMutation({
    mutationFn: (location: object) => api.addLocation(slug, location),
    onSuccess: () => onSuccess(),
  });
};

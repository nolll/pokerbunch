import api from '@/api';
import { useQuery, useMutation } from 'vue-query';

function locationQueryKey(id: string) {
  return ['location', id];
}

export function locationsQueryKey(slug: string) {
  return ['locations', slug];
}

export function useLocationQuery(id: string) {
  return useQuery(locationQueryKey(id), () => api.getLocation(id), {
    select: (response) => response.data,
  });
}

export function useLocationsQuery(slug: string) {
  return useQuery(locationsQueryKey(slug), () => api.getLocations(slug), {
    select: (response) => response.data,
  });
}

export function useAddLocationMutation(slug: string, onSuccess: () => void | Promise<unknown>) {
  return useMutation({
    mutationFn: (location: object) => api.addLocation(slug, location),
    onSuccess: () => onSuccess(),
  });
}

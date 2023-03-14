import api from '@/api';
import { useQuery, useMutation } from 'vue-query';

const queryKey = 'locations';

function locationQueryKey(slug: string, id: string) {
  return [queryKey, slug, id];
}

export function locationsQueryKey(slug: string) {
  return [queryKey, slug];
}

export function useLocationQuery(slug: string, id: string) {
  return useQuery(locationQueryKey(slug, id), () => api.getLocation(slug, id), {
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

import { computed } from 'vue';
import { useLocationListQuery } from '@/queries/locationQueries';
import { LocationResponse } from '@/response/LocationResponse';

export default function useLocationList(slug: string) {
  const locationListQuery = useLocationListQuery(slug);

  const locations = computed((): LocationResponse[] => locationListQuery.data.value ?? []);
  const locationsReady = computed((): boolean => !locationListQuery.isPending.value);
  const getLocation = (id: string): LocationResponse | null => locations.value.find((l) => l.id.toString() === id) || null;

  return {
    locationsReady,
    locations,
    getLocation,
  };
}

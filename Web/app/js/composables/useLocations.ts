import { BunchStoreActions, BunchStoreGetters } from '@/store/helpers/BunchStoreHelpers';
import { computed } from 'vue';
import { useStore } from 'vuex';
import { useRoute } from 'vue-router';
import { LocationStoreActions, LocationStoreGetters } from '@/store/helpers/LocationStoreHelpers';
import { LocationResponse } from '@/response/LocationResponse';

export default function useLocations() {
  const store = useStore();
  const route = useRoute();

  const locationsReady = computed((): boolean => {
    return store.getters[LocationStoreGetters.LocationsReady];
  });

  const locations = computed((): LocationResponse[] => {
    return store.getters[LocationStoreGetters.Locations];
  });

  const getLocation = (id: string): LocationResponse | null => {
    return locations.value.find((l) => l.id.toString() === id) || null;
  };

  const loadLocations = () => {
    store.dispatch(LocationStoreActions.LoadLocations, { slug: route.params.slug });
  };

  const addLocation = (name: string) => {
    store.dispatch(LocationStoreActions.AddLocation, { bunchId: route.params.slug, name });
  };

  return {
    locationsReady,
    locations,
    getLocation,
    loadLocations,
    addLocation,
  };
}

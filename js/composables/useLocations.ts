import { computed } from 'vue';
import { useStore } from 'vuex';
import { useRoute } from 'vue-router';
import { LocationStoreMutations } from '@/store/helpers/LocationStoreHelpers';
import { LocationResponse } from '@/response/LocationResponse';
import api from '@/api';

export default function useLocations() {
  const store = useStore();
  const route = useRoute();

  const locationsReady = computed((): boolean => {
    return store.state.location._locationsReady;
  });

  const locations = computed((): LocationResponse[] => {
    return store.state.location._locations;
  });

  const getLocation = (id: string): LocationResponse | null => {
    return locations.value.find((l) => l.id.toString() === id) || null;
  };

  const loadLocations = async () => {
    const slug = route.params.slug as string;
    if (slug !== store.state.location._slug) {
      store.commit(LocationStoreMutations.SetSlug, slug);
      const response = await api.getLocations(slug);
      store.commit(LocationStoreMutations.SetLocationsData, response.data);
    }
  };

  const addLocation = async (name: string) => {
    if (store.state.location._locationsReady) {
      const slug = route.params.slug as string;
      const response = await api.addLocation(slug, { name: name });
      store.commit(LocationStoreMutations.AddLocation, response.data);
    }
  };

  return {
    locationsReady,
    locations,
    getLocation,
    loadLocations,
    addLocation,
  };
}

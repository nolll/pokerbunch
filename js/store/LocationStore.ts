import { StoreOptions } from 'vuex';
import { LocationStoreMutations, LocationStoreState } from '@/store/helpers/LocationStoreHelpers';
import { LocationResponse } from '@/response/LocationResponse';

export default {
  namespaced: false,
  state: {
    _slug: '',
    _locations: [],
    _locationsReady: false,
  },
  mutations: {
    [LocationStoreMutations.SetLocationsData](state, players: LocationResponse[]) {
      state._locations = players;
      state._locationsReady = true;
    },
    [LocationStoreMutations.SetSlug](state, slug: string) {
      state._slug = slug;
    },
    [LocationStoreMutations.AddLocation](state, location) {
      state._locations.push(location);
    },
  },
} as StoreOptions<LocationStoreState>;

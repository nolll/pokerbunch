import { StoreOptions } from 'vuex';
import api from '@/api';
import { LocationStoreGetters, LocationStoreActions, LocationStoreMutations, LocationStoreState, AddLocationParams } from '@/store/helpers/LocationStoreHelpers';
import { LocationResponse } from '@/response/LocationResponse';

export default {
    namespaced: false,
    state: {
        _slug: '',
        _locations: [],
        _locationsReady: false,
        _initialized: false
    },
    getters: {
        [LocationStoreGetters.Slug]: state => state._slug,
        [LocationStoreGetters.Locations]: state => state._locations,
        [LocationStoreGetters.LocationsReady]: state => state._locationsReady
    },
    actions: {
        async [LocationStoreActions.LoadLocations](context, data) {
            if (!context.state._initialized) {
                context.commit(LocationStoreMutations.SetInitialized);
                const response = await api.getLocations(data.slug);
                context.commit(LocationStoreMutations.SetLocationsData, response.data);
            }
        },
        async [LocationStoreActions.AddLocation](context, data: AddLocationParams) {
            if (context.state._locationsReady) {
                const response = await api.addLocation(data.bunchId, { name: data.name });
                context.commit(LocationStoreMutations.AddLocation, response.data);
            }
        }
    },
    mutations: {
        [LocationStoreMutations.SetLocationsData](state, players: LocationResponse[]) {
            state._locations = players;
            state._locationsReady = true;
        },
        [LocationStoreMutations.SetInitialized](state) {
            state._initialized = true;
        },
        [LocationStoreMutations.AddLocation](state, location) {
            state._locations.push(location);
        }
    }
} as StoreOptions<LocationStoreState>;

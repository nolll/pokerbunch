import { StoreOptions } from 'vuex';
import api from '@/api';
import { TimezoneStoreGetters, TimezoneStoreActions, TimezoneStoreMutations, TimezoneStoreState } from '@/store/helpers/TimezoneStoreHelpers';
import { Timezone } from '@/models/Timezone';

export default {
    namespaced: false,
    state: {
        _timezones: [],
        _timezonesReady: false,
        _initialized: false
    },
    getters: {
        [TimezoneStoreGetters.Timezones]: state => state._timezones,
        [TimezoneStoreGetters.TimezonesReady]: state => state._timezonesReady
    },
    actions: {
        async [TimezoneStoreActions.LoadTimezones](context) {
            if (!context.state._initialized) {
                context.commit(TimezoneStoreMutations.SetInitialized);
                const response = await api.getTimezones();
                context.commit(TimezoneStoreMutations.SetTimezonesData, response.data);
            }
        }
    },
    mutations: {
        [TimezoneStoreMutations.SetTimezonesData](state, timezones: Timezone[]) {
            state._timezones = timezones;
            state._timezonesReady = true;
        },
        [TimezoneStoreMutations.SetInitialized](state) {
            state._initialized = true;
        }
    }
} as StoreOptions<TimezoneStoreState>;

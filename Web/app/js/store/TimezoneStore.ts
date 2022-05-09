import { StoreOptions } from 'vuex';
import { TimezoneStoreMutations, TimezoneStoreState } from '@/store/helpers/TimezoneStoreHelpers';
import { Timezone } from '@/models/Timezone';

export default {
  namespaced: false,
  state: {
    _timezones: [],
    _timezonesReady: false,
  },
  mutations: {
    [TimezoneStoreMutations.SetTimezonesData](state, timezones: Timezone[]) {
      state._timezones = timezones;
      state._timezonesReady = true;
    },
  },
} as StoreOptions<TimezoneStoreState>;

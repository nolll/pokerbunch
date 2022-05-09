import { computed } from 'vue';
import { useStore } from 'vuex';
import { TimezoneStoreMutations } from '@/store/helpers/TimezoneStoreHelpers';
import { Timezone } from '@/models/Timezone';
import api from '@/api';

export default function useTimezones() {
  const store = useStore();

  const timezonesReady = computed((): boolean => {
    return store.state.timezone._timezonesReady;
  });

  const timezones = computed((): Timezone[] => {
    return store.state.timezone._timezones;
  });

  const loadTimezones = async () => {
    if (store.state.timezone._timezones.length === 0) {
      const response = await api.getTimezones();
      store.commit(TimezoneStoreMutations.SetTimezonesData, response.data);
    }
  };

  return {
    timezonesReady,
    timezones,
    loadTimezones,
  };
}

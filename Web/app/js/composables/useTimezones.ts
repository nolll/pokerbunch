import { computed } from 'vue';
import { useStore } from 'vuex';
import { TimezoneStoreActions, TimezoneStoreGetters } from '@/store/helpers/TimezoneStoreHelpers';
import { Timezone } from '@/models/Timezone';

export default function useTimezones() {
  const store = useStore();

  const timezonesReady = computed((): boolean => {
    return store.getters[TimezoneStoreGetters.TimezonesReady];
  });

  const timezones = computed((): Timezone[] => {
    return store.getters[TimezoneStoreGetters.Timezones];
  });

  const loadTimezones = () => {
    store.dispatch(TimezoneStoreActions.LoadTimezones);
  };

  return {
    timezonesReady,
    timezones,
    loadTimezones,
  };
}

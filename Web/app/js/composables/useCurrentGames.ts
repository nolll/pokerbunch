import { computed } from 'vue';
import { useStore } from 'vuex';
import { useRoute } from 'vue-router';
import { CurrentGameResponse } from '@/response/CurrentGameResponse';
import { CurrentGameStoreActions, CurrentGameStoreGetters } from '@/store/helpers/CurrentGameStoreHelpers';

export default function useBunch() {
  const store = useStore();
  const route = useRoute();

  const currentGames = computed((): CurrentGameResponse[] => {
    return store.getters[CurrentGameStoreGetters.CurrentGames];
  });

  const currentGamesReady = computed((): boolean => {
    return store.getters[CurrentGameStoreGetters.CurrentGamesReady];
  });

  const loadCurrentGames = () => {
    store.dispatch(CurrentGameStoreActions.LoadCurrentGames, { slug: route.params.slug });
  };

  return {
    currentGames,
    currentGamesReady,
    loadCurrentGames,
  };
}

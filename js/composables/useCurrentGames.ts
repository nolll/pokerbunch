import { computed } from 'vue';
import { useStore } from 'vuex';
import { useRoute } from 'vue-router';
import { CurrentGameResponse } from '@/response/CurrentGameResponse';
import { CurrentGameStoreMutations } from '@/store/helpers/CurrentGameStoreHelpers';
import api from '@/api';

export default function useCurrentGames() {
  const store = useStore();
  const route = useRoute();

  const currentGames = computed((): CurrentGameResponse[] => {
    return store.state.currentGame._currentGames;
  });

  const currentGamesReady = computed((): boolean => {
    return store.state.currentGame._currentGamesReady;
  });

  const loadCurrentGames = async () => {
    const slug = route.params.slug as string;
    try {
      const response = await api.getCurrentGames(slug);
      if (response.status === 200) {
        store.commit(CurrentGameStoreMutations.DataLoaded, response.data);
      }
      store.commit(CurrentGameStoreMutations.LoadingComplete);
    } catch {
      store.commit(CurrentGameStoreMutations.LoadingComplete);
    }
  };

  return {
    currentGames,
    currentGamesReady,
    loadCurrentGames,
  };
}

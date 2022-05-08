import { StoreOptions } from 'vuex';
import { CurrentGameStoreMutations, CurrentGameStoreState } from '@/store/helpers/CurrentGameStoreHelpers';
import { CurrentGameResponse } from '@/response/CurrentGameResponse';

export default {
  namespaced: false,
  state: {
    _currentGames: [],
    _currentGamesReady: false,
  },
  mutations: {
    [CurrentGameStoreMutations.LoadingComplete](state) {
      state._currentGamesReady = true;
    },
    [CurrentGameStoreMutations.DataLoaded](state, data: CurrentGameResponse[]) {
      state._currentGames = data;
    },
  },
} as StoreOptions<CurrentGameStoreState>;

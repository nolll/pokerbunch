import { StoreOptions } from 'vuex';
import { GameArchiveStoreMutations, GameArchiveStoreState } from '@/store/helpers/GameArchiveStoreHelpers';
import { CashgameSortOrder } from '@/models/CashgameSortOrder';
import { CashgamePlayerSortOrder } from '@/models/CashgamePlayerSortOrder';

export default {
  namespaced: false,
  state: {
    _gameSortOrder: CashgameSortOrder.Date,
    _games: [],
    _playerSortOrder: CashgamePlayerSortOrder.Winnings,
    _slug: '',
    _selectedYear: undefined,
    _ready: false,
  },
  mutations: {
    [GameArchiveStoreMutations.SetData](state, games) {
      state._games = games;
      state._ready = true;
    },
    [GameArchiveStoreMutations.SetGameSortorder](state, sortOrder) {
      state._gameSortOrder = sortOrder;
    },
    [GameArchiveStoreMutations.SetPlayerSortorder](state, sortOrder) {
      state._playerSortOrder = sortOrder;
    },
    [GameArchiveStoreMutations.SetSlug](state, slug: string) {
      state._slug = slug;
    },
    [GameArchiveStoreMutations.SetSelectedYear](state, year) {
      state._selectedYear = year ? year : null;
    },
  },
} as StoreOptions<GameArchiveStoreState>;

import { StoreOptions } from 'vuex';
import { PlayerStoreMutations, PlayerStoreState } from '@/store/helpers/PlayerStoreHelpers';
import { Player } from '@/models/Player';

export default {
  namespaced: false,
  state: {
    _slug: '',
    _players: [],
    _playersReady: false,
  },
  mutations: {
    [PlayerStoreMutations.SetPlayersData](state, players: Player[]) {
      state._players = players;
      state._playersReady = true;
    },
    [PlayerStoreMutations.SetSlug](state, slug: string) {
      state._slug = slug;
    },
    [PlayerStoreMutations.AddPlayer](state, player) {
      state._players.push(player);
    },
    [PlayerStoreMutations.DeletePlayer](state, player) {
      for (let i = 0; i < state._players.length; i++) {
        if (state._players[i].id === player.id) {
          state._players.splice(i, 1);
          return;
        }
      }
    },
  },
} as StoreOptions<PlayerStoreState>;

import { StoreOptions } from 'vuex';
import api from '@/api';
import { PlayerStoreGetters, PlayerStoreActions, PlayerStoreMutations, PlayerStoreState } from '@/store/helpers/PlayerStoreHelpers';
import { Player } from '@/models/Player';

export default {
    namespaced: false,
    state: {
        _slug: '',
        _players: [],
        _playersReady: false,
        _initialized: false
    },
    getters: {
        [PlayerStoreGetters.Slug]: state => state._slug,
        [PlayerStoreGetters.Players]: state => state._players,
        [PlayerStoreGetters.PlayersReady]: state => state._playersReady,
        [PlayerStoreGetters.GetPlayer]: (state) => (id: number) => {
            if (!state._players)
                return null;
            let i;
            for (i = 0; i < state._players.length; i++) {
                if (state._players[i].id === id) {
                    return state._players[i];
                }
            }
            return null;
        }
    },
    actions: {
        async [PlayerStoreActions.LoadPlayers](context, data) {
            if (!context.state._initialized) {
                context.commit(PlayerStoreMutations.SetInitialized);
                const response = await api.getPlayers(data.slug);
                context.commit(PlayerStoreMutations.SetPlayersData, response.data);
            }
        }
    },
    mutations: {
        [PlayerStoreMutations.SetPlayersData](state, players: Player[]) {
            state._players = players;
            state._playersReady = true;
        },
        [PlayerStoreMutations.SetInitialized](state) {
            state._initialized = true;
        }
    }
} as StoreOptions<PlayerStoreState>;

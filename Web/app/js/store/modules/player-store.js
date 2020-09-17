import api from '@/api';
import { PlayerStoreGetters, PlayerStoreActions, PlayerStoreMutations } from '@/store/helpers/PlayerStoreHelpers';

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
        [PlayerStoreGetters.GetPlayer]: (state) => (id) => {
            if (!state._players)
                return null;
            var i;
            for (i = 0; i < state._players.length; i++) {
                if (state._players[i].id === id) {
                    return state._players[i];
                }
            }
            return null;
        }
    },
    actions: {
        [PlayerStoreActions.LoadPlayers]: function loadPlayers(context, data) {
            if (!context.state._initialized) {
                context.commit(PlayerStoreMutations.SetInitialized);
                api.getPlayers(data.slug)
                    .then(function (response) {
                        context.commit(PlayerStoreMutations.SetPlayersData, response.data);
                    });
            }
        }
    },
    mutations: {
        [PlayerStoreMutations.SetPlayersData]: function (state, players) {
            state._players = players;
            state._playersReady = true;
        },
        [PlayerStoreMutations.SetInitialized]: function (state) {
            state._initialized = true;
        }
    }
};

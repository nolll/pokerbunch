import api from '@/api';

export default {
    namespaced: true,
    state: {
        _slug: '',
        _players: '',
        _playersReady: false,
        _initialized: false
    },
    getters: {
        slug: state => state._slug,
        players: state => state._players,
        playersReady: state => state._playersReady,
        getPlayer: (state) => (id) => {
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
        loadPlayers(context, data) {
            if (!context.state._initialized) {
                context.commit('setInitialized');
                api.getPlayers(data.slug)
                    .then(function (response) {
                        context.commit('setPlayersData', response.data);
                    });
            }
        }
    },
    mutations: {
        setPlayersData(state, players) {
            //state._slug = bunch.id;
            state._players = players;
            state._playersReady = true;
        },
        setInitialized(state) {
            state._initialized = true;
        }
    }
};

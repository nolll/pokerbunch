import api from '@/api';

export default {
    namespaced: true,
    state: {
        _slug: '',
        _name: '',
        _currencyFormat: '${0}',
        _thousandSeparator: ',',
        _players: '',
        _bunchReady: false,
        _playersReady: false,
        _initialized: false
    },
    getters: {
        slug: state => state._slug,
        name: state => state._name,
        currencyFormat: state => state._currencyFormat,
        thousandSeparator: state => state._thousandSeparator,
        bunchReady: state => state._bunchReady && state._playersReady,
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
        loadBunch(context, data) {
            if (!context.state._initialized) {
                context.commit('setInitialized');
                api.getBunch(data.slug)
                    .then(function (response) {
                        context.commit('setBunchData', response.data);
                    });
                api.getPlayers(data.slug)
                    .then(function (response) {
                        context.commit('setPlayersData', response.data);
                    });
            }
        }
    },
    mutations: {
        setBunchData(state, bunch) {
            state._slug = bunch.id;
            state._name = bunch.name;
            state._currencyFormat = bunch.currencyFormat;
            state._thousandSeparator = bunch.thousandSeparator;
            state._bunchReady = true;
        },
        setPlayersData(state, players) {
            state._players = players;
            state._playersReady = true;
        },
        setInitialized(state) {
            state._initialized = true;
        }
    }
};

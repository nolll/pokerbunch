import api from '@/api';

export default {
    namespaced: true,
    state: {
        _slug: '',
        _name: '',
        _currencyFormat: '${0}',
        _thousandSeparator: ',',
        _ready: false,
        _initialized: false
    },
    getters: {
        slug(state) {
            return state._slug;
        },
        name(state) {
            return state._name;
        },
        currencyFormat(state) {
            return state._currencyFormat;
        },
        thousandSeparator(state) {
            return state._thousandSeparator;
        },
        bunchReady(state) {
            return state._ready;
        }
    },
    actions: {
        loadBunch(context, data) {
            if (!context.state._initialized) {
                context.commit('setInitialized');
                api.getBunch(data.slug)
                    .then(function (response) {
                        context.commit('setData', response.data);
                    });
            }
        }
    },
    mutations: {
        setData(state, bunch) {
            state._slug = bunch.id;
            state._name = bunch.name;
            state._currencyFormat = bunch.currencyFormat;
            state._thousandSeparator = bunch.thousandSeparator;
            state._ready = true;
        },
        setInitialized(state) {
            state._initialized = true;
        }
    }
};

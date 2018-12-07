import api from '../../api';

export default {
    namespaced: true,
    state: {
        slug: '',
        name: '',
        currencyFormat: '${0}',
        thousandSeparator: ',',
        bunchReady: false,
        bunchInitialized: false
    },
    getters: {
        
    },
    actions: {
        loadBunch(context, data) {
            if (!context.state.bunchInitialized) {
                context.commit('setBunchInitialized');
                api.getBunch(data.slug)
                    .then(function (response) {
                        context.commit('setData', response.data);
                    });
            }
        }
    },
    mutations: {
        setData(state, bunch) {
            state.slug = bunch.id;
            state.name = bunch.name;
            state.currencyFormat = bunch.currencyFormat;
            state.thousandSeparator = bunch.thousandSeparator;
            state.bunchReady = true;
        },
        setBunchInitialized(state) {
            state.bunchInitialized = true;
        }
    }
};

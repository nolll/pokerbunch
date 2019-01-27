import api from '@/api';
import roles from '@/roles';

export default {
    namespaced: true,
    state: {
        _slug: '',
        _name: '',
        _currencyFormat: '${0}',
        _thousandSeparator: ',',
        _description: '',
        _houseRules: '',
        _role: roles.none,
        _bunchReady: false,
        _initialized: false
    },
    getters: {
        slug: state => state._slug,
        name: state => state._name,
        currencyFormat: state => state._currencyFormat,
        thousandSeparator: state => state._thousandSeparator,
        description: state => state._description && state._description.length > 0 ? state._description : null,
        houseRules: state => state._houseRules && state._houseRules.length > 0 ? state._houseRules : null,
        isManager: state => state._role === roles.manager || state._role === roles.admin,
        bunchReady: state => state._bunchReady
    },
    actions: {
        loadBunch(context, data) {
            if (!context.state._initialized) {
                context.commit('setInitialized');
                api.getBunch(data.slug)
                    .then(function (response) {
                        context.commit('setBunchData', response.data);
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
            state._description = bunch.description;
            state._houseRules = bunch.houseRules;
            state._role = bunch.role;
            state._bunchReady = true;
        },
        setInitialized(state) {
            state._initialized = true;
        }
    }
};

﻿import api from '@/api';
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
        _defaultBuyin: 0,
        _role: roles.none,
        _playerId: null,
        _bunchReady: false,
        _bunchInitialized: false,
        _userBunches: [],
        _userBunchesReady: false,
        _userBunchesInitialized: false
    },
    getters: {
        slug: state => state._slug,
        name: state => state._name,
        currencyFormat: state => state._currencyFormat,
        thousandSeparator: state => state._thousandSeparator,
        description: state => state._description && state._description.length > 0 ? state._description : null,
        houseRules: state => state._houseRules && state._houseRules.length > 0 ? state._houseRules : null,
        defaultBuyin: state => state._defaultBuyin,
        playerId: state => state._playerId,
        isManager: state => state._role === roles.manager || state._role === roles.admin,
        bunchReady: state => state._bunchReady,
        userBunches: state => state._userBunches,
        userBunchesReady: state => state._userBunchesReady
    },
    actions: {
        loadBunch(context, data) {
            if (!context.state._bunchInitialized) {
                context.commit('setBunchInitialized');
                api.getBunch(data.slug)
                    .then(function (response) {
                        context.commit('setBunchData', response.data);
                    });
            }
        },
        loadUserBunches(context) {
            if (!context.state._userBunchesInitialized) {
                context.commit('setUserBunchesInitialized');
                api.getUserBunches()
                    .then(function (response) {
                        context.commit('setUserBunchesData', response.data);
                    })
                    .catch(function (error) {
                        context.commit('setUserBunchesError');
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
            state._defaultBuyin = bunch.defaultBuyin;
            state._playerId = bunch.player.id;
            state._role = bunch.role;
            state._bunchReady = true;
        },
        setBunchInitialized(state) {
            state._bunchInitialized = true;
        },
        setUserBunchesData(state, bunches) {
            state._userBunches = bunches;
            state._userBunchesReady = true;
        },
        setUserBunchesError(state) {
            state._userBunches = [];
            state._userBunchesReady = true;
        },
        setUserBunchesInitialized(state) {
            state._userBunchesInitialized = true;
        }
    }
};

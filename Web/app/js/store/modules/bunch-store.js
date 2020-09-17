import api from '@/api';
import roles from '@/roles';
import { BunchStoreGetters, BunchStoreActions, BunchStoreMutations } from '@/store/helpers/BunchStoreHelpers';

export default {
    namespaced: false,
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
        [BunchStoreGetters.Slug]: state => state._slug,
        [BunchStoreGetters.Name]: state => state._name,
        [BunchStoreGetters.CurrencyFormat]: state => state._currencyFormat,
        [BunchStoreGetters.ThousandSeparator]: state => state._thousandSeparator,
        [BunchStoreGetters.Description]: state => state._description && state._description.length > 0 ? state._description : null,
        [BunchStoreGetters.HouseRules]: state => state._houseRules && state._houseRules.length > 0 ? state._houseRules : null,
        [BunchStoreGetters.DefaultBuyin]: state => state._defaultBuyin,
        [BunchStoreGetters.PlayerId]: state => state._playerId,
        [BunchStoreGetters.IsManager]: state => state._role === roles.manager || state._role === roles.admin,
        [BunchStoreGetters.BunchReady]: state => state._bunchReady,
        [BunchStoreGetters.UserBunches]: state => state._userBunches,
        [BunchStoreGetters.UserBunchesReady]: state => state._userBunchesReady
    },
    actions: {
        [BunchStoreActions.LoadBunch](context, data) {
            if (!context.state._bunchInitialized) {
                context.commit(BunchStoreMutations.SetBunchInitialized);
                api.getBunch(data.slug)
                    .then(function (response) {
                        context.commit(BunchStoreMutations.SetBunchData, response.data);
                    });
            }
        },
        [BunchStoreActions.LoadUserBunches](context) {
            if (!context.state._userBunchesInitialized) {
                context.commit(BunchStoreMutations.SetUserBunchesInitialized);
                api.getUserBunches()
                    .then(function (response) {
                        context.commit(BunchStoreMutations.SetUserBunchesData, response.data);
                    })
                    .catch(function (error) {
                        context.commit(BunchStoreMutations.SetUserBunchesError);
                    });
            }
        }
    },
    mutations: {
        [BunchStoreMutations.SetBunchData](state, bunch) {
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
        [BunchStoreMutations.SetBunchInitialized](state) {
            state._bunchInitialized = true;
        },
        [BunchStoreMutations.SetUserBunchesData](state, bunches) {
            state._userBunches = bunches;
            state._userBunchesReady = true;
        },
        [BunchStoreMutations.SetUserBunchesError](state) {
            state._userBunches = [];
            state._userBunchesReady = true;
        },
        [BunchStoreMutations.SetUserBunchesInitialized](state) {
            state._userBunchesInitialized = true;
        }
    }
};

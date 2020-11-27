import { StoreOptions } from 'vuex';
import api from '@/api';
import roles from '@/roles';
import { BunchStoreGetters, BunchStoreActions, BunchStoreMutations, BunchStoreState, LoadBunchParams } from '@/store/helpers/BunchStoreHelpers';
import { BunchResponse } from '@/response/BunchResponse';

export default {
    namespaced: false,
    state: {
        _bunch: null,
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
        _userBunches: [],
        _userBunchesReady: false,
        _bunches: [],
        _bunchesReady: false
    },
    getters: {
        [BunchStoreGetters.Bunch]: state => state._bunch,
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
        [BunchStoreGetters.UserBunchesReady]: state => state._userBunchesReady,
        [BunchStoreGetters.Bunches]: state => state._bunches,
        [BunchStoreGetters.BunchesReady]: state => state._bunchesReady
    },
    actions: {
        async [BunchStoreActions.LoadBunch](context, params: LoadBunchParams) {
            if (params.forceLoad || params.slug !== context.state._slug) {
                context.commit(BunchStoreMutations.SetBunchReady, false);
                const response = await api.getBunch(params.slug);
                context.commit(BunchStoreMutations.SetBunchData, response.data);
                context.commit(BunchStoreMutations.SetBunchReady, true);
            }
        },
        async [BunchStoreActions.LoadUserBunches](context) {
            try{
                const response = await api.getUserBunches();
                context.commit(BunchStoreMutations.SetUserBunchesData, response.data);
            } catch {
                context.commit(BunchStoreMutations.SetUserBunchesError);
            }
            context.commit(BunchStoreMutations.SetUserBunchesReady, true);
        },
        async [BunchStoreActions.LoadBunches](context) {
            if (context.state._bunches.length === 0) {
                try{
                    const response = await api.getBunches();
                    context.commit(BunchStoreMutations.SetBunchesData, response.data);
                } catch {
                    context.commit(BunchStoreMutations.SetBunchesError);
                }
                context.commit(BunchStoreMutations.SetBunchesReady, true);
            }
        }
    },
    mutations: {
        [BunchStoreMutations.SetBunchData](state, bunch: BunchResponse) {
            state._bunch = bunch;
            state._slug = bunch.id;
            state._name = bunch.name;
            state._currencyFormat = bunch.currencyFormat;
            state._thousandSeparator = bunch.thousandSeparator;
            state._description = bunch.description;
            state._houseRules = bunch.houseRules;
            state._defaultBuyin = bunch.defaultBuyin;
            state._playerId = bunch.player?.id;
            state._role = bunch.role;
        },
        [BunchStoreMutations.SetBunchReady](state, isReady: boolean) {
            state._bunchReady = isReady;
        },
        [BunchStoreMutations.SetUserBunchesData](state, bunches: BunchResponse[]) {
            state._userBunches = bunches;
        },
        [BunchStoreMutations.SetUserBunchesReady](state, isReady: boolean) {
            state._userBunchesReady = isReady;
        },
        [BunchStoreMutations.SetUserBunchesError](state) {
            state._userBunches = [];
        },
        [BunchStoreMutations.SetBunchesData](state, bunches: BunchResponse[]) {
            state._bunches = bunches;
        },
        [BunchStoreMutations.SetBunchesError](state) {
            state._bunches = [];
        },
        [BunchStoreMutations.SetBunchesReady](state, isReady: boolean) {
            state._bunchesReady = isReady;
        }
    }
} as StoreOptions<BunchStoreState>;

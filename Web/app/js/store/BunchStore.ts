import { StoreOptions } from 'vuex';
import roles from '@/roles';
import { BunchStoreMutations, BunchStoreState } from '@/store/helpers/BunchStoreHelpers';
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
    _bunchesReady: false,
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
    },
  },
} as StoreOptions<BunchStoreState>;

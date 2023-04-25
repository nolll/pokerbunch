import { StoreOptions } from 'vuex';
import { RootStoreState } from './helpers/RootStoreHelpers';
import BunchStore from './BunchStore';
import UserStore from './UserStore';

export default {
  strict: true,
  modules: {
    bunch: BunchStore,
    user: UserStore,
  },
} as StoreOptions<RootStoreState>;

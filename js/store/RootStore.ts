import { StoreOptions } from 'vuex';
import { RootStoreState } from './helpers/RootStoreHelpers';
import CurrentGameStore from './CurrentGameStore';
import PlayerStore from './PlayerStore';

export default {
  strict: true,
  modules: {
    currentGame: CurrentGameStore,
    player: PlayerStore,
  },
} as StoreOptions<RootStoreState>;

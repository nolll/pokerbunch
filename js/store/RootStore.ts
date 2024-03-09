import { StoreOptions } from 'vuex';
import { RootStoreState } from './helpers/RootStoreHelpers';
import BunchStore from './BunchStore';
import CurrentGameStore from './CurrentGameStore';
import PlayerStore from './PlayerStore';

export default {
  strict: true,
  modules: {
    bunch: BunchStore,
    currentGame: CurrentGameStore,
    player: PlayerStore,
  },
} as StoreOptions<RootStoreState>;

import { StoreOptions } from 'vuex';
import { RootStoreState } from './helpers/RootStoreHelpers';
import BunchStore from './BunchStore';
import CurrentGameStore from './CurrentGameStore';
import EventStore from './EventStore';
import GameArchiveStore from './GameArchiveStore';
import PlayerStore from './PlayerStore';
import UserStore from './UserStore';

export default {
  strict: true,
  modules: {
    bunch: BunchStore,
    currentGame: CurrentGameStore,
    event: EventStore,
    gameArchive: GameArchiveStore,
    player: PlayerStore,
    user: UserStore,
  },
} as StoreOptions<RootStoreState>;

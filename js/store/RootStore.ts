import { StoreOptions } from 'vuex';
import { RootStoreState } from './helpers/RootStoreHelpers';
import BunchStore from './BunchStore';
import CurrentGameStore from './CurrentGameStore';
import EventStore from './EventStore';
import GameArchiveStore from './GameArchiveStore';
import LocationStore from './LocationStore';
import PlayerStore from './PlayerStore';

export default {
  strict: true,
  modules: {
    bunch: BunchStore,
    currentGame: CurrentGameStore,
    event: EventStore,
    location: LocationStore,
    gameArchive: GameArchiveStore,
    player: PlayerStore,
  },
} as StoreOptions<RootStoreState>;

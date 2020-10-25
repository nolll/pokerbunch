import { StoreOptions } from 'vuex';
import { RootStoreState } from './helpers/RootStoreHelpers';
import BunchStore from './BunchStore';
import CashgameStore from './CashgameStore';
import CurrentGameStore from './CurrentGameStore';
import EventStore from './EventStore';
import GameArchiveStore from './GameArchiveStore';
import LocationStore from './LocationStore';
import PlayerStore from './PlayerStore';
import UserStore from './UserStore';
import TimezoneStore from './TimezoneStore';

export default {
    strict: true,
    modules: {
        bunch: BunchStore,
        cashgame: CashgameStore,
        currentGame: CurrentGameStore,
        event: EventStore,
        location: LocationStore,
        gameArchive: GameArchiveStore,
        player: PlayerStore,
        timezone: TimezoneStore,
        user: UserStore
    }
} as StoreOptions<RootStoreState>;

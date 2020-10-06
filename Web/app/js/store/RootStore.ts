import { StoreOptions } from 'vuex';
import { RootStoreState } from './helpers/RootStoreHelpers';
import { default as BunchStore } from './BunchStore';
import { default as CashgameStore } from './CashgameStore';
import { default as CurrentGameStore } from './CurrentGameStore';
import { default as EventStore } from './EventStore';
import { default as GameArchiveStore } from './GameArchiveStore';
import { default as LocationStore } from './LocationStore';
import { default as PlayerStore } from './PlayerStore';
import { default as UserStore } from './UserStore';

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
        user: UserStore
    }
} as StoreOptions<RootStoreState>;

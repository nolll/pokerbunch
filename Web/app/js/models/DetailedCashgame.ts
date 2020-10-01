import { DetailedCashgameBunch } from './DetailedCashgameBunch';
import { DetailedCashgameEvent } from './DetailedCashgameEvent';
import { DetailedCashgameLocation } from './DetailedCashgameLocation';
import { DetailedCashgamePlayer } from './DetailedCashgamePlayer';

export interface DetailedCashgame {
    isRunning: boolean;
    id: string;
    bunch: DetailedCashgameBunch;
    location: DetailedCashgameLocation;
    startTime: Date;
    updatedTime: Date;
    players: DetailedCashgamePlayer[];
    event: DetailedCashgameEvent | null;
}

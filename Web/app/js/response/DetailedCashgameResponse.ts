import { DetailedCashgameResponseBunch } from './DetailedCashgameResponseBunch';
import { DetailedCashgameResponseEvent } from './DetailedCashgameResponseEvent';
import { DetailedCashgameResponseLocation } from './DetailedCashgameResponseLocation';
import { DetailedCashgameResponsePlayer } from './DetailedCashgameResponsePlayer';

export interface DetailedCashgameResponse {
    isRunning: boolean;
    id: string;
    bunch: DetailedCashgameResponseBunch;
    location: DetailedCashgameResponseLocation;
    startTime: string;
    updatedTime: string;
    players: DetailedCashgameResponsePlayer[];
    event: DetailedCashgameResponseEvent | null;
}

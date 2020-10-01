import { DetailedCashgameAction } from './DetailedCashgameAction';

export interface DetailedCashgamePlayer {
    id: string;
    name: string;
    color: string;
    startTime: Date | null;
    updatedTime: Date | null;
    buyin: number | null;
    stack: number | null;
    actions: DetailedCashgameAction[];
}

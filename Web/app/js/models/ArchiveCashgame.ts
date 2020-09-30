import { ArchiveCashgameLocationResponse } from '@/response/ArchiveCashgameLocationResponse';

import { ArchiveCashgamePlayerResponse } from '@/response/ArchivePlayerResponse';

export interface ArchiveCashgame {
    id: string;
    startTime: Date;
    updatedTime: Date;
    date: Date;
    turnover: number;
    averageBuyin: number;
    playerCount: number;
    duration: number;
    location: ArchiveCashgameLocationResponse;
    players: ArchiveCashgamePlayerResponse[];
}

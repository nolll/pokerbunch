import { ArchiveCashgameLocationResponse } from './ArchiveCashgameLocationResponse';
import { ArchiveCashgamePlayerResponse } from './ArchiveCashgamePlayerResponse';

export interface ArchiveCashgameResponse {
    id: string;
    startTime: Date;
    updatedTime: Date;
    location: ArchiveCashgameLocationResponse;
    players: ArchiveCashgamePlayerResponse[];
}

import { ArchiveCashgameLocationResponse } from './ArchiveCashgameLocationResponse';
import { ArchiveCashgamePlayerResponse } from './ArchivePlayerResponse';

export interface ArchiveCashgameResponse {
    id: string;
    startTime: Date;
    updatedTime: Date;
    location: ArchiveCashgameLocationResponse;
    players: ArchiveCashgamePlayerResponse[];
}

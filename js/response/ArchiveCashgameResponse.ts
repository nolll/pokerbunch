import { ArchiveCashgameLocationResponse } from './ArchiveCashgameLocationResponse';
import { ArchiveCashgameResultResponse } from './ArchiveCashgameResultResponse';

export interface ArchiveCashgameResponse {
    id: string;
    startTime: Date;
    updatedTime: Date;
    location: ArchiveCashgameLocationResponse;
    results: ArchiveCashgameResultResponse[];
}

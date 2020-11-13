import { DetailedCashgameResponseAction } from '@/response/DetailedCashgameResponseAction';
import { DetailedCashgameResponseActionType } from '@/response/DetailedCashgameResponseActionType';
import dayjs from 'dayjs';

export class DetailedCashgameAction {
    id: string | null;
    type: DetailedCashgameResponseActionType;
    time: Date;
    stack: number;
    added: number | null;

    constructor(response: DetailedCashgameResponseAction) {
        this.id = response.id;
        this.type = response.type;
        this.time = dayjs(response.time).toDate();
        this.stack = response.stack;
        this.added = response.added;
    }
}

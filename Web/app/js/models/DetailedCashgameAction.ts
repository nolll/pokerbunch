import { DetailedCashgameResponseActionType } from '@/response/DetailedCashgameResponseActionType';

export interface DetailedCashgameAction {
    id: string | null;
    type: DetailedCashgameResponseActionType;
    time: Date;
    stack: number;
    added: number | null;
}

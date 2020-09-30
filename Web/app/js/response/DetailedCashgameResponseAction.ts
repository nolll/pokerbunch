import { DetailedCashgameResponseActionType } from './DetailedCashgameResponseActionType';

export interface DetailedCashgameResponseAction {
    id: string | null;
    type: DetailedCashgameResponseActionType;
    time: Date;
    stack: number;
    added: number | null;
}

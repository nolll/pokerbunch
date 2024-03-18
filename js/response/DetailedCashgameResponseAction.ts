import { DetailedCashgameResponseActionType } from './DetailedCashgameResponseActionType';

export interface DetailedCashgameResponseAction {
  id: string;
  type: DetailedCashgameResponseActionType;
  time: Date;
  stack: number;
  added: number | null;
}

import { DetailedCashgameResponseActionType } from './DetailedCashgameResponseActionType';

export interface DetailedCashgameResponseAction {
  id: string | undefined;
  type: DetailedCashgameResponseActionType;
  time: Date;
  stack: number;
  added: number | null;
}

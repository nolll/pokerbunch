import { DetailedCashgameResponseAction } from './DetailedCashgameResponseAction';

export interface DetailedCashgameResponsePlayer {
  id: string;
  name: string;
  color: string;
  startTime: Date | null;
  updatedTime: Date | null;
  buyin: number | null;
  stack: number | null;
  actions: DetailedCashgameResponseAction[];
}

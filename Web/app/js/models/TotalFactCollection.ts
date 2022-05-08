import { TotalTimeFact } from './TotalTimeFact';
import { TotalAmountFact } from './TotalAmountFact';

export interface TotalFactCollection {
  mostTime: TotalTimeFact;
  bestTotal: TotalAmountFact;
  worstTotal: TotalAmountFact;
  biggestBuyin: TotalAmountFact;
  biggestCashout: TotalAmountFact;
}

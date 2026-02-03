import { CashgamePlayerYearlyResult } from './CashgamePlayerYearlyResult';

export interface CashgamePlayerYearlyResultCollection{
    rank: number;
    id: string;
    name: string;
    winnings: number;
    years: CashgamePlayerYearlyResult[];
}
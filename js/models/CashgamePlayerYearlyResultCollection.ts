import { CashgamePlayerYearlyResult } from './CashgamePlayerYearlyResult';

export interface CashgamePlayerYearlyResultCollection{
    id: string;
    name: string;
    winnings: number;
    years: CashgamePlayerYearlyResult[];
}
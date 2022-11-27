import { CashgamePlayerData } from './CashgamePlayerData';

export interface CashgameListPlayerData {
    id: string;
    name: string;
    winnings: number;
    winrate: number;
    buyin: number;
    stack: number;
    gameResults: CashgamePlayerData[];
    gameCount: number;
    rank: number;
    playedTimeInMinutes: number;
}

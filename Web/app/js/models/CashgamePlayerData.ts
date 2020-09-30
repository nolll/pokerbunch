export interface CashgamePlayerData {
    gameId: string;
    buyin: number;
    stack: number;
    winnings: number;
    buyinTime: Date | null;
    updatedTime: Date | null;
    playedTimeInMinutes: number;
    isWinner: boolean;
    playedThisGame: boolean;
}

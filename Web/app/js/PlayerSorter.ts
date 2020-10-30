import comparer from './comparer';
import { CashgameListPlayerData } from './models/CashgameListPlayerData';
import { CashgamePlayerSortOrder } from './models/CashgamePlayerSortOrder';

export default {
    sort(players: CashgameListPlayerData[], sortOrder?: CashgamePlayerSortOrder) {
        return players.slice().sort(getPlayersCompareFunc(sortOrder)).reverse();
    }
};

function getPlayersCompareFunc(sortOrder?: CashgamePlayerSortOrder) {
    if (sortOrder === CashgamePlayerSortOrder.Buyin)
        return compareBuyin;
    if (sortOrder === CashgamePlayerSortOrder.Stack)
        return compareStack;
    if (sortOrder === CashgamePlayerSortOrder.Time)
        return compareTime;
    if (sortOrder === CashgamePlayerSortOrder.GameCount)
        return compareGameCount;
    if (sortOrder === CashgamePlayerSortOrder.Winrate)
        return compareWinrate;
    return compareWinnings;
}

function compareWinnings(a: CashgameListPlayerData, b: CashgameListPlayerData) {
    return comparer.compare(a.winnings, b.winnings);
}

function compareBuyin(a: CashgameListPlayerData, b: CashgameListPlayerData) {
    return comparer.compare(a.buyin, b.buyin);
}

function compareStack(a: CashgameListPlayerData, b: CashgameListPlayerData) {
    return comparer.compare(a.stack, b.stack);
}

function compareTime(a: CashgameListPlayerData, b: CashgameListPlayerData) {
    return comparer.compare(a.playedTimeInMinutes, b.playedTimeInMinutes);
}

function compareGameCount(a: CashgameListPlayerData, b: CashgameListPlayerData) {
    return comparer.compare(a.gameCount, b.gameCount);
}

function compareWinrate(a: CashgameListPlayerData, b: CashgameListPlayerData) {
    return comparer.compare(a.winrate, b.winrate);
}

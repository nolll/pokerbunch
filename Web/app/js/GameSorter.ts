import comparer from './comparer';
import { ArchiveCashgame } from './models/Cashgame';
import { CashgameSortOrder } from './models/CashgameSortOrder';

export default {
    sort(games: ArchiveCashgame[], sortOrder: CashgameSortOrder) {
        return games.slice().sort(getGamesCompareFunc(sortOrder)).reverse();
    }
};

function getGamesCompareFunc(sortOrder: CashgameSortOrder) {
    if (sortOrder === CashgameSortOrder.PlayerCount)
        return comparePlayerCount;
    if (sortOrder === CashgameSortOrder.Duration)
        return compareDuration;
    if (sortOrder === CashgameSortOrder.Turnover)
        return compareTurnover;
    if (sortOrder === CashgameSortOrder.AverageBuyin)
        return compareAverageBuyin;
    return compareDate;
}

function compareDate(a: ArchiveCashgame, b: ArchiveCashgame) {
    return comparer.compare(a.date, b.date);
}

function comparePlayerCount(a: ArchiveCashgame, b: ArchiveCashgame) {
    return comparer.compare(a.playerCount, b.playerCount);
}

function compareDuration(a: ArchiveCashgame, b: ArchiveCashgame) {
    return comparer.compare(a.duration, b.duration);
}

function compareTurnover(a: ArchiveCashgame, b: ArchiveCashgame) {
    return comparer.compare(a.turnover, b.turnover);
}

function compareAverageBuyin(a: ArchiveCashgame, b: ArchiveCashgame) {
    return comparer.compare(a.averageBuyin, b.averageBuyin);
}

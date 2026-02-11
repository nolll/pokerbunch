import comparer from './comparer';
import { ArchiveCashgame } from './models/ArchiveCashgame';
import { CashgameSortOrder } from './models/CashgameSortOrder';

export default {
  sort(games: ArchiveCashgame[], sortOrder: CashgameSortOrder) {
    return games.slice().sort(getGamesCompareFunc(sortOrder)).reverse();
  },
};

const getGamesCompareFunc = (sortOrder: CashgameSortOrder) => {
  if (sortOrder === CashgameSortOrder.PlayerCount) return comparePlayerCount;
  if (sortOrder === CashgameSortOrder.Duration) return compareDuration;
  if (sortOrder === CashgameSortOrder.Turnover) return compareTurnover;
  if (sortOrder === CashgameSortOrder.AverageBuyin) return compareAverageBuyin;
  return compareDate;
};

const compareDate = (a: ArchiveCashgame, b: ArchiveCashgame) => comparer.compare(a.date, b.date);
const comparePlayerCount = (a: ArchiveCashgame, b: ArchiveCashgame) => comparer.compare(a.playerCount, b.playerCount);
const compareDuration = (a: ArchiveCashgame, b: ArchiveCashgame) => comparer.compare(a.duration, b.duration);
const compareTurnover = (a: ArchiveCashgame, b: ArchiveCashgame) => comparer.compare(a.turnover, b.turnover);
const compareAverageBuyin = (a: ArchiveCashgame, b: ArchiveCashgame) => comparer.compare(a.averageBuyin, b.averageBuyin);

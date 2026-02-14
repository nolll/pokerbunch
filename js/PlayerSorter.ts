import comparer from './comparer';
import { CashgameListPlayerData } from './models/CashgameListPlayerData';
import { CashgamePlayerSortOrder } from './models/CashgamePlayerSortOrder';

export default {
  sort: (players: CashgameListPlayerData[], sortOrder?: CashgamePlayerSortOrder) =>
    players.slice().sort(getPlayersCompareFunc(sortOrder)).reverse(),
};

const getPlayersCompareFunc = (sortOrder?: CashgamePlayerSortOrder) => {
  if (sortOrder === CashgamePlayerSortOrder.Buyin) return compareBuyin;
  if (sortOrder === CashgamePlayerSortOrder.Stack) return compareStack;
  if (sortOrder === CashgamePlayerSortOrder.Time) return compareTime;
  if (sortOrder === CashgamePlayerSortOrder.GameCount) return compareGameCount;
  if (sortOrder === CashgamePlayerSortOrder.Winrate) return compareWinrate;
  return compareWinnings;
};

const compareWinnings = (a: CashgameListPlayerData, b: CashgameListPlayerData) => comparer.compare(a.winnings, b.winnings);
const compareBuyin = (a: CashgameListPlayerData, b: CashgameListPlayerData) => comparer.compare(a.buyin, b.buyin);
const compareStack = (a: CashgameListPlayerData, b: CashgameListPlayerData) => comparer.compare(a.stack, b.stack);
const compareTime = (a: CashgameListPlayerData, b: CashgameListPlayerData) =>
  comparer.compare(a.playedTimeInMinutes, b.playedTimeInMinutes);
const compareGameCount = (a: CashgameListPlayerData, b: CashgameListPlayerData) => comparer.compare(a.gameCount, b.gameCount);
const compareWinrate = (a: CashgameListPlayerData, b: CashgameListPlayerData) => comparer.compare(a.winrate, b.winrate);

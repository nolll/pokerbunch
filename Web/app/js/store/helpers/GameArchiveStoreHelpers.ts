import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import { CashgamePlayerSortOrder } from '@/models/CashgamePlayerSortOrder';
import { CashgameSortOrder } from '@/models/CashgameSortOrder';

export enum GameArchiveStoreMutations {
  SetData = 'gameArchive_setData',
  SetGameSortorder = 'gameArchive_setGameSortorder',
  SetPlayerSortorder = 'gameArchive_setPlayerSortorder',
  SetSlug = 'gameArchive_setSlug',
  SetSelectedYear = 'gameArchive_setSelectedYear',
}

export interface GameArchiveStoreState {
  _gameSortOrder: CashgameSortOrder;
  _games: ArchiveCashgame[];
  _playerSortOrder: CashgamePlayerSortOrder;
  _slug: string;
  _selectedYear: number | undefined;
  _ready: boolean;
}

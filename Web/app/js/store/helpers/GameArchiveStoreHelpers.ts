import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import { CashgamePlayerSortOrder } from '@/models/CashgamePlayerSortOrder';
import { CashgameSortOrder } from '@/models/CashgameSortOrder';

export enum GameArchiveStoreGetters {
  GameSortOrder = 'gameArchive_gameSortOrder',
  Games = 'gameArchive_games',
  PlayerSortOrder = 'gameArchive_playerSortOrder',
  SelectedYear = 'gameArchive_selectedYear',
  SortedGames = 'gameArchive_sortedGames',
  SortedPlayers = 'gameArchive_sortedPlayers',
  CurrentYearGames = 'gameArchive_currentYearGames',
  CurrentYearPlayers = 'gameArchive_currentYearPlayers',
  AllYearsPlayers = 'gameArchive_allYearsPlayers',
  Years = 'gameArchive_years',
  CurrentYear = 'gameArchive_currentYear',
  GamesReady = 'gameArchive_gamesReady',
  HasGames = 'gameArchive_hasGames',
}

export enum GameArchiveStoreActions {
  LoadGames = 'gameArchive_loadGames',
  SelectYear = 'gameArchive_selectYear',
  SortGames = 'gameArchive_sortGames',
  SortPlayers = 'gameArchive_sortPlayers',
}

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

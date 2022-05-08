import { CurrentGameResponse } from '@/response/CurrentGameResponse';

export enum CurrentGameStoreMutations {
  LoadingComplete = 'currentGame_loadingComplete',
  DataLoaded = 'currentGame_dataLoaded',
}

export interface CurrentGameStoreState {
  _currentGames: CurrentGameResponse[];
  _currentGamesReady: boolean;
}

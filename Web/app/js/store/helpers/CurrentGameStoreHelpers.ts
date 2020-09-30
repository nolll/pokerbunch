import { CurrentGameResponse } from '@/response/CurrentGameResponse';

export enum CurrentGameStoreGetters {
    CurrentGamesReady = 'currentGame_currentGamesReady',
    CurrentGames = 'currentGame_currentGames'
}

export enum CurrentGameStoreActions {
    LoadCurrentGames = 'currentGame_loadCurrentGames'
}

export enum CurrentGameStoreMutations {
    LoadingComplete = 'currentGame_loadingComplete',
    DataLoaded = 'currentGame_dataLoaded'
}

export interface CurrentGameStoreState {
    _currentGames: CurrentGameResponse[];
    _currentGamesReady: boolean;
}
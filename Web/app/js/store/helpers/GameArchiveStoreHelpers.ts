export enum GameArchiveStoreGetters {
    GameSortOrder = 'gameArchive_gameSortOrder',
    Games = 'gameArchive_games',
    PlayerSortOrder = 'gameArchive_playerSortOrder',
    SelectedYear = 'gameArchive_selectedYear',
    IsPageNavExpanded = 'gameArchive_isPageNavExpanded',
    IsYearNavExpanded = 'gameArchive_isYearNavExpanded',
    SortedGames = 'gameArchive_sortedGames',
    SortedPlayers = 'gameArchive_sortedPlayers',
    CurrentYearGames = 'gameArchive_currentYearGames',
    CurrentYearPlayers = 'gameArchive_currentYearPlayers',
    AllYearsPlayers = 'gameArchive_allYearsPlayers',
    Years = 'gameArchive_years',
    CurrentYear = 'gameArchive_currentYear',
    GamesReady = 'gameArchive_gamesReady',
    HasGames = 'gameArchive_hasGames'
}

export enum GameArchiveStoreActions {
    LoadGames = 'gameArchive_loadGames',
    SelectYear = 'gameArchive_selectYear',
    SortGames = 'gameArchive_sortGames',
    SortPlayers = 'gameArchive_sortPlayers',
    TogglePageNav = 'gameArchive_togglePageNav',
    ToggleYearNav = 'gameArchive_toggleYearNav'
}

export enum GameArchiveStoreMutations {
    SetData = 'gameArchive_setData',
    SetGameSortorder = 'gameArchive_setGameSortorder',
    SetPlayerSortorder = 'gameArchive_setPlayerSortorder',
    SetInitialized = 'gameArchive_setInitialized',
    SetSelectedYear = 'gameArchive_setSelectedYear',
    TogglePageNav = 'gameArchive_togglePageNav',
    ToggleYearNav = 'gameArchive_toggleYearNav'
}

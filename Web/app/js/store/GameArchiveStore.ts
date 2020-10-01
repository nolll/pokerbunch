import { StoreOptions } from 'vuex';
import api from '@/api';
import gameSorter from '@/GameSorter';
import playerSorter from '@/PlayerSorter';
import timeFunctions from '@/time-functions';
import dayjs from 'dayjs';
import { GameArchiveStoreGetters, GameArchiveStoreActions, GameArchiveStoreMutations, GameArchiveStoreState } from '@/store/helpers/GameArchiveStoreHelpers';
import { CashgameSortOrder } from '@/models/CashgameSortOrder';
import { CashgamePlayerSortOrder } from '@/models/CashgamePlayerSortOrder';
import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import { CashgamePlayerData } from '@/models/CashgamePlayerData';
import { ArchiveCashgameResponse } from '@/response/ArchiveCashgameResponse';
import { ArchiveCashgamePlayerResponse } from '@/response/ArchivePlayerResponse';

export default {
    namespaced: false,
    state: {
        _gameSortOrder: CashgameSortOrder.Date,
        _games: [],
        _playerSortOrder: CashgamePlayerSortOrder.Winnings,
        _initialized: false,
        _selectedYear: null,
        _isPageNavExpanded: false,
        _isYearNavExpanded: false,
        _ready: false
    },
    getters: {
        [GameArchiveStoreGetters.GameSortOrder]: state => state._gameSortOrder,
        [GameArchiveStoreGetters.Games]: state => state._games,
        [GameArchiveStoreGetters.PlayerSortOrder]: state => state._playerSortOrder,
        [GameArchiveStoreGetters.SelectedYear]: state => state._selectedYear,
        [GameArchiveStoreGetters.IsPageNavExpanded]: state => state._isPageNavExpanded,
        [GameArchiveStoreGetters.IsYearNavExpanded]: state => state._isYearNavExpanded,
        [GameArchiveStoreGetters.SortedGames]: state => {
            const selectedGames = getSelectedGames(state._games, state._selectedYear);
            return gameSorter.sort(selectedGames, state._gameSortOrder);
        },
        [GameArchiveStoreGetters.SortedPlayers]: (state, getters) => {
            return playerSorter.sort(getPlayers(getters[GameArchiveStoreGetters.SortedGames]), state._playerSortOrder);
        },
        [GameArchiveStoreGetters.CurrentYearGames]: (state, getters) => {
            const selectedGames = getSelectedGames(state._games, getters[GameArchiveStoreGetters.CurrentYear]);
            return gameSorter.sort(selectedGames, CashgameSortOrder.Date);
        },
        [GameArchiveStoreGetters.CurrentYearPlayers]: (state, getters) => {
            return playerSorter.sort(getPlayers(getters[GameArchiveStoreGetters.CurrentYearGames]), CashgamePlayerSortOrder.Winnings);
        },
        [GameArchiveStoreGetters.AllYearsPlayers]: state => {
            return playerSorter.sort(getPlayers(state._games), CashgamePlayerSortOrder.Winnings);
        },
        [GameArchiveStoreGetters.Years]: state => {
            return getYears(state._games);
        },
        [GameArchiveStoreGetters.CurrentYear]: state => {
            if (state._games.length > 0) {
                const latestGame = state._games[0];
                return dayjs(latestGame.startTime).year();
            }
            return null;
        },
        [GameArchiveStoreGetters.GamesReady]: state => state._ready,
        [GameArchiveStoreGetters.HasGames]: state => {
            return state._games.length > 0;
        }
    },
    actions: {
        async [GameArchiveStoreActions.LoadGames](context, data) {
            if (!context.state._initialized) {
                context.commit(GameArchiveStoreMutations.SetInitialized);
                const response = await api.getGames(data.slug);
                context.commit(GameArchiveStoreMutations.SetData, response.data);
            }
        },
        [GameArchiveStoreActions.SelectYear](context, data) {
            context.commit(GameArchiveStoreMutations.SetSelectedYear, data.year);
        },
        [GameArchiveStoreActions.SortGames](context, sortOrder) {
            context.commit(GameArchiveStoreMutations.SetGameSortorder, sortOrder);
        },
        [GameArchiveStoreActions.SortPlayers](context, sortOrder) {
            context.commit(GameArchiveStoreMutations.SetPlayerSortorder, sortOrder);
        },
        [GameArchiveStoreActions.TogglePageNav](context) {
            context.commit(GameArchiveStoreMutations.SetPageNavExpanded, !context.state._isPageNavExpanded);
            context.commit(GameArchiveStoreMutations.SetYearNavExpanded, false);
        },
        [GameArchiveStoreActions.ToggleYearNav](context) {
            context.commit(GameArchiveStoreMutations.SetYearNavExpanded, !context.state._isYearNavExpanded);
            context.commit(GameArchiveStoreMutations.SetPageNavExpanded, false);
        },
        [GameArchiveStoreActions.ClosePageNav](context) {
            context.commit(GameArchiveStoreMutations.SetPageNavExpanded, false);
        },
        [GameArchiveStoreActions.CloseYearNav](context) {
            context.commit(GameArchiveStoreMutations.SetYearNavExpanded, false);
        }
    },
    mutations: {
        [GameArchiveStoreMutations.SetData](state, games) {
            state._games = buildGames(games);
            state._ready = true;
        },
        [GameArchiveStoreMutations.SetGameSortorder](state, sortOrder) {
            state._gameSortOrder = sortOrder;
        },
        [GameArchiveStoreMutations.SetPlayerSortorder](state, sortOrder) {
            state._playerSortOrder = sortOrder;
        },
        [GameArchiveStoreMutations.SetInitialized](state) {
            state._initialized = true;
        },
        [GameArchiveStoreMutations.SetSelectedYear](state, year) {
            state._selectedYear = year ? year : null;
        },
        [GameArchiveStoreMutations.SetPageNavExpanded](state, isExpanded) {
            state._isPageNavExpanded = isExpanded;
        },
        [GameArchiveStoreMutations.SetYearNavExpanded](state, isExpanded) {
            state._isYearNavExpanded = isExpanded;
        }
    }
} as StoreOptions<GameArchiveStoreState>;

function buildGames(rawGames: ArchiveCashgameResponse[]): ArchiveCashgame[] {
    const responseGames: ArchiveCashgame[] = [];
    for (let gi = 0; gi < rawGames.length; gi++) {
        const rawGame = rawGames[gi];
        const game: ArchiveCashgame = {
            id: rawGame.id,
            startTime: rawGame.startTime,
            updatedTime: rawGame.updatedTime,
            location: rawGame.location,
            players: rawGame.players,
            date: rawGame.startTime,
            turnover: getTurnover(rawGame),
            averageBuyin: getAverageBuyin(rawGame),
            playerCount: rawGame.players.length,
            duration: timeFunctions.diffInMinutes(rawGame.startTime, rawGame.updatedTime)
        };
        responseGames.push(game);
    }
    return responseGames;
}

function getSelectedGames(games: ArchiveCashgame[], selectedYear?: number | null) {
    if (!selectedYear)
        return games;
    const selectedGames = [];
    for (let gi = 0; gi < games.length; gi++) {
        const game = games[gi];
        const year = dayjs(game.startTime).year();
        if (year === selectedYear) {
            selectedGames.push(game);
        }
    }
    return selectedGames;
}

function getYears(games: ArchiveCashgame[]) {
    const years: number[] = [];
    for (let gi = 0; gi < games.length; gi++) {
        const game = games[gi];
        const year = dayjs(game.startTime).year();
        if (!years.includes(year)) {
            years.push(year);
        }
    }
    return years;
}

function getAverageBuyin(game: ArchiveCashgameResponse) {
    const sum = getTurnover(game);
    const playerCount = game.players.length;
    return Math.round(sum / playerCount);
}

function getTurnover(game: ArchiveCashgameResponse) {
    let sum = 0;
    for (let pi = 0; pi < game.players.length; pi++) {
        sum += game.players[pi].buyin;
    }
    return sum;
}

function getPlayers(games: ArchiveCashgame[]) {
    var resultsList = [];
    for (let gi = 0; gi < games.length; gi++) {
        const game = games[gi];
        const winningPlayerId = getWinningPlayerId(game.players);
        for (let pi = 0; pi < game.players.length; pi++) {
            const singlePlayerResult = game.players[pi];
            const isWinner = singlePlayerResult.id === winningPlayerId;
            let playerResult = getPlayerResult(resultsList, singlePlayerResult.id);
            if (!playerResult) {
                playerResult = {
                    id: singlePlayerResult.id,
                    name: singlePlayerResult.name,
                    winnings: 0,
                    winrate: 0,
                    buyin: 0,
                    stack: 0,
                    gameResults: [],
                    gameCount: 0,
                    rank: 0,
                    playedTimeInMinutes: 0
                };
                resultsList.push(playerResult);
            }
            const buyinTime = singlePlayerResult.startTime;
            const updatedTime = singlePlayerResult.updatedTime;
            const timeDiffInMinutes = timeFunctions.diffInMinutes(buyinTime, updatedTime);
            const playerGameResult: CashgamePlayerData = {
                gameId: game.id,
                buyin: singlePlayerResult.buyin,
                stack: singlePlayerResult.stack,
                winnings: singlePlayerResult.stack - singlePlayerResult.buyin,
                buyinTime,
                updatedTime,
                playedTimeInMinutes: timeDiffInMinutes,
                isWinner,
                playedThisGame: true
            };
            playerResult.winnings += playerGameResult.winnings;
            playerResult.buyin += playerGameResult.buyin;
            playerResult.stack += playerGameResult.stack;
            playerResult.gameResults.push(playerGameResult);
            playerResult.gameCount++;
            playerResult.playedTimeInMinutes += playerGameResult.playedTimeInMinutes;
        }
    }
    const sortedPlayers = playerSorter.sort(resultsList, CashgamePlayerSortOrder.Winnings);
    for (let spi = 0; spi < sortedPlayers.length; spi++) {
        const p = sortedPlayers[spi];
        p.rank = spi + 1;
        p.winrate = getWinrate(p.winnings, p.playedTimeInMinutes);
    }
    return fillEmptyGames(sortedPlayers, games);
}

function getWinrate(winnings: number, timeInMinutes: number) {
    if (timeInMinutes === 0)
        return 0;
    const perMinute = winnings / timeInMinutes;
    return Math.round(perMinute * 60);
}

function getPlayerResult(results: CashgameListPlayerData[], id: string): CashgameListPlayerData | null {
    for (var i = 0; i < results.length; i++) {
        var playerResult = results[i];
        if (playerResult && playerResult.id === id)
            return playerResult;
    }
    return null;
}

function getWinningPlayerId(players: ArchiveCashgamePlayerResponse[]) {
    let winningPlayerId = '';
    let best: number | null = null;
    for (var i = 0; i < players.length; i++) {
        const player = players[i];
        const winnings = player.stack - player.buyin;
        if (best === null || winnings > best) {
            winningPlayerId = player.id;
            best = winnings;
        }
    }
    return winningPlayerId;
}

function fillEmptyGames(resultsList: CashgameListPlayerData[], games: ArchiveCashgame[]) {
    for (var pi = 0; pi < resultsList.length; pi++) {
        const player = resultsList[pi];
        const playerGameResults = [];
        for (var gi = 0; gi < games.length; gi++) {
            const result = getPlayerGame(player.gameResults, games[gi].id);
            playerGameResults.push(result);
        }
        player.gameResults = playerGameResults;
    }
    return resultsList;
}

function getPlayerGame(playerGames: CashgamePlayerData[], gameId: string): CashgamePlayerData {
    for (var i = 0; i < playerGames.length; i++) {
        const playerGame = playerGames[i];
        if (playerGame.gameId === gameId)
            return playerGame;
    }
    return {
        gameId,
        buyin: 0,
        stack: 0,
        winnings: 0,
        buyinTime: null,
        updatedTime: null,
        playedTimeInMinutes: 0,
        isWinner: false,
        playedThisGame: false
    };
}

import { StoreOptions } from 'vuex';
import api from '@/api';
import gameSorter from '@/GameSorter';
import playerSorter from '@/PlayerSorter';
import archiveHelper from '@/ArchiveHelper';
import timeFunctions from '@/time-functions';
import dayjs from 'dayjs';
import { GameArchiveStoreGetters, GameArchiveStoreActions, GameArchiveStoreMutations, GameArchiveStoreState } from '@/store/helpers/GameArchiveStoreHelpers';
import { CashgameSortOrder } from '@/models/CashgameSortOrder';
import { CashgamePlayerSortOrder } from '@/models/CashgamePlayerSortOrder';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import { ArchiveCashgameResponse } from '@/response/ArchiveCashgameResponse';

export default {
    namespaced: false,
    state: {
        _gameSortOrder: CashgameSortOrder.Date,
        _games: [],
        _playerSortOrder: CashgamePlayerSortOrder.Winnings,
        _slug: '',
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
            return playerSorter.sort(archiveHelper.getPlayers(getters[GameArchiveStoreGetters.SortedGames]), state._playerSortOrder);
        },
        [GameArchiveStoreGetters.CurrentYearGames]: (state, getters) => {
            const selectedGames = getSelectedGames(state._games, getters[GameArchiveStoreGetters.CurrentYear]);
            return gameSorter.sort(selectedGames, CashgameSortOrder.Date);
        },
        [GameArchiveStoreGetters.CurrentYearPlayers]: (state, getters) => {
            return playerSorter.sort(archiveHelper.getPlayers(getters[GameArchiveStoreGetters.CurrentYearGames]), CashgamePlayerSortOrder.Winnings);
        },
        [GameArchiveStoreGetters.AllYearsPlayers]: state => {
            return playerSorter.sort(archiveHelper.getPlayers(state._games), CashgamePlayerSortOrder.Winnings);
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
            if (data.slug !== context.state._slug) {
                context.commit(GameArchiveStoreMutations.SetSlug, data.slug);
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
        [GameArchiveStoreMutations.SetSlug](state, slug: string) {
            state._slug = slug;
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
    for (const rawGame of rawGames) {
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
    for (const game of games) {
        const year = dayjs(game.startTime).year();
        if (year === selectedYear) {
            selectedGames.push(game);
        }
    }
    return selectedGames;
}

function getYears(games: ArchiveCashgame[]) {
    const years: number[] = [];
    for (const game of games) {
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
    for (const p of game.players) {
        sum += p.buyin;
    }
    return sum;
}

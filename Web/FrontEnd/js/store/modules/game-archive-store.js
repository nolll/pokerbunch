import api from '@/api';
import gameSorter from '@/game-sorter';
import playerSorter from '@/player-sorter';
import moment from 'moment';

export default {
    namespaced: true,
    state: {
        _gameSortOrder: 'date',
        _games: [],
        _playerSortOrder: 'winnings',
        _initialized: false,
        _selectedYear: null,
        _isPageNavExpanded: false,
        _isYearNavExpanded: false,
        _ready: false
    },
    getters: {
        gameSortOrder(state) {
            return state._gameSortOrder;
        },
        games(state) {
            return state._games;
        },
        playerSortOrder(state) {
            return state._playerSortOrder;
        },
        selectedYear(state) {
            return state._selectedYear;
        },
        isPageNavExpanded(state) {
            return state._isPageNavExpanded;
        },
        isYearNavExpanded(state) {
            return state._isYearNavExpanded;
        },
        sortedGames: state => {
            var selectedGames = getSelectedGames(state._games, state._selectedYear);
            return gameSorter.sort(selectedGames, state._gameSortOrder);
        },
        sortedPlayers: (state, getters) => {
            return playerSorter.sort(getPlayers(getters.sortedGames), state._playerSortOrder);
        },
        currentYearGames: (state, getters) => {
            var selectedGames = getSelectedGames(state._games, getters.currentYear);
            return gameSorter.sort(selectedGames, 'date');
        },
        currentYearPlayers: (state, getters) => {
            return playerSorter.sort(getPlayers(getters.currentYearGames), 'winnings');
        },
        allYearsPlayers: state => {
            return playerSorter.sort(getPlayers(state._games), 'winnings');
        },
        years: state => {
            return getYears(state._games);
        },
        currentYear: state => {
            if (state._games.length > 0) {
                const latestGame = state._games[0];
                return moment(latestGame.startTime).year();
            }
            return null;
        },
        gamesReady: state => {
            return state._ready;
        },
        hasGames: state => {
            return state._games.length > 0;
        }
    },
    actions: {
        loadGames(context, data) {
            if (!context.state.initialized) {
                context.commit('setInitialized');
                api.getGames(data.slug)
                    .then(function (response) {
                        context.commit('setData', response.data);
                    });
            }
        },
        selectYear(context, data) {
            context.commit('setSelectedYear', data.year);
        },
        sortGames(context, sortOrder) {
            context.commit('setGameSortorder', sortOrder);
        },
        sortPlayers(context, sortOrder) {
            context.commit('setPlayerSortorder', sortOrder);
        },
        togglePageNav(context) {
            context.commit('togglePageNav', !context.state._isPageNavExpanded);
            context.commit('toggleYearNav', false);
        },
        toggleYearNav(context) {
            context.commit('toggleYearNav', !context.state._isYearNavExpanded);
            context.commit('togglePageNav', false);
        }
    },
    mutations: {
        setData(state, games) {
            state._games = getGames(games);
            state._ready = true;
        },
        setGameSortorder(state, sortOrder) {
            state._gameSortOrder = sortOrder;
        },
        setPlayerSortorder(state, sortOrder) {
            state._playerSortOrder = sortOrder;
        },
        setInitialized(state) {
            state.initialized = true;
        },
        setSelectedYear(state, year) {
            state._selectedYear = year ? year : null;
        },
        togglePageNav(state, isExpanded) {
            state._isPageNavExpanded = isExpanded;
        },
        toggleYearNav(state, isExpanded) {
            state._isYearNavExpanded = isExpanded;
        }
    }
};

function getGames(rawGames) {
    const games = rawGames.slice();
    for (let gi = 0; gi < games.length; gi++) {
        const game = games[gi];
        game.date = game.startTime;
        game.turnover = getTurnover(game);
        game.averageBuyin = getAverageBuyin(game);
        game.playerCount = game.players.length;
        game.duration = 0;
        const startTime = moment(game.startTime);
        const updatedTime = moment(game.updatedTime);
        game.duration =  getDurationInMinutes(startTime, updatedTime);
    }
    return games;
}

function getSelectedGames(games, selectedYear) {
    if (!selectedYear)
        return games;
    const selectedGames = [];
    for (let gi = 0; gi < games.length; gi++) {
        const game = games[gi];
        const year = moment(game.startTime).year();
        if (year === selectedYear) {
            selectedGames.push(game);
        }
    }
    return selectedGames;
}

function getYears(games) {
    const years = [];
    for (let gi = 0; gi < games.length; gi++) {
        const game = games[gi];
        const year = moment(game.startTime).year();
        if (!years.includes(year)) {
            years.push(year);
        }
    }
    return years;
}

function getAverageBuyin(game) {
    const sum = getTurnover(game);
    const playerCount = game.players.length;
    return Math.round(sum / playerCount);
}

function getTurnover(game) {
    let sum = 0;
    for (let pi = 0; pi < game.players.length; pi++) {
        sum += game.players[pi].buyin;
    }
    return sum;
}

function getPlayers(games) {
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
                    games: [],
                    gameCount: 0,
                    rank: 0,
                    playedTimeInMinutes: 0
                }
                resultsList.push(playerResult);
            }
            const buyinTime = moment(singlePlayerResult.startTime);
            const updatedTime = moment(singlePlayerResult.updatedTime);
            const timeDiffInMinutes = getDurationInMinutes(buyinTime, updatedTime);
            const playerGameResult = {
                gameId: game.id,
                buyin: singlePlayerResult.buyin,
                stack: singlePlayerResult.stack,
                winnings: singlePlayerResult.stack - singlePlayerResult.buyin,
                buyinTime: buyinTime,
                updatedTime: updatedTime,
                playedTimeInMinutes: timeDiffInMinutes,
                isWinner: isWinner
            };
            playerResult.winnings += playerGameResult.winnings;
            playerResult.buyin += playerGameResult.buyin;
            playerResult.stack += playerGameResult.stack;
            playerResult.games.push(playerGameResult);
            playerResult.gameCount++;
            playerResult.playedTimeInMinutes += playerGameResult.playedTimeInMinutes;
        }
    }
    const sortedPlayers = playerSorter.sort(resultsList, 'winnings');
    for (let spi = 0; spi < sortedPlayers.length; spi++) {
        const p = sortedPlayers[spi];
        p.rank = spi + 1;
        p.winrate = getWinrate(p.winnings, p.playedTimeInMinutes);
    }
    return fillEmptyGames(sortedPlayers, games);
}

function getDurationInMinutes(startTime, endTime) {
    const timeDiff = moment.duration(endTime.diff(startTime));
    return Math.round(Math.abs(timeDiff.asMinutes()));
}

function getWinrate(winnings, timeInMinutes) {
    if (timeInMinutes === 0)
        return 0;
    const perMinute = winnings / timeInMinutes;
    return Math.round(perMinute * 60);
}

function getPlayerResult(results, id) {
    for (var i = 0; i < results.length; i++) {
        var playerResult = results[i];
        if (playerResult && playerResult.id === id)
            return playerResult;
    }
    return null;
}

function getWinningPlayerId(players) {
    var winningPlayerId = 0;
    var best = null;
    for (var i = 0; i < players.length; i++) {
        var player = players[i];
        var winnings = player.stack - player.buyin;
        if (best === null || winnings > best) {
            winningPlayerId = player.id;
            best = winnings;
        }
    }
    return winningPlayerId;
}

function fillEmptyGames(resultsList, games) {
    for (var pi = 0; pi < resultsList.length; pi++) {
        var player = resultsList[pi];
        var playerGames = [];
        for (var gi = 0; gi < games.length; gi++) {
            var playerGame = getPlayerGame(player.games, games[gi].id);
            playerGames.push(playerGame);
        }
        player.games = playerGames;
    }
    return resultsList;
}

function getPlayerGame(playerGames, gameId) {
    for (var i = 0; i < playerGames.length; i++) {
        var playerGame = playerGames[i];
        if (playerGame.gameId === gameId)
            return playerGame;
    }
    return false;
}

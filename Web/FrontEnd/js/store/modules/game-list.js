'use strict';
export default {
    namespaced: true,
    state: {
        orderBy: 'date',
        currencyFormat: '',
        thousandSeparator: '',
        games: []
    },
    getters: {
        sortedGames: state => {
            return sortGames(state.games, state.orderBy);
        }
    },
    actions: {
        setData(context, data) {
            context.commit('setData', data);
        },
        sortBy(context, orderBy) {
            context.commit('sortBy', orderBy);
        }
    },
    mutations: {
        setData(state, data) {
            state.orderBy = data.orderBy;
            state.currencyFormat = data.currencyFormat;
            state.thousandSeparator = data.thousandSeparator;
            state.games = data.games;
        },
        sortBy(state, orderBy) {
            state.orderBy = orderBy;
        }
    }
};

function sortGames(games, orderBy) {
    return games.slice().sort(getCompareFunc(orderBy)).reverse();
}

function getCompareFunc(orderBy) {
    if (orderBy === 'playercount')
        return comparePlayerCount;
    if (orderBy === 'duration')
        return compareDuration;
    if (orderBy === 'turnover')
        return compareTurnover;
    if (orderBy === 'averagebuyin')
        return compareAverageBuyin;
    return compareDate;
}

function compareDate(a, b) {
    return compareValues(a.date, b.date);
}

function comparePlayerCount(a, b) {
    return compareValues(a.playerCount, b.playerCount);
}

function compareDuration(a, b) {
    return compareValues(a.duration, b.duration);
}

function compareTurnover(a, b) {
    return compareValues(a.turnover, b.turnover);
}

function compareAverageBuyin(a, b) {
    return compareValues(a.averageBuyin, b.averageBuyin);
}

function compareValues(a, b) {
    if (a < b)
        return -1;
    else if (a > b)
        return 1;
    else
        return 0;
}

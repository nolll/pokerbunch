'use strict';
export default {
    namespaced: true,
    state: {
        orderBy: 'winnings',
        currencyFormat: '',
        thousandSeparator: '',
        players: []
    },
    getters: {
        sortedPlayers: state => {
            return sortPlayers(state.players, state.orderBy);
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
            state.players = data.players;
        },
        sortBy(state, orderBy) {
            state.orderBy = orderBy;
        }
    }
};

function sortPlayers(players, orderBy) {
    return players.slice().sort(getCompareFunc(orderBy)).reverse();
}

function getCompareFunc(orderBy) {
    if (orderBy === "buyin")
        return compareBuyin;
    if (orderBy === "cashout")
        return compareCashout;
    if (orderBy === "time")
        return compareTime;
    if (orderBy === "gamecount")
        return compareGameCount;
    if (orderBy === "winrate")
        return compareWinRate;
    return compareWinnings;
}

function compareWinnings(a, b) {
    return compareValues(a.winnings, b.winnings);
}

function compareBuyin(a, b) {
    return compareValues(a.buyin, b.buyin);
}

function compareCashout(a, b) {
    return compareValues(a.cashout, b.cashout);
}

function compareTime(a, b) {
    return compareValues(a.time, b.time);
}

function compareGameCount(a, b) {
    return compareValues(a.gameCount, b.gameCount);
}

function compareWinRate(a, b) {
    return compareValues(a.winRate, b.winRate);
}

function compareValues(a, b) {
    if (a < b)
        return -1;
    else if (a > b)
        return 1;
    else
        return 0;
}
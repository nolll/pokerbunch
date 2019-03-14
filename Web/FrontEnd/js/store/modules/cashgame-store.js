import moment from 'moment';
import api from '@/api';
import playerCalculator from '@/player-calculator';
import actionTypes from '@/action-types';

const longRefresh = 30000;

export default {
    namespaced: true,
    state: {
        _slug: '',
        _id: '',
        _isRunning: false,
        _playerId: null,
        _locationId: '',
        _locationName: '',
        _players: [],
        _currentStack: 0,
        _reportFormVisible: false,
        _buyinFormVisible: false,
        _cashoutFormVisible: false,
        _cashgameReady: false
    },
    getters: {
        id: state => state._id,
        isRunning: state => state._isRunning,
        locationId: state => state._locationId,
        locationName: state => state._locationName,
        reportFormVisible: state => state._reportFormVisible,
        buyinFormVisible: state => state._buyinFormVisible,
        cashoutFormVisible: state => state._cashoutFormVisible,
        players: state => state._players,
        hasPlayers: state => state._players.length > 0,
        playerId: (state, getters, rootState, rootGetters) => {
            if(state._playerId)
                return state._playerId;
            return rootGetters['bunch/playerId'];
        },
        sortedPlayers: (state) => {
            return state._players.slice().sort(function (left, right) {
                return playerCalculator.getWinnings(right) - playerCalculator.getWinnings(left);
            });
        },
        getPlayer: (state) => (id) => {
            if (!id)
                return null;
            return state._players.find(p => p.id.toString() === id.toString()) || null;
        },
        isInGame: (state, getters) => {
            return getters.player !== null;
        },
        canReport: (state, getters) => {
            return getters.isInGame && !getters.hasCashedOut;
        },
        canBuyin: (state, getters) => {
            return !getters.hasCashedOut;
        },
        canCashout: (state, getters) => {
            return getters.isInGame;
        },
        canEndGame: state => {
            var i;
            if (state._players.length === 0)
                return false;
            for (i = 0; i < state._players.length; i++) {
                if (!playerCalculator.hasCashedOut(state._players[i])) {
                    return false;
                }
            }
            return true;
        },
        hasCashedOut: (state, getters) => {
            if (!getters.isInGame)
                return false;
            return playerCalculator.hasCashedOut(getters.player);
        },
        player: (state, getters) => {
            return getters.getPlayer(getters.playerId);
        },
        totalStacks: state => {
            var sum = 0;
            for (var i = 0; i < state._players.length; i++) {
                var c = state._players[i].actions;
                sum += c.length > 0 ? c[c.length - 1].stack : 0;
            }
            return sum;
        },
        totalBuyin: state => {
            var sum = 0;
            for (var i = 0; i < state._players.length; i++) {
                var buyin = 0;
                var player = state._players[i];
                if (player.actions.length === 0)
                    continue;

                for (var j = 0; j < player.actions.length; j++) {
                    buyin += player.actions[j].added || 0;
                }
                sum += buyin;
            }
            return sum;
        },
        startTime: state => {
            var i,
                first,
                t = moment().utc(),
                p = state._players;

            if (p.length === 0)
                return t;
            for (i = 0; i < p.length; i++) {
                first = p[i].actions[0];
                if (first) {
                    const firstTime = moment(first.time);
                    if (firstTime.isBefore(t)) {
                        t = firstTime;
                    }
                }
            }
            return t;
        },
        updatedTime: state => {
            return moment(state._updatedTime);
        },
        cashgameReady(state) {
            return state._cashgameReady;
        }
    },
    actions: {
        loadCashgame(context, { id }) {
            api.getCashgame(id)
                .then(function (response) {
                    if (response.status === 200) {
                        context.commit('dataLoaded', response.data);
                    }
                    context.commit('loadingComplete');
                    setupRefresh(context, longRefresh);
                })
                .catch(function () {
                    
                });
        },
        selectPlayer(context, { playerId }) {
            context.commit('setPlayerId', playerId);
        },
        refresh(context) {
            refresh(context);
        },
        report(context, { stack }) {
            const player = context.getters.player;
            const reportData = { playerId: player.id, stack: stack };
            api.report(context.state._slug, reportData)
                .then(function() {
                    refresh(context);
                });
            context.commit('report', { player, reportData });
            context.commit('hideForms');
            context.commit('resetPlayerId');
        },
        buyin(context, { amount, stack }) {
            const buyinData = { playerId: context.getters.playerId, stack: stack, added: amount };
            const player = context.getters.player;
            api.buyin(context.state._slug, buyinData)
                .then(function () {
                    refresh(context);
                });
            context.commit('buyin', { player, buyinData });
            context.commit('hideForms');
            context.commit('resetPlayerId');
        },
        firstBuyin(context, { amount, stack, name, color }) {
            const player = createPlayer(context.getters.playerId, name, color);
            context.commit('addPlayer', { player });
            context.dispatch('buyin', { amount: amount, stack: stack });
        },
        cashout(context, { stack }) {
            const player = context.getters.player;
            const cashoutData = { playerId: player.id, stack: stack };
            api.cashout(context.state._slug, cashoutData)
                .then(function () {
                    refresh(context);
                });
            context.commit('cashout', { player, cashoutData });
            context.commit('hideForms');
            context.commit('resetPlayerId');
        },
        showReportForm(context) {
            refresh(context);
            context.commit('showReportForm');
        },
        showBuyinForm(context) {
            refresh(context);
            context.commit('showBuyinForm');
        },
        showCashoutForm(context) {
            refresh(context);
            context.commit('showCashoutForm');
        },
        hideForms(context) {
            context.commit('hideForms');
        }
    },
    mutations: {
        setPlayerId(state, playerId) {
            state._playerId = playerId;
        },
        resetPlayerId(state) {
            state._playerId = null;
        },
        setPlayers(state, players) {
            state._players = players;
        },
        setIsRunning(state, isRunning) {
            state._isRunning = isRunning;
        },
        report(state, { player, reportData }) {
            state._currentStack = reportData.stack;
            addCheckpoint(actionTypes.report, player, reportData.stack, 0);
        },
        cashout(state, { player, cashoutData }) {
            state._currentStack = cashoutData.stack;
            addCheckpoint(actionTypes.cashout, player, cashoutData.stack, 0);
        },
        buyin(state, { player, buyinData }) {
            const afterStack = buyinData.stack + buyinData.added;
            state._currentStack = afterStack;
            addCheckpoint(actionTypes.buyin, player, afterStack, buyinData.added);
        },
        addPlayer(state, { player }) {
            state._players.push(player);
        },
        showReportForm(state) {
            state._reportFormVisible = true;
        },
        showBuyinForm(state) {
            state._buyinFormVisible = true;
        },
        showCashoutForm(state) {
            state._cashoutFormVisible = true;
        },
        hideForms(state) {
            state._reportFormVisible = false;
            state._buyinFormVisible = false;
            state._cashoutFormVisible = false;
        },
        loadingComplete(state) {
            state._cashgameReady = true;
        },
        dataLoaded(state, data) {
            state._isRunning = data.isRunning;
            state._id = data.id;
            state._slug = data.bunch.id;
            state._locationId = data.location.id;
            state._locationName = data.location.name;
            state._updatedTime = data.updatedTime;
            state._players = data.players;
        }
    }
};

function setupRefresh(context, refreshTimeout) {
    if (context.state._isRunning) {
        window.setTimeout(function () {
            refresh(context);
        }, refreshTimeout);
    }
}

function refresh(context) {
    api.getCashgame(context.state._id)
        .then(function (response) {
            if (response.status === 200) {
                setPlayers(context, response.data);
            }
        })
        .catch(function(response) {
            console.log('refresh error');
        });
}

function setPlayers(context, data) {
    context.commit('setPlayers', data.players);
    context.commit('setIsRunning', data.isRunning);
    setupRefresh(context, longRefresh);
}

function addCheckpoint(type, player, stack, added) {
    const action = { type: type, time: moment().utc().format(), stack: stack, added: added };
    player.actions.push(action);
}

function createPlayer(playerId, playerName, playerColor) {
    return {
        id: playerId,
        name: playerName || '',
        color: playerColor || '#9e9e9e',
        url: null,
        actions: []
    };
}

import moment from 'moment';
import api from '@/api';
import playerCalculator from '@/player-calculator';
import actionTypes from '@/action-types';

var longRefresh = 30000,
    shortRefresh = 10000;

export default {
    namespaced: true,
    state: {
        _slug: '',
        _isRunning: false,
        _playerId: null,
        _locationUrl: '',
        _defaultBuyin: 0,
        _locationName: '',
        _isManager: false,
        _players: [],
        _currentStack: 0,
        _loadedPlayerId: '0',
        _reportFormVisible: false,
        _buyinFormVisible: false,
        _cashoutFormVisible: false,
        _currentGameReady: false
    },
    getters: {
        isRunning: state => state._isRunning,
        locationUrl: state => state._locationUrl,
        locationName: state => state._locationName,
        reportFormVisible: state => state._reportFormVisible,
        buyinFormVisible: state => state._buyinFormVisible,
        cashoutFormVisible: state => state._cashoutFormVisible,
        defaultBuyin: state => state._defaultBuyin,
        isManager: state => state._isManager,
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
            return getters.getPlayer(state._playerId);
        },
        totalStacks: state => {
            var sum = 0;
            for (var i = 0; i < state._players.length; i++) {
                var c = state._players[i].checkpoints;
                sum += c.length > 0 ? c[c.length - 1].stack : 0;
            }
            return sum;
        },
        totalBuyin: state => {
            var sum = 0;
            for (var i = 0; i < state._players.length; i++) {
                var buyin = 0;
                var player = state._players[i];
                if (player.checkpoints.length === 0)
                    continue;

                for (var j = 0; j < player.checkpoints.length; j++) {
                    buyin += player.checkpoints[j].addedMoney;
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
                return '';
            for (i = 0; i < p.length; i++) {
                first = p[i].checkpoints[0];
                if (first) {
                    var firstTime = moment(first.time);
                    if (firstTime.isBefore(t)) {
                        t = firstTime;
                    }
                }
            }
            return t;
        },
        currentGameReady(state) {
            return state._currentGameReady;
        }
    },
    actions: {
        loadCurrentGame(context, { slug }) {
            api.getCurrentGame(slug)
                .then(function (response) {
                    if (response.status === 200) {
                        context.commit('dataLoaded', response.data);
                    }
                    context.commit('loadingComplete', slug);
                    setupRefresh(context, longRefresh);
                })
                .catch(function () {
                    setupRefresh(context, shortRefresh);
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
            const buyinData = { playerId: context.state._playerId, stack: stack, addedMoney: amount };
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
            const player = createPlayer(context.state._playerId, name, color);
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
            state._playerId = state._loadedPlayerId;
        },
        setPlayers(state, players) {
            state._players = players;
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
            const afterStack = buyinData.stack + buyinData.addedMoney;
            state._currentStack = afterStack;
            addCheckpoint(actionTypes.buyin, player, afterStack, buyinData.addedMoney);
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
        loadingComplete(state, slug) {
            state._currentGameReady = true;
            state._slug = slug;
        },
        dataLoaded(state, data) {
            state._isRunning = true;
            state._playerId = data.playerId;
            state._locationUrl = data.locationUrl;
            state._defaultBuyin = data.defaultBuyin;
            state._locationName = data.locationName;
            state._isManager = data.isManager;
            state._players = data.players;
            state._loadedPlayerId = data.playerId;
        }
    }
};

function setupRefresh(context, refreshTimeout) {
    window.setTimeout(function () {
        refresh(context);
    }, refreshTimeout);
}

function refresh(context) {
    api.getCurrentGame(context.state._slug)
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
    setupRefresh(context, longRefresh);
}

function addCheckpoint(type, player, stack, addedMoney) {
    const checkpoint = { type: type, time: moment().utc().format(), stack: stack, addedMoney: addedMoney };
    player.checkpoints.push(checkpoint);
}

function createPlayer(playerId, playerName, playerColor) {
    return {
        id: playerId,
        name: playerName || '',
        color: playerColor || '#9e9e9e',
        url: null,
        checkpoints: []
    };
}

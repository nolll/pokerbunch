import api from '@/api';
import moment from 'moment';

var longRefresh = 30000,
    shortRefresh = 10000;

export default {
    namespaced: true,
    state: {
        _slug: '',
        _isRunning: false,
        _playerId: '0',
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
        playerId: state => state._playerId,
        locationUrl: state => state._locationUrl,
        locationName: state => state._locationName,
        reportFormVisible: state => state._reportFormVisible,
        buyinFormVisible: state => state._buyinFormVisible,
        cashoutFormVisible: state => state._cashoutFormVisible,
        defaultBuyin: state => state._defaultBuyin,
        isManager: state => state._isManager,
        players: state => state._players,
        hasPlayers: state => state._players.length > 0,
        sortedPlayers: (state, getters) => {
            return state._players.slice().sort(function (left, right) {
                return getters.getWinnings(right) - getters.getWinnings(left);
            });
        },
        getPlayer: (state) => (id) => {
            if (!state._players)
                return null;
            var i;
            for (i = 0; i < state._players.length; i++) {
                if (state._players[i].id === id) {
                    return state._players[i];
                }
            }
            return null;
        },
        getLastReportTime: () => (player) => {
            if (player.checkpoints.length === 0)
                return moment().fromNow();
            return moment(player.checkpoints[player.checkpoints.length - 1].time).fromNow();
        },
        getBuyin: () => (player) => {
            if (player.checkpoints.length === 0)
                return 0;
            var sum = 0;
            for (var i = 0; i < player.checkpoints.length; i++) {
                sum += player.checkpoints[i].addedMoney;
            }
            return sum;
        },
        getStack: () => (player) => {
            var c = player.checkpoints;
            if (c.length === 0)
                return 0;
            return c[c.length - 1].stack;
        },
        getWinnings: (state, getters) => (player) => {
            return getters.getStack(player) - getters.getBuyin(player);
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
                if (!state._players[i].hasCashedOut) {
                    return false;
                }
            }
            return true;
        },
        hasCashedOut: (state, getters) => {
            if (!getters.isInGame)
                return false;
            return getters.player.hasCashedOut;
        },
        player: (state, getters) => {
            return getters.getPlayer(state._playerId);
        },
        bunchPlayer: state => {
            var i,
                bp = state._bunchPlayers;
            for (i = 0; i < bp.length; i++) {
                if (bp[i].id === state._playerId) {
                    return bp[i];
                }
            }
            return null;
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
            addCheckpoint(player, reportData.stack, 0);
        },
        cashout(state, { player, cashoutData }) {
            state._currentStack = cashoutData.stack;
            addCheckpoint(player, cashoutData.stack, 0);
            player.hasCashedOut = true;
        },
        buyin(state, { player, buyinData }) {
            const afterStack = buyinData.stack + buyinData.addedMoney;
            state._currentStack = afterStack;
            addCheckpoint(player, afterStack, buyinData.addedMoney);
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

function addCheckpoint(player, stack, addedMoney) {
    const checkpoint = { time: moment().utc().format(), stack: stack, addedMoney: addedMoney };
    player.checkpoints.push(checkpoint);
}

function createPlayer(playerId, playerName, playerColor) {
    return {
        id: playerId,
        name: playerName || '',
        color: playerColor || '#9e9e9e',
        url: null,
        hasCashedOut: false,
        checkpoints: []
    };
}

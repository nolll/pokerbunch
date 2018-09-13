'use strict';

import Vue from 'vue';
import Vuex from 'vuex';
import ajax from '../../ajax';
import moment from 'moment';

Vue.use(Vuex);

var longRefresh = 30000,
    shortRefresh = 10000;

export default {
    namespaced: true,
    state: {
        slug: '',
        playerId: '0',
        refreshUrl: '',
        reportUrl: '',
        buyinUrl: '',
        cashoutUrl: '',
        cashgameIndexUrl: '',
        locationUrl: '',
        defaultBuyin: 0,
        locationName: '',
        isManager: false,
        bunchPlayers: [],
        players: [],
        currentStack: 0,
        beforeBuyinStack: 0,
        buyinAmount: 0,
        loadedPlayerId: '0',
        currencyFormat: '${0}',
        thousandSeparator: ',',
        reportFormVisible: false,
        buyinFormVisible: false,
        cashoutFormVisible: false,
        initialized: false
    },
    getters: {
        hasPlayers: state => {
            return state.players.length > 0;
        },
        sortedPlayers: (state, getters) => {
            return state.players.slice().sort(function (left, right) {
                return getters.getWinnings(right) - getters.getWinnings(left);
            });
        },
        getPlayer: (state) => (id) => {
            var i;
            for (i = 0; i < state.players.length; i++) {
                if (state.players[i].id === id) {
                    return state.players[i];
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
            if (state.players.length === 0)
                return false;
            for (i = 0; i < state.players.length; i++) {
                if (!state.players[i].hasCashedOut) {
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
            return getters.getPlayer(state.playerId);
        },
        bunchPlayer: state => {
            var i,
                bp = state.bunchPlayers;
            for (i = 0; i < bp.length; i++) {
                if (bp[i].id === state.playerId) {
                    return bp[i];
                }
            }
            return null;
        },
        totalStacks: state => {
            var sum = 0;
            for (var i = 0; i < state.players.length; i++) {
                var c = state.players[i].checkpoints;
                sum += c.length > 0 ? c[c.length - 1].stack : 0;
            }
            return sum;
        },
        totalBuyin: state => {
            var sum = 0;
            for (var i = 0; i < state.players.length; i++) {
                var buyin = 0;
                var player = state.players[i];
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
                p = state.players;

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
        }
    },
    actions: {
        loadCurrentGame(context, { slug }) {
            const url = '/cashgame/runninggamejson/' + slug;
            ajax.get(url, function (data) {
                context.commit('currentGameLoaded', data);
                setupRefresh(context, longRefresh);
            }, function () {
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
            ajax.post(context.state.reportUrl, reportData, function () { refresh(context) });
            context.commit('report', { player, reportData });
            context.commit('hideForms');
            context.commit('resetPlayerId');
        },
        buyin(context, { amount, stack }) {
            const buyinData = { playerId: context.state.playerId, stack: stack, addedMoney: amount };
            let player = context.getters.player;
            if (!player) {
                player = createPlayer(context.state);

                context.commit('addPlayer', { player });
            }
            ajax.post(context.state.buyinUrl, buyinData, function () { refresh(context) });
            context.commit('buyin', { player, buyinData });
            context.commit('hideForms');
            context.commit('resetPlayerId');
        },
        cashout(context, { stack }) {
            const player = context.getters.player;
            const cashoutData = { playerId: player.id, stack: stack };
            ajax.post(context.state.cashoutUrl, cashoutData, function () { refresh(context) });
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
            state.playerId = playerId;
        },
        resetPlayerId(state) {
            state.playerId = state.loadedPlayerId;
        },
        setPlayers(state, players) {
            state.players = players;
        },
        report(state, { player, reportData }) {
            state.currentStack = reportData.stack;
            addCheckpoint(player, reportData.stack, 0);
        },
        cashout(state, { player, cashoutData }) {
            state.currentStack = cashoutData.stack;
            addCheckpoint(player, cashoutData.stack, 0);
            player.hasCashedOut = true;
        },
        buyin(state, { player, buyinData }) {
            state.buyinAmount = buyinData.addedMoney;
            state.beforeBuyinStack = buyinData.stack;
            state.currentStack = state.defaultBuyin;
            const afterStack = buyinData.stack + buyinData.addedMoney;
            addCheckpoint(player, afterStack, buyinData.addedMoney);
        },
        addPlayer(state, { player }) {
            state.players.push(player);
        },
        showReportForm(state) {
            state.reportFormVisible = true;
        },
        showBuyinForm(state) {
            state.buyinFormVisible = true;
        },
        showCashoutForm(state) {
            state.cashoutFormVisible = true;
        },
        hideForms(state) {
            state.reportFormVisible = false;
            state.buyinFormVisible = false;
            state.cashoutFormVisible = false;
        },
        currentGameLoaded(state, data) {
            state.slug = data.slug;
            state.playerId = data.playerId;
            state.refreshUrl = data.refreshUrl;
            state.reportUrl = data.reportUrl;
            state.buyinUrl = data.buyinUrl;
            state.cashoutUrl = data.cashoutUrl;
            state.cashgameIndexUrl = data.cashgameIndexUrl;
            state.locationUrl = data.locationUrl;
            state.defaultBuyin = data.defaultBuyin;
            state.currencyFormat = data.currencyFormat;
            state.thousandSeparator = data.thousandSeparator;
            state.locationName = data.locationName;
            state.isManager = data.isManager;
            state.bunchPlayers = data.bunchPlayers;
            state.players = data.players;
            state.buyinAmount = data.defaultBuyin;
            state.loadedPlayerId = data.playerId;
            state.initialized = true;
        }
    }
};

function setupRefresh(context, refreshTimeout) {
    window.setTimeout(function () {
        refresh(context);
    }, refreshTimeout);
}

function refresh(context) {
    ajax.get(context.state.refreshUrl,
        function (playerData) {
            setPlayers(context, playerData);
        },
        function () {
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

function createPlayer(state) {
    const bunchPlayer = state.bunchPlayer;
    const playerName = bunchPlayer != null ? bunchPlayer.name : '';
    const playerColor = bunchPlayer != null ? bunchPlayer.color : '#9e9e9e';
    return {
        id: state.playerId,
        name: playerName,
        color: playerColor,
        url: null,
        hasCashedOut: false,
        checkpoints: []
    };
}

'use strict';

import Vue from 'vue';
import Vuex from 'vuex';
import ajax from '../../ajax';
import gameService from '../../game-service';
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
        startTime: state => {
            return gameService.getStartTime(state.players);
        },
        sortedPlayers: state => {
            return gameService.sortPlayers(state.players);
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
            return gameService.canBeEnded(state.players);
        },
        hasCashedOut: (state, getters) => {
            if (!getters.player)
                return false;
            return getters.player.hasCashedOut;
        },
        player: state => {
            return gameService.getPlayer(state.players, state.playerId);
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
                context.commit('addPlayer', player);
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

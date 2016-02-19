define(["jquery", "vue", "moment", "text!standings.html", "ajax", "/signalr/hubs?"],
    function($, vue, moment, html, ajax) {
        "use strict";

        return vue.extend({
            template: html,
            data: function() {
                return {
                    slug: "",
                    playerId: 0,
                    refreshUrl: "",
                    reportUrl: "",
                    buyinUrl: "",
                    cashoutUrl: "",
                    endGameUrl: "",
                    cashgameIndexUrl: "",
                    locationUrl: "",
                    defaultBuyin: 0,
                    locationName: "",
                    isManager: false,
                    bunchPlayers: [],
                    players: [],
                    areButtonsVisible: true,
                    reportFormVisible: false,
                    buyInFormVisible: false,
                    cashOutFormVisible: false,
                    endGameFormVisible: false,
                    currentStack: 0,
                    beforeBuyInStack: 0,
                    buyInAmount: 0,
                    loadedPlayerId: 0,
                    currencyFormat: '{0} kr',
                    initialized: false
                }
            },
            ready: function() {
                this.initData(this.url);
            },
            computed: {
                hasPlayers: function () {
                    return this.players.length > 0;
                },
                noPlayers: function() {
                    return this.players.length === 0;
                },
                startTime: function () {
                    var i, first,
                    t = moment().utc(),
                    p = this.players;
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
                    return t.format('HH:mm');
                },
                sortedPlayers: function() {
                    return this.players.sort(function (left, right) {
                        return right.winnings - left.winnings;
                    });
                },
                isInGame: function () {
                    return this.getPlayer(this.playerId) !== null;
                },
                canCashOut: function () {
                    return this.isInGame;
                },
                hasCashedOut: function() {
                    var player = this.getPlayer(this.playerId);
                    if (!player)
                        return false;
                    return player.hasCashedOut;
                },
                canEndGame: function () {
                    var i;
                    if (this.players.length === 0)
                        return false;
                    for (i = 0; i < this.players.length; i++) {
                        if (!this.players[i].hasCashedOut) {

                            return false;
                        }
                    }
                    return true;
                },
                canReport: function () {
                    return this.isInGame && !this.hasCashedOut;
                },
                canBuyIn: function () {
                    return !this.hasCashedOut;
                }
            },
            methods: {
                showReportForm: function () {
                    //refresh(me.setPlayers);
                    this.reportFormVisible = true;
                    this.hideButtons();
                },
                showBuyInForm: function () {
                    //refresh(me.setPlayers);
                    this.buyInFormVisible = true;
                    this.hideButtons();
                },
                showCashOutForm: function () {
                    //refresh(me.setPlayers);
                    this.cashOutFormVisible = true;
                    this.hideButtons();
                },
                showEndGameForm: function () {
                    //refresh(me.setPlayers);
                    this.endGameFormVisible = true;
                    this.hideButtons();
                },
                hideButtons: function() {
                    this.areButtonsVisible = false;
                },
                hideForms: function () {
                    this.reportFormVisible = false;
                    this.buyInFormVisible = false;
                    this.cashOutFormVisible = false;
                    this.endGameFormVisible = false;
                    this.areButtonsVisible = true;
                },
                getPlayer: function (playerId) {
                    var i;
                    for (i = 0; i < this.players.length; i++) {
                        if (this.players[i].id === playerId) {
                            return this.players[i];
                        }
                    }
                    return null;
                },
                hasCashedOut: function() {
                    var player = this.getPlayer(this.playerId);
                    if (!player)
                        return false;
                    return player.hasCashedOut();
                },
                loadComplete: function (data) {
                    this.slug = data.slug;
                    this.playerId = data.playerId;
                    this.refreshUrl = data.refreshUrl;
                    this.reportUrl = data.reportUrl;
                    this.buyinUrl = data.buyinUrl;
                    this.cashoutUrl = data.cashoutUrl;
                    this.endGameUrl = data.endGameUrl;
                    this.cashgameIndexUrl = data.cashgameIndexUrl;
                    this.locationUrl = data.locationUrl;
                    this.defaultBuyin = data.defaultBuyin;
                    this.locationName = data.locationName;
                    this.isManager = data.isManager;
                    this.bunchPlayers = data.bunchPlayers;
                    this.players = data.players;
                    this.buyInAmount = data.defaultBuyin;
                    this.loadedPlayerId = data.playerId;
                    this.initialized = true;
                    this.initSockets(data.slug);
                },
                loadError: function() {
                    
                },
                initData: function(url) {
                    ajax.load(url, this.loadComplete, this.loadError);
                },
                addCheckpoint: function (player, stack, addedMoney) {
                    var checkpoint = { time: moment().utc().format(), stack: stack, addedMoney: addedMoney };
                    player.checkpoints.push(checkpoint);
                },
                getBunchPlayer: function () {
                    var i,
                        bp = this.bunchPlayers();
                    for (i = 0; i < bp.length; i++) {
                        if (bp[i].id === this.playerId()) {
                            return bp[i];
                        }
                    }
                    return null;
                },
                resetPlayerId: function() {
                    this.playerId = this.loadedPlayerId;
                },
                report: function (stack) {
                    this.currentStack = stack;
                    var reportData = { playerId: this.playerId, stack: stack };
                    var player = this.getPlayer(this.playerId);
                    this.addCheckpoint(player, reportData.stack, 0);
                    this.hideForms();
                    var playerId = this.playerId;
                    var callback = this.notify;
                    ajax.post(this.reportUrl, reportData, function() {
                        callback(playerId);
                    });
                    this.resetPlayerId();
                },
                buyin: function (amount, stack) {
                    this.buyInAmount = amount;
                    this.beforeBuyInStack = stack;
                    var afterStack = stack + amount;
                    var buyInData = { playerId: this.playerId, stack: stack, addedMoney: amount };
                    var player = this.getPlayer(this.playerId);
                    if (!player) {
                        player = this.createPlayer();
                        this.addCheckpoint(player, afterStack, buyInData.addedMoney);
                        this.players.push(player);
                    } else {
                        player.addCheckpoint(player, afterStack, buyInData.addedMoney);
                    }
                    this.hideForms();
                    this.currentStack = this.defaultBuyIn;
                    var playerId = this.playerId;
                    var callback = this.notify;
                    ajax.post(this.buyInUrl, buyInData, function () {
                        callback(playerId);
                    });
                    this.resetPlayerId();
                },
                createPlayer: function() {
                    var bunchPlayer = this.getBunchPlayer();
                    var playerName = bunchPlayer != null ? bunchPlayer.name : '';
                    var playerColor = bunchPlayer != null ? bunchPlayer.color : '#9e9e9e';
                    return {
                        id: this.playerId,
                        name: playerName,
                        color: playerColor,
                        url: null,
                        hasCashedOut: false,
                        checkpoints: []
                    };
                },
                cashout: function (stack) {
                    this.currentStack = stack;
                    var cashOutData = { playerId: this.playerId, stack: stack };
                    var player = this.getPlayer(this.playerId);
                    this.addCheckpoint(player, cashOutData.stack, 0);
                    player.hasCashedOut = true;
                    this.hideForms();
                    ajax.post(this.cashOutUrl, cashOutData, this.notify);
                    this.resetPlayerId();
                },
                endgame: function () {
                    ajax.post(this.endGameUrl, null, function () {
                        this.notify();
                        location.href = this.cashgameIndexUrl;
                    });
                },
                refresh: function(callback) {
                    ajax.load(this.refreshUrl, function (playerData) {
                        callback(playerData);
                    });
                },
                initSockets: function (slug) {
                    var socket = $.connection.runningGameHub;
                    socket.client.updateClient = this.updateClient;
                    $.connection.hub.start({ pingInterval: null }).done(function () {
                        socket.server.joinGame(slug);
                    });
                    $.connection.hub.disconnected(function () {
                        setTimeout(function () {
                            $.connection.hub.start();
                        }, 5000);
                    });
                },
                updateClient: function (playerId) {
                    if (playerId !== this.playerId) {
                        this.refresh(this.setPlayers);
                    }
                },
                setPlayers: function(data) {
                    this.players = data.players;
                },
                notify: function() {
                    var socket = $.connection.runningGameHub;
                    socket.server.dataUpdated(this.slug, this.playerId);
                }
            },
            events: {
                'change-player': function (playerId) {
                    this.playerId = playerId;
                },
                'report': function (stack) {
                    this.report(stack);
                },
                'buyin': function (amount, stack) {
                    this.buyin(amount, stack);
                },
                'cashout': function (stack) {
                    this.cashout(stack);
                },
                'endgame': function() {
                    this.endgame();
                },
                'hide-forms': function() {
                    this.hideForms();
                }
            }
        });
    }
);
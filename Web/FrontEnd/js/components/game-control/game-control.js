define(["vue", "moment", "./game-control.html", "../../ajax", "../../game-service"],
    function(vue, moment, html, ajax, gameService) {
        "use strict";

        var longRefresh = 30000,
            shortRefresh = 10000;

        return {
            template: html,
            data: defaultData,
            props: ["url"],
            mounted: function () {
                var self = this;
                self.$nextTick(function() {
                    self.initData(self.url);
                });
            },
            computed: {
                hasPlayers: function() {
                    return this.players.length > 0;
                },
                startTime: function() {
                    var t = gameService.getStartTime(this.players);
                    return t.format("HH:mm");
                },
                sortedPlayers: function() {
                    return gameService.sortPlayers(this.players);
                },
                isInGame: function() {
                    return gameService.getPlayer(this.players, this.playerId) !== null;
                },
                canCashout: function() {
                    return this.isInGame;
                },
                canEndGame: function() {
                    return gameService.canBeEnded(this.players);
                },
                canReport: function() {
                    return this.isInGame && !this.hasCashedOut();
                },
                canBuyin: function() {
                    return !this.hasCashedOut();
                }
            },
            methods: {
                showReportForm: function() {
                    this.refresh();
                    this.reportFormVisible = true;
                    this.hideButtons();
                },
                showBuyinForm: function() {
                    this.refresh();
                    this.buyinFormVisible = true;
                    this.hideButtons();
                },
                showCashoutForm: function() {
                    this.refresh();
                    this.cashoutFormVisible = true;
                    this.hideButtons();
                },
                hideButtons: function() {
                    this.areButtonsVisible = false;
                },
                hideForms: function() {
                    this.reportFormVisible = false;
                    this.buyinFormVisible = false;
                    this.cashoutFormVisible = false;
                    this.areButtonsVisible = true;
                },
                hasCashedOut: function() {
                    var player = gameService.getPlayer(this.players, this.playerId);
                    if (!player)
                        return false;
                    return player.hasCashedOut;
                },
                loadComplete: function(data) {
                    this.slug = data.slug;
                    this.playerId = data.playerId;
                    this.refreshUrl = data.refreshUrl;
                    this.reportUrl = data.reportUrl;
                    this.buyinUrl = data.buyinUrl;
                    this.cashoutUrl = data.cashoutUrl;
                    this.cashgameIndexUrl = data.cashgameIndexUrl;
                    this.locationUrl = data.locationUrl;
                    this.defaultBuyin = data.defaultBuyin;
                    this.currencyFormat = data.currencyFormat;
                    this.thousandSeparator = data.thousandSeparator;
                    this.locationName = data.locationName;
                    this.isManager = data.isManager;
                    this.bunchPlayers = data.bunchPlayers;
                    this.players = data.players;
                    this.buyinAmount = data.defaultBuyin;
                    this.loadedPlayerId = data.playerId;
                    this.initialized = true;
                    this.setupRefresh(longRefresh);
                },
                loadError: function() {
                    this.setupRefresh(shortRefresh);
                },
                initData: function(url) {
                    ajax.get(url, this.loadComplete, this.loadError);
                },
                setupRefresh: function(refreshTimeout) {
                    window.setTimeout(this.refresh, refreshTimeout);
                },
                addCheckpoint: function(player, stack, addedMoney) {
                    var checkpoint = { time: moment().utc().format(), stack: stack, addedMoney: addedMoney };
                    player.checkpoints.push(checkpoint);
                },
                getBunchPlayer: function() {
                    var i,
                        bp = this.bunchPlayers;
                    for (i = 0; i < bp.length; i++) {
                        if (bp[i].id === this.playerId) {
                            return bp[i];
                        }
                    }
                    return null;
                },
                resetPlayerId: function() {
                    this.playerId = this.loadedPlayerId;
                },
                report: function(stack) {
                    this.currentStack = stack;
                    var reportData = { playerId: this.playerId, stack: stack };
                    var player = gameService.getPlayer(this.players, this.playerId);
                    this.addCheckpoint(player, reportData.stack, 0);
                    this.hideForms();
                    ajax.post(this.reportUrl, reportData);
                    this.resetPlayerId();
                },
                buyin: function(amount, stack) {
                    this.buyinAmount = amount;
                    this.beforeBuyinStack = stack;
                    var afterStack = stack + amount;
                    var buyinData = { playerId: this.playerId, stack: stack, addedMoney: amount };
                    var player = gameService.getPlayer(this.players, this.playerId);
                    if (!player) {
                        player = this.createPlayer();
                        this.addCheckpoint(player, afterStack, buyinData.addedMoney);
                        this.players.push(player);
                    } else {
                        this.addCheckpoint(player, afterStack, buyinData.addedMoney);
                    }
                    this.hideForms();
                    this.currentStack = this.defaultBuyin;
                    ajax.post(this.buyinUrl, buyinData);
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
                cashout: function(stack) {
                    this.currentStack = stack;
                    var cashoutData = { playerId: this.playerId, stack: stack };
                    var player = gameService.getPlayer(this.players, this.playerId);
                    this.addCheckpoint(player, cashoutData.stack, 0);
                    player.hasCashedOut = true;
                    this.hideForms();
                    var callback = this.redirectIfGameHasEnded;
                    ajax.post(this.cashoutUrl, cashoutData, callback);
                    this.resetPlayerId();
                },
                refresh: function() {
                    this.redirectIfGameHasEnded();
                    var callback = this.setPlayers;
                    ajax.get(this.refreshUrl,
                        function(playerData) {
                            callback(playerData);
                        });
                },
                setPlayers: function(data) {
                    this.players = data.players;
                    this.setupRefresh(longRefresh);
                },
                redirectIfGameHasEnded: function() {
                    if (this.canEndGame) {
                        location.href = this.cashgameIndexUrl;
                    }
                },
                setPlayerId: function(playerId) {
                    this.playerId = playerId;
                }
            },
            created: function() {
                this.eventHub.$on('change-player', this.setPlayerId);
                this.eventHub.$on('report', this.report);
                this.eventHub.$on('buyin', this.buyin);
                this.eventHub.$on('cashout', this.cashout);
                this.eventHub.$on('hide-forms', this.hideForms);
            },
            beforeDestroy: function() {
                this.eventHub.$off('change-player');
                this.eventHub.$off('report');
                this.eventHub.$off('buyin');
                this.eventHub.$off('cashout');
                this.eventHub.$off('hide-forms');
            }
        };

        function defaultData() {
            return {
                slug: "",
                playerId: "0",
                refreshUrl: "",
                reportUrl: "",
                buyinUrl: "",
                cashoutUrl: "",
                cashgameIndexUrl: "",
                locationUrl: "",
                defaultBuyin: 0,
                locationName: "",
                isManager: false,
                bunchPlayers: [],
                players: [],
                areButtonsVisible: true,
                reportFormVisible: false,
                buyinFormVisible: false,
                cashoutFormVisible: false,
                currentStack: 0,
                beforeBuyinStack: 0,
                buyinAmount: 0,
                loadedPlayerId: "0",
                currencyFormat: "${0}",
                thousandSeparator: ",",
                initialized: false
            }
        }
    }
);
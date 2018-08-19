define(["vue", "moment", "./dashboard.html", "../../ajax", "../../game-service"],
    function(vue, moment, html, ajax, gameService) {
        "use strict";

        var longRefresh = 30000,
            shortRefresh = 10000;

        return {
            template: html,
            data: defaultData,
            props: ["url"],
            mounted: function() {
                this.initData(this.url);
            },
            computed: {
                hasPlayers: function () {
                    return this.players.length > 0;
                },
                startTime: function () {
                    var t = gameService.getStartTime(this.players);
                    return t.format('HH:mm');
                },
                sortedPlayers: function() {
                    return gameService.sortPlayers(this.players);
                }
            },
            methods: {
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
                getBunchPlayer: function () {
                    var i,
                        bp = this.bunchPlayers;
                    for (i = 0; i < bp.length; i++) {
                        if (bp[i].id === this.playerId) {
                            return bp[i];
                        }
                    }
                    return null;
                },
                refresh: function() {
                    var callback = this.setPlayers;
                    ajax.get(this.refreshUrl, function (playerData) {
                        callback(playerData);
                    });
                },
                setPlayers: function (data) {
                    this.players = data.players;
                    this.setupRefresh(longRefresh);
                }
            }
        };

        function defaultData() {
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
                currentStack: 0,
                beforeBuyinStack: 0,
                buyinAmount: 0,
                loadedPlayerId: 0,
                currencyFormat: '{0} kr',
                initialized: false
            }
        }
    }
);
define(["jquery", "knockout", "moment", "select-on-focus"],
    function ($, ko, moment) {
        "use strict";

        function init() {
            var $el = $(this);
            var url = $el.data('url');
            loadData(url, function (data) {
                var standings = new StandingsViewModel(data);
                ko.applyBindings(standings);
                standings.startAutoRefresh();
            });
        }

        function formatResult(n) {
            if (n > 0)
                n = "+" + n;
            return formatCurrency(n);
        }

        function formatCurrency(n) {
            return n + " kr";
        }

        function StandingsViewModel(data) {
            var me = this;

            me.refreshTimeout = 30000;
            me.players = ko.observableArray(createPlayers(data));
            me.bunchPlayers = ko.observableArray(createBunchPlayers(data));
            me.loadedPlayerId = data.playerId;
            me.playerId = ko.observable(me.loadedPlayerId);
            me.reportUrl = data.reportUrl;
            me.buyInUrl = data.buyinUrl;
            me.cashOutUrl = data.cashoutUrl;
            me.endGameUrl = data.endGameUrl;
            me.cashgameIndexUrl = data.cashgameIndexUrl;
            me.defaultBuyIn = data.defaultBuyin;
            me.location = data.location;
            me.refreshUrl = data.refreshUrl;
            me.isManager = ko.observable(data.isManager);
            me.areButtonsVisible = ko.observable(true);
            me.reportFormVisible = ko.observable(false);
            me.buyInFormVisible = ko.observable(false);
            me.cashOutFormVisible = ko.observable(false);
            me.endGameFormVisible = ko.observable(false);
            me.currentStack = ko.observable(0);
            me.beforeBuyInStack = ko.observable(0);
            me.buyInAmount = ko.observable(me.defaultBuyIn);

            me.refresh = function(callback) {
                loadData(me.refreshUrl, function (playerData) {
                    callback(playerData);
                });
            };

            me.startAutoRefresh = function() {
                window.setTimeout(me.autoRefresh, me.refreshTimeout);
            }

            me.autoRefresh = function() {
                me.refresh(me.setPlayers);
                window.setTimeout(me.autoRefresh, me.refreshTimeout);
            }

            me.setPlayers = function(playerData) {
                me.players(createPlayers(playerData));
            };

            me.sortedPlayers = ko.computed(function () {
                return me.players().sort(function (left, right) {
                    return right.winnings() - left.winnings();
                });
            });

            me.report = function () {
                var reportData = { playerId: me.playerId(), stack: parseInt(me.currentStack()) };
                var player = me.getPlayer(me.playerId());
                player.addCheckpoint(reportData.stack, 0);
                me.hideForms();
                postData(me.reportUrl, reportData);
                me.resetPlayerId();
            };

            me.buyIn = function () {
                var buyinAmount = parseInt(me.buyInAmount());
                var beforeStack = parseInt(me.beforeBuyInStack());
                var afterStack = beforeStack + buyinAmount;
                var buyInData = { playerId: me.playerId(), stack: beforeStack, addedMoney: buyinAmount };
                var player = me.getPlayer(me.playerId());
                if (!player) {
                    var playerName = me.getPlayerName();
                    player = new PlayerViewModel(me.playerId(), playerName, null, false, []);
                    player.addCheckpoint(afterStack, buyInData.addedMoney);
                    me.players.push(player);
                } else {
                    player.addCheckpoint(afterStack, buyInData.addedMoney);
                }
                me.hideForms();
                me.currentStack(me.defaultBuyIn);
                postData(me.buyInUrl, buyInData);
                me.resetPlayerId();
            };

            me.cashOut = function () {
                var cashOutData = { playerId: me.playerId(), stack: parseInt(me.currentStack()) };
                var player = me.getPlayer(me.playerId());
                player.addCheckpoint(cashOutData.stack, 0);
                player.hasCashedOut(true);
                me.hideForms();
                postData(me.cashOutUrl, cashOutData);
                me.resetPlayerId();
            };

            me.endGame = function () {
                postData(me.endGameUrl);
                location.href = me.cashgameIndexUrl;
            };

            me.getPlayer = function (playerId) {
                var i;
                for (i = 0; i < me.players().length; i++) {
                    if (me.players()[i].id == playerId) {
                        return me.players()[i];
                    }
                }
                return null;
            }

            me.hasPlayers = ko.computed(function () {
                return me.players().length > 0;
            });

            me.noPlayers = ko.computed(function () {
                return !me.hasPlayers();
            });

            me.isInGame = ko.computed(function () {
                return me.getPlayer(me.playerId()) !== null;
            });

            me.canCashOut = ko.computed(function () {
                return me.isInGame();
            });

            me.hasCashedOut = ko.computed(function() {
                var player = me.getPlayer(me.playerId());
                if (!player)
                    return false;
                return player.hasCashedOut();
            });

            me.canEndGame = ko.computed(function () {
                var i;
                if (me.players().length === 0)
                    return false;
                for (i = 0; i < me.players().length; i++) {
                    if (!me.players()[i].hasCashedOut()) {
                        return false;
                    }
                }
                return true;
            });

            me.canReport = ko.computed(function () {
                return me.isInGame() && !me.hasCashedOut();
            });

            me.canBuyIn = ko.computed(function () {
                return !me.hasCashedOut();
            });

            me.addCheckpoint = function(playerId, stack, addedMoney) {
                var i;
                for (i = 0; i < me.players().length; i++) {
                    if (me.players()[i].id == playerId) {
                        me.players()[i].addCheckpoint({ time: moment().utc(), stack: stack, addedMoney: addedMoney });
                    }
                }
            };

            me.showReportForm = function () {
                me.reportFormVisible(true);
                me.hideButtons();
            };

            me.showBuyInForm = function () {
                me.buyInFormVisible(true);
                me.hideButtons();
            };

            me.showCashOutForm = function () {
                me.cashOutFormVisible(true);
                me.hideButtons();
            };

            me.showEndGameForm = function () {
                me.endGameFormVisible(true);
                me.hideButtons();
            };

            me.hideButtons = function() {
                me.areButtonsVisible(false);
            }

            me.hideForms = function() {
                me.reportFormVisible(false);
                me.buyInFormVisible(false);
                me.cashOutFormVisible(false);
                me.endGameFormVisible(false);
                me.areButtonsVisible(true);
            };

            me.totalBuyin = ko.computed(function () {
                var sum = 0;
                for (var i = 0; i < me.players().length; i++) {
                    sum += me.players()[i].buyin();
                }
                return sum;
            });

            me.formattedTotalBuyin = ko.computed(function () {
                return formatCurrency(me.totalBuyin());
            });

            me.totalStacks = ko.computed(function () {
                var sum = 0;
                for (var i = 0; i < me.players().length; i++) {
                    sum += me.players()[i].stack();
                }
                return sum;
            });

            me.startTime = ko.computed(function () {
                var i, first,
                    t = moment().utc(),
                    p = me.players();
                if (p.length === 0)
                    return '';
                for (i = 0; i < p.length; i++) {
                    first = p[i].checkpoints()[0];
                    if (first !== undefined && first.time.isBefore(t)) {
                        t = first.time;
                    }
                }
                return t.format('HH:mm');
            });

            me.formattedTotalStacks = ko.computed(function () {
                return formatCurrency(me.totalStacks());
            });

            me.getPlayerName = function() {
                var i,
                    bp = me.bunchPlayers();
                for (i = 0; i < bp.length; i++) {
                    if (bp[i].id == me.playerId()) {
                        return bp[i].name;
                    }
                }
                return '';
            }

            me.resetPlayerId = function() {
                me.playerId(me.loadedPlayerId);
            }

            function createPlayers(loadedData) {
                var i, j, player, players, checkpoint, checkpoints;
                players = [];
                for (i = 0; i < loadedData.players.length; i++) {
                    player = loadedData.players[i];
                    checkpoints = [];
                    for (j = 0; j < player.checkpoints.length; j++) {
                        checkpoint = player.checkpoints[j];
                        checkpoints.push(new CheckpointViewModel(moment(checkpoint.time), checkpoint.stack, checkpoint.addedMoney));
                    }
                    players.push(new PlayerViewModel(player.id, player.name, player.url, player.hasCashedOut, checkpoints));
                }
                return players;
            }

            function createBunchPlayers(loadedData) {
                var i, player, players;
                players = [];
                for (i = 0; i < loadedData.bunchPlayers.length; i++) {
                    player = loadedData.bunchPlayers[i];
                    players.push(new BunchPlayerViewModel(player.id, player.name));
                }
                return players;
            }
        }

        function PlayerViewModel(id, name, url, hasCashedOut, checkpoints) {
            var me = this;
            me.id = id;
            me.name = name;
            me.url = url;
            me.hasCashedOut = ko.observable(hasCashedOut);
            me.checkpoints = ko.observableArray(checkpoints);

            me.addCheckpoint = function (stack, addedMoney) {
                var checkpoint = { time: moment().utc(), stack: stack, addedMoney: addedMoney };
                me.checkpoints.push(checkpoint);
            }

            me.lastReportTime = ko.computed(function () {
                if (me.checkpoints().length === 0)
                    return moment().fromNow();
                return me.checkpoints()[me.checkpoints().length - 1].time.fromNow();
            });

            me.buyin = ko.computed(function () {
                if (me.checkpoints().length === 0)
                    return 0;
                var sum = 0;
                for (var i = 0; i < me.checkpoints().length; i++) {
                    sum += me.checkpoints()[i].addedMoney;
                }
                return sum;
            });

            me.formattedBuyin = ko.computed(function () {
                return formatCurrency(me.buyin());
            });

            me.stack = ko.computed(function () {
                var c = me.checkpoints();
                if (c.length === 0)
                    return 0;
                return c[c.length - 1].stack;
            });

            me.formattedStack = ko.computed(function () {
                return formatCurrency(me.stack());
            });

            me.winnings = ko.computed(function () {
                return me.stack() - me.buyin();
            });

            me.formattedWinnings = ko.computed(function () {
                return formatResult(me.winnings());
            });

            me.winningsCssClass = ko.computed(function () {
                var winnings = me.winnings();
                if (winnings === 0)
                    return "";
                return winnings > 0 ? "pos-result" : "neg-result";
            });
        }

        function CheckpointViewModel(time, stack, addedMoney) {
            this.time = time;
            this.stack = stack;
            this.addedMoney = addedMoney !== undefined ? addedMoney : 0;
        }

        function BunchPlayerViewModel(id, name) {
            this.id = id;
            this.name = name;
        }

        function loadData(url, callback) {
            $.ajax({
                dataType: 'json',
                url: url,
                success: callback,
                cache: false
                //,error: function () {
                //    alert('load error');
                //}
            });
        }

        function postData(url, data, callback) {
            $.ajax({
                dataType: 'json',
                url: url,
                type: "POST",
                data: data,
                success: callback
                //,error: function () {
                //    alert('post error');
                //}
            });
        }

        return {
            init: init
        };
    }
);
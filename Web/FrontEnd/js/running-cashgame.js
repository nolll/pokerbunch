define(["jquery", "knockout", "moment"],
    function ($, ko, moment) {
        "use strict";

        function init() {
            var url = "/pokerpoker/cashgame/runningjson";
            loadData(url, function(data) {
                ko.applyBindings(new StandingsViewModel(data));
            });
        }

        function formatResult(n) {
            if (n > 0)
                n = "+" + n;
            return formatCurrency(n);
        }

        function formatCurrency(n) {
            return n += " kr";
        }

        function StandingsViewModel(data) {
            var me = this;

            me.players = ko.observableArray(createPlayers(data));
            me.playerId = data.playerId;
            me.playerName = data.playerName;
            me.reportUrl = data.reportUrl;
            me.buyInUrl = data.buyinUrl;
            me.cashOutUrl = data.cashoutUrl;
            me.endGameUrl = data.endGameUrl;
            me.defaultBuyIn = data.defaultBuyin;
            me.hasPlayers = me.players.length > 0;
            me.noPlayers = !me.hasPlayers;
            me.canReport = ko.observable(true);
            me.canBuyIn = ko.observable(true);
            me.canCashOut = ko.observable(true);
            me.areButtonsVisible = ko.observable(true);
            me.reportFormVisible = ko.observable(false);
            me.buyInFormVisible = ko.observable(false);
            me.cashOutFormVisible = ko.observable(false);
            me.endGameFormVisible = ko.observable(false);
            me.currentStack = ko.observable(0);
            me.beforeBuyInStack = ko.observable(0);
            me.buyInAmount = ko.observable(me.defaultBuyIn);

            me.report = function () {
                var reportData = { playerId: me.playerId, stack: parseInt(me.currentStack()) };
                var player = me.getPlayer(me.playerId);
                player.addCheckpoint(reportData.stack, 0);
                me.hideForms();
                postData(me.reportUrl, reportData);
            };

            me.buyIn = function () {
                var buyInData = { playerId: me.playerId, stack: parseInt(me.beforeBuyInStack()), addedMoney: parseInt(me.buyInAmount()) };
                var player = me.getPlayer(me.playerId);
                if (player === null) {
                    player = new PlayerViewModel(me.playerId, me.playerName, false, []);
                    me.players.push(player);
                }
                player.addCheckpoint(buyInData.stack, buyInData.addedMoney);
                me.hideForms();
                postData(me.buyInUrl, buyInData);
            };

            me.cashOut = function () {
                var cashOutData = { playerId: me.playerId, stack: parseInt(me.currentStack()) };
                var player = me.getPlayer(me.playerId);
                player.addCheckpoint(cashOutData.stack, 0);
                player.hasCashedOut(true);
                me.hideForms();
                postData(me.cashOutUrl, cashOutData);
            };

            me.endGame = function () {
                me.hideForms();
                postData(me.endGameUrl);
            };

            me.getPlayer = function (playerId) {
                var i;
                for (i = 0; i < me.players.length; i++) {
                    if (me.players[i].id == playerId) {
                        return me.players[i];
                    }
                }
                return null;
            }

            me.isInGame = ko.computed(function () {
                return me.getPlayer(me.playerId) !== null;
            });

            me.canCashOut = ko.computed(function () {
                return me.isInGame();
            });

            me.canEndGame = ko.computed(function () {
                //var i;
                //for (i = 0; i < me.players.length; i++) {
                //    if (!me.players[i].hasCashedOut) {
                //        return false;
                //    }
                //}
                //return true;
                return false;
            });

            me.canReport = ko.computed(function () {
                return me.isInGame() && !me.canEndGame();
            });

            me.canBuyIn = ko.computed(function () {
                return !me.canEndGame();
            });

            me.addCheckpoint = function (playerId, stack, addedMoney) {
                var i;
                for (i = 0; i < me.players.length; i++) {
                    if (me.players[i].id == playerId) {
                        me.players[i].addCheckpoint({ time: moment().utc(), stack: stack, addedMoney: addedMoney });
                    }
                }
            }

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
                for (var i = 0; i < me.players.length; i++) {
                    sum += me.players[i].buyin();
                }
                return sum;
            });

            me.formattedTotalBuyin = ko.computed(function () {
                return formatCurrency(me.totalBuyin());
            });

            me.totalStacks = ko.computed(function () {
                var sum = 0;
                for (var i = 0; i < me.players.length; i++) {
                    sum += me.players[i].stack();
                }
                return sum;
            });

            me.formattedTotalStacks = ko.computed(function () {
                return formatCurrency(me.totalStacks());
            });

            function createPlayers(loadedData) {
                var i, j, player, players, checkpoint, checkpoints;
                players = [];
                for (i = 0; i < loadedData.players.length; i++) {
                    player = loadedData.players[i];
                    checkpoints = [];
                    for (j = 0; j < player.checkpoints.length; j++) {
                        checkpoint = player.checkpoints[j];
                        checkpoints[checkpoints.length] = new CheckpointViewModel(moment(checkpoint.time), checkpoint.stack, checkpoint.addedMoney);
                    }
                    players.push(new PlayerViewModel(player.id, player.name, player.hasCashedOut, checkpoints));
                }
                return players;
            }
        }

        function PlayerViewModel(id, name, hasCashedOut, checkpoints) {
            var me = this;
            me.id = id;
            me.name = name;
            me.hasCashedOut = ko.observable(hasCashedOut);
            me.checkpoints = ko.observableArray(checkpoints);

            me.addCheckpoint = function (stack, addedMoney) {
                var checkpoint = { time: moment().utc(), stack: stack, addedMoney: addedMoney };
                me.checkpoints.push(checkpoint);
            }

            me.lastReportTime = ko.computed(function () {
                if (checkpoints.length === 0)
                    return moment().fromNow();
                return me.checkpoints()[me.checkpoints().length - 1].time.fromNow();
            });

            me.buyin = ko.computed(function () {
                if (checkpoints.length === 0)
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
                if (checkpoints.length === 0)
                    return 0;
                return me.checkpoints()[me.checkpoints().length - 1].stack;
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

        function loadData(url, callback) {
            $.ajax({
                dataType: 'json',
                url: url,
                success: callback,
                error: function () {
                    alert('load error');
                }
            });
        }

        function postData(url, data, callback) {
            $.ajax({
                dataType: 'json',
                url: url,
                type: "POST",
                data: data,
                success: callback,
                error: function () {
                    alert('post error');
                }
            });
        }

        return {
            init: init
        };
    }
);
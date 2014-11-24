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
            
            me.players = getPlayers(data);
            me.playerId = data.playerId;
            me.reportUrl = data.reportUrl;
            me.hasPlayers = me.players.length > 0;
            me.noPlayers = !me.hasPlayers;
            me.canReport = ko.observable(true);
            me.canBuyIn = ko.observable(true);
            me.canCashOut = ko.observable(true);
            me.canEndGame = ko.observable(false);
            me.areButtonsVisible = ko.observable(true);
            me.reportFormVisible = ko.observable(false);
            me.buyInFormVisible = ko.observable(false);
            me.cashOutFormVisible = ko.observable(false);
            me.endGameFormVisible = ko.observable(false);
            me.reportStack = ko.observable(0);

            this.report = function () {
                var reportUrl = "/pokerpoker/cashgame/report";
                var reportData = { playerId: this.playerId, stack: parseInt(me.reportStack()) };
                postData(reportUrl, reportData);
                this.addCheckpoint(this.playerId, reportData.stack, 0);
                this.hideForms();
            };

            this.addCheckpoint = function(playerId, stack, addedMoney) {
                var i;
                for (i = 0; i < this.players.length; i++) {
                    if (this.players[i].id == playerId) {
                        this.players[i].addCheckpoint({ time: moment().utc(), stack: stack, addedMoney: addedMoney });
                    }
                }
            }

            this.buyIn = function () {
                alert("buy in");
            };

            this.cashOut = function () {
                alert("cash out");
            };

            this.endGame = function () {
                alert("end game");
            };

            this.showReportForm = function () {
                this.reportFormVisible(true);
                this.hideButtons();
            };

            this.showBuyInForm = function () {
                this.buyInFormVisible(true);
                this.hideButtons();
            };

            this.showCashOutForm = function () {
                this.cashOutFormVisible(true);
                this.hideButtons();
            };

            this.showEndGameForm = function () {
                this.endGameFormVisible(true);
                this.hideButtons();
            };

            this.hideButtons = function() {
                this.areButtonsVisible(false);
            }

            this.hideForms = function() {
                this.reportFormVisible(false);
                this.buyInFormVisible(false);
                this.cashOutFormVisible(false);
                this.endGameFormVisible(false);
                this.areButtonsVisible(true);
            };

            this.totalBuyin = ko.computed(function () {
                var sum = 0;
                for (var i = 0; i < me.players.length; i++) {
                    sum += me.players[i].buyin();
                }
                return sum;
            }, this);

            this.formattedTotalBuyin = ko.computed(function () {
                return formatCurrency(me.totalBuyin());
            }, this);

            this.totalStacks = ko.computed(function () {
                var sum = 0;
                for (var i = 0; i < me.players.length; i++) {
                    sum += me.players[i].stack();
                }
                return sum;
            }, this);

            this.formattedTotalStacks = ko.computed(function () {
                return formatCurrency(me.totalStacks());
            }, this);

            function getPlayers(loadedData) {
                var i, j, player, players, checkpoint, checkpoints;
                players = [];
                for (i = 0; i < loadedData.players.length; i++) {
                    player = loadedData.players[i];
                    checkpoints = [];
                    for (j = 0; j < player.checkpoints.length; j++) {
                        checkpoint = player.checkpoints[j];
                        checkpoints[checkpoints.length] = new CheckpointViewModel(moment(checkpoint.time), checkpoint.stack, checkpoint.addedMoney);
                    }
                    players[i] = new PlayerViewModel(player.id, player.name, player.hasCashedOut, checkpoints);;
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

            this.addCheckpoint = function(checkpoint) {
                this.checkpoints.push(checkpoint);
            }

            this.lastReportTime = ko.computed(function () {
                return me.checkpoints()[me.checkpoints().length - 1].time.fromNow();
            }, this);

            this.buyin = ko.computed(function () {
                var sum = 0;
                for (var i = 0; i < me.checkpoints().length; i++) {
                    sum += me.checkpoints()[i].addedMoney;
                }
                return sum;
            }, this);

            this.formattedBuyin = ko.computed(function () {
                return formatCurrency(me.buyin());
            }, this);

            this.stack = ko.computed(function () {
                return me.checkpoints()[me.checkpoints().length - 1].stack;
            }, this);

            this.formattedStack = ko.computed(function () {
                return formatCurrency(me.stack());
            }, this);

            this.winnings = ko.computed(function () {
                return me.stack() - me.buyin();
            }, this);

            this.formattedWinnings = ko.computed(function () {
                return formatResult(me.winnings());
            }, this);

            this.winningsCssClass = ko.computed(function () {
                var winnings = me.winnings();
                if (winnings === 0)
                    return "";
                return winnings > 0 ? "pos-result" : "neg-result";
            }, this);
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
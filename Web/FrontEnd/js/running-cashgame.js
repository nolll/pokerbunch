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
                    players[i] = new PlayerViewModel(player.name, player.hasCashedOut, checkpoints);;
                }
                return players;
            }
        }

        function PlayerViewModel(name, hasCashedOut, checkpoints) {
            var me = this;
            me.name = name;
            me.hasCashedOut = hasCashedOut;
            me.checkpoints = checkpoints;

            this.lastReportTime = ko.computed(function () {
                return me.checkpoints[me.checkpoints.length - 1].time.fromNow();
            }, this);

            this.buyin = ko.computed(function () {
                var sum = 0;
                for (var i = 0; i < me.checkpoints.length; i++) {
                    sum += me.checkpoints[i].addedMoney;
                }
                return sum;
            }, this);

            this.formattedBuyin = ko.computed(function () {
                return formatCurrency(me.buyin());
            }, this);

            this.stack = ko.computed(function () {
                return me.checkpoints[me.checkpoints.length - 1].stack;
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
                    alert('ajax error');
                }
            });
        }

        return {
            init: init
        };
    }
);
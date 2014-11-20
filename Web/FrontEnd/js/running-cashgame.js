define(["knockout", "moment"],
    function (ko, moment) {
        "use strict";

        function init() {
            ko.applyBindings(new StandingsViewModel());
        }

        function StandingsViewModel() {
            var me = this;
            me.players = [
                new PlayerViewModel('Henrik S', [new CheckpointViewModel(moment().utc().add(-130, "s"), 200, 200), new CheckpointViewModel(moment().utc().add(-30, "s"), 299)]),
                new PlayerViewModel('Mikael K', [new CheckpointViewModel(moment().utc().add(-330, "s"), 200, 200), new CheckpointViewModel(moment().utc().add(-235, "s"), 101)])
            ];

            this.totalBuyin = ko.computed(function () {
                var sum = 0;
                for (var i = 0; i < me.players.length; i++) {
                    sum += me.players[i].buyin();
                }
                return sum;
            }, this);

            this.totalStacks = ko.computed(function () {
                var sum = 0;
                for (var i = 0; i < me.players.length; i++) {
                    sum += me.players[i].stack();
                }
                return sum;
            }, this);
        }

        function PlayerViewModel(name, checkpoints) {
            var me = this;
            me.name = name;
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

            this.stack = ko.computed(function () {
                return me.checkpoints[me.checkpoints.length - 1].stack;
            }, this);

            this.winnings = ko.computed(function () {
                return me.stack() - me.buyin();
            }, this);

            this.formattedWinnings = ko.computed(function () {
                var winnings = me.winnings();
                if (winnings > 0)
                    winnings = "+" + winnings;
                winnings += " kr";
                return winnings;
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

        return {
            init: init
        };
    }
);
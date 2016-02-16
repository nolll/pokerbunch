define(["vue", "standings", "jquery", "moment"],
    function(vue, standings, $, moment) {
        "use strict";

        var el, vm;

        function init(data) {
            el = this;
            var url = data.url;
            loadData(url, initVue);
        }

        function initVue(data) {
            var preparedData = prepareData(data);
            vue.config.debug = true;
            vm = new standings({
                el: el,
                data: preparedData
            });
        }

        function prepareData(data) {
            data.areButtonsVisible = true;
            data.reportFormVisible = false;
            data.buyInFormVisible = false;
            data.cashOutFormVisible = false;
            data.endGameFormVisible = false;
            data.currentStack = 0;
            data.beforeBuyInStack = 0;
            data.buyInAmount = data.defaultBuyin;
            data.loadedPlayerId = data.playerId;
            data.currencyFormat = '{0} kr';
            data.players = preparePlayers(data.players);
            return data;
        }

        function preparePlayers(players) {
            var i, j, player, checkpoint, checkpoints;
            for (i = 0; i < players.length; i++) {
                player = players[i];
                checkpoints = [];
                for (j = 0; j < player.checkpoints.length; j++) {
                    checkpoint = player.checkpoints[j];
                    checkpoints.push(prepareCheckpoint(checkpoint));
                }
                player.checkpoints = checkpoints;
                player.prototype = Player.prototype;
            }
            return players;
        }

        function prepareCheckpoint(checkpoint) {
            return {
                time: moment(checkpoint.time),
                stack: checkpoint.stack,
                addedMoney: checkpoint.addedMoney !== undefined ? checkpoint.addedMoney : 0
            }
        }

        function loadData(url, callback) {
            $.ajax({
                dataType: 'json',
                url: url,
                success: callback,
                cache: false
            });
        }

        function Player() {
            this.prototype.addCheckpoint = function (stack, addedMoney) {
                var checkpoint = { time: moment().utc(), stack: stack, addedMoney: addedMoney };
                this.checkpoints.push(checkpoint);
            }

            this.prototype.lastReportTime = function () {
                if (this.checkpoints.length === 0)
                    return moment().fromNow();
                return this.checkpoints[this.checkpoints.length - 1].time.fromNow();
            };

            this.prototype.buyin = function () {
                if (this.checkpoints.length === 0)
                    return 0;
                var sum = 0;
                for (var i = 0; i < this.checkpoints.length; i++) {
                    sum += this.checkpoints[i].addedMoney;
                }
                return sum;
            };

            this.prototype.formattedBuyin = function () {
                return formatCurrency(this.buyin);
            };

            this.prototype.stack = function () {
                var c = this.checkpoints();
                if (c.length === 0)
                    return 0;
                return c[c.length - 1].stack;
            };

            this.prototype.formattedStack = function () {
                return formatCurrency(this.stack);
            };

            this.prototype.winnings = function () {
                return this.stack - this.buyin;
            };

            this.prototype.formattedWinnings = function () {
                return formatResult(this.winnings);
            };

            this.prototype.winningsCssClass = function () {
                var winnings = this.winnings;
                if (winnings === 0)
                    return "";
                return winnings > 0 ? "pos-result" : "neg-result";
            };
        }
        
        return {
            init: init
        };

        function formatResult(n) {
            if (n > 0)
                n = "+" + n;
            return formatCurrency(n);
        }

        function formatCurrency(n) {
            return n + " kr";
        }
    }
);
define(["vue", "text!player-row.html", "moment"],
    function(vue, html, moment) {
        "use strict";

        return vue.extend({
            template: html,
            props: ['player', 'currencyFormat'],
            computed: {
                lastReportTime: function () {
                    if (this.player.checkpoints.length === 0)
                        return moment().fromNow();
                    return this.player.checkpoints[this.player.checkpoints.length - 1].time.fromNow();
                },
                buyin: function () {
                    if (this.player.checkpoints.length === 0)
                        return 0;
                    var sum = 0;
                    for (var i = 0; i < this.player.checkpoints.length; i++) {
                        sum += this.player.checkpoints[i].addedMoney;
                    }
                    return sum;
                },
                formattedBuyin: function () {
                    //return formatCurrency(this.buyin);
                    return this.buyin;
                },
                stack: function () {
                    var c = this.player.checkpoints;
                    if (c.length === 0)
                        return 0;
                    return c[c.length - 1].stack;
                },
                formattedStack: function () {
                    //return formatCurrency(this.stack);
                    return this.stack;
                },
                winnings: function () {
                    return this.stack - this.buyin;
                },
                formattedWinnings: function () {
                    //return formatResult(this.winnings);
                    return this.winnings;
                },
                winningsCssClass: function () {
                    var winnings = this.winnings;
                    if (winnings === 0)
                        return "";
                    return winnings > 0 ? "pos-result" : "neg-result";
                }
            }
        });
    }
);
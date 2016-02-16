define(["vue", "text!components/player-table.html", "moment"],
    function(vue, html, moment) {
        "use strict";

        return vue.extend({
            template: html,
            props: ['players', 'currencyFormat'],
            computed: {
                totalBuyin: function () {
                    var sum = 0;
                    for (var i = 0; i < this.players.length; i++) {
                        var buyin = 0;
                        var player = this.players[i];
                        if (player.checkpoints.length === 0)
                            continue;
                        
                        for (var j = 0; j < player.checkpoints.length; j++) {
                            buyin += player.checkpoints[j].addedMoney;
                        }
                        sum += buyin;
                    }
                    return sum;
                },
                formattedTotalBuyin: function () {
                    //return formatCurrency(this.totalBuyin);
                    return this.totalBuyin;
                },
                totalStacks: function () {
                    var sum = 0;
                    for (var i = 0; i < this.players.length; i++) {
                        var c = this.players[i].checkpoints;
                        sum += c.length > 0 ? c[c.length - 1].stack : 0;
                    }
                    return sum;
                },
                startTime: function () {
                    var i, first,
                        t = moment().utc(),
                        p = this.players;
                    if (p.length === 0)
                        return '';
                    for (i = 0; i < p.length; i++) {
                        first = p[i].checkpoints[0];
                        if (first !== undefined && first.time.isBefore(t)) {
                            t = first.time;
                        }
                    }
                    return t.format('HH:mm');
                },
                formattedTotalStacks: function () {
                    //return formatCurrency(this.totalStacks);
                    return this.totalStacks;
                }
            }
        });
    }
);
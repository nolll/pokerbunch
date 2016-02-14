define(["vue", "moment", "text!standings.html"],
    function(vue, moment, html) {
        "use strict";

        return vue.extend({
            template: html,
            computed: {
                hasPlayers: function () {
                    return this.players.length > 0;
                },
                noPlayers: function() {
                    return this.players.length === 0;
                },
                startTime: function () {
                    var t = moment().utc();
                    return t.format('HH:mm');
                },
                areButtonsVisible: function() {
                    return true;
                },
                sortedPlayers: function() {
                    return this.players.sort(function (left, right) {
                        return right.winnings - left.winnings;
                    });
                },
                isInGame: function () {
                    return this.getPlayer(this.playerId) !== null;
                },
                canCashOut: function () {
                    return this.isInGame;
                },
                hasCashedOut: function() {
                    var player = this.getPlayer(this.playerId);
                    if (!player)
                        return false;
                    return player.hasCashedOut;
                },
                canEndGame: function () {
                    var i;
                    if (this.players.length === 0)
                        return false;
                    for (i = 0; i < this.players.length; i++) {
                        if (!this.players[i].hasCashedOut) {

                            return false;
                        }
                    }
                    return true;
                },
                canReport: function () {
                    return this.isInGame && !this.hasCashedOut;
                }
            },
            methods: {
                getPlayer: function (playerId) {
                    var i;
                    for (i = 0; i < this.players.length; i++) {
                        if (this.players[i].id === playerId) {
                            return this.players[i];
                        }
                    }
                    return null;
                },
                hasCashedOut: function() {
                    var player = this.getPlayer(this.playerId);
                    if (!player)
                        return false;
                    return player.hasCashedOut();
                }
            },
            ready: function () {
                var x = 0;
            }
        });
    }
);
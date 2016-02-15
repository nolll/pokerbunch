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
                },
                canBuyIn: function () {
                    return !this.hasCashedOut;
                }
            },
            methods: {
                showReportForm: function () {
                    //refresh(me.setPlayers);
                    this.reportFormVisible = true;
                    this.hideButtons();
                },
                showBuyInForm: function () {
                    //refresh(me.setPlayers);
                    this.buyInFormVisible = true;
                    this.hideButtons();
                },
                showCashOutForm: function () {
                    //refresh(me.setPlayers);
                    this.cashOutFormVisible = true;
                    this.hideButtons();
                },
                showEndGameForm: function () {
                    //refresh(me.setPlayers);
                    this.endGameFormVisible = true;
                    this.hideButtons();
                },
                hideButtons: function() {
                    this.areButtonsVisible = false;
                },
                hideForms: function () {
                    this.reportFormVisible = false;
                    this.buyInFormVisible = false;
                    this.cashOutFormVisible = false;
                    this.endGameFormVisible = false;
                    this.areButtonsVisible = true;
                },
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
            events: {
                'change-player': function (playerId) {
                    this.playerId = playerId;
                }
            },
            ready: function () {
                var x = 0;
            }
        });
    }
);
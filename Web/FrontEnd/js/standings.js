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
                    return this.$data.players.sort(function (left, right) {
                        return right.winnings() - left.winnings();
                    });
                }
            },
            ready: function () {
                var x = 0;
            }
        });
    }
);
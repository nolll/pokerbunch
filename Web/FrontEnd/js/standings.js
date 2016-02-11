define(["vue", "moment"],
    function(vue, moment) {
        "use strict";

        return vue.extend({
            template: '#running-cashgame-template-vue',
            computed: {
                hasPlayers: function () {
                    return this.players.length > 0;
                },
                startTime: function () {
                    var t = moment().utc();
                    return t.format('HH:mm');
                }
            },
            ready: function () {
                var x = 0;
            }
        });
    }
);
define(["vue", "text!components/player-table/player-table.html", "game-service"],
    function(vue, html, gameService) {
        "use strict";

        return vue.component("player-table", {
            template: html,
            props: ['players', 'currencyFormat'],
            created: function() {
                var x = 0;
            },
            computed: {
                totalBuyin: function () {
                    return gameService.getTotalBuyin(this.players);
                },
                totalStacks: function () {
                    return gameService.getTotalStacks(this.players);
                }
            }
        });
    }
);
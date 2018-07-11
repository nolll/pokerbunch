define(["vue", "text!components/player-table/player-table.html", "game-service"],
    function(vue, html, gameService) {
        "use strict";

        return vue.component("player-table", {
            template: html,
            props: ['players', 'currencyFormat'],
            created: function() {
            },
            computed: {
                totalBuyin: function () {
                    return gameService.getTotalBuyin(this.players);
                },
                totalStacks: function () {
                    return gameService.getTotalStacks(this.players);
                },
                formattedTotalBuyin: function() {
                    return this.formatCurrency(this.totalBuyin);
                },
                formattedTotalStacks: function () {
                    return this.formatCurrency(this.totalStacks);
                }
            },
            methods: {
                formatCurrency: function(amount) {
                    return this.$options.filters.customCurrency(amount);
                }
            }
        });
    }
);
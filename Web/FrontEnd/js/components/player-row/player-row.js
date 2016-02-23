define(["vue", "text!components/player-row/player-row.html", "game-service"],
    function(vue, html, gameService) {
        "use strict";

        return vue.component("player-row", {
            template: html,
            props: ['player', 'currencyFormat'],
            computed: {
                hasCashedOut: function() {
                    return this.player.hasCashedOut;
                },
                lastReportTime: function () {
                    return gameService.getLastReportTime(this.player);
                },
                buyin: function () {
                    return gameService.getBuyin(this.player);
                },
                stack: function () {
                    return gameService.getStack(this.player);
                },
                winnings: function () {
                    return gameService.getWinnings(this.player);
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
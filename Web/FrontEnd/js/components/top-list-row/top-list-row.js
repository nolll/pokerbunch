define(["vue", "text!components/top-list-row/top-list-row.html"],
    function(vue, html) {
        "use strict";

        return vue.component("top-list-row", {
            template: html,
            props: ["player", "currencyFormat", "thousandSeparator"],
            created: function () {
                var x = 0;
            },
            computed: {
                winningsCssClass: function () {
                    var winnings = this.player.winnings;
                    if (winnings === 0)
                        return "";
                    return winnings > 0 ? "pos-result" : "neg-result";
                }
            }
        });
    }
);
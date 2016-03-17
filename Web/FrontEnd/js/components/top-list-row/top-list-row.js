define(["vue", "text!components/top-list-row/top-list-row.html"],
    function(vue, html) {
        "use strict";

        return vue.component("top-list-row", {
            template: html,
            props: ["player", "orderBy", "currencyFormat", "thousandSeparator"],
            created: function () {
                var x = 0;
            },
            computed: {
                winningsCssClass: function () {
                    var winnings = this.player.winnings;
                    if (winnings === 0)
                        return "";
                    return winnings > 0 ? "pos-result" : "neg-result";
                },
                winningsSortCssClass: function () {
                    return getSortCssClass(this.orderBy, "winnings");
                },
                buyinSortCssClass: function () {
                    return getSortCssClass(this.orderBy, "buyin");
                },
                cashoutSortCssClass: function () {
                    return getSortCssClass(this.orderBy, "cashout");
                },
                timeSortCssClass: function () {
                    return getSortCssClass(this.orderBy, "time");
                },
                gameCountSortCssClass: function () {
                    return getSortCssClass(this.orderBy, "gamecount");
                },
                winRateSortCssClass: function () {
                    return getSortCssClass(this.orderBy, "winrate");
                }
            }
        });

        function getSortCssClass(orderBy, query) {
            if (orderBy === query)
                return "table-list--sortable__sort-item";
            return "";
        }
    }
);
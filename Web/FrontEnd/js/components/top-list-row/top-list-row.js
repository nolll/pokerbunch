define(["vue", "./top-list-row.html"],
    function(vue, html) {
        "use strict";

        return {
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
                },
                formattedWinnings: function () {
                    return this.formatResult(this.player.winnings);
                },
                formattedBuyin: function () {
                    return this.formatCurrency(this.player.buyin);
                },
                formattedCashout: function () {
                    return this.formatCurrency(this.player.cashout);
                },
                formattedWinRate: function () {
                    return this.formatWinRate(this.player.winRate);
                },
                formattedTime: function() {
                    return this.formatTime(this.player.time);
                }
            },
            methods: {
                formatResult: function (result) {
                    return this.$options.filters.result(result, this.currencyFormat, this.thousandSeparator);
                },
                formatCurrency: function(amount) {
                    return this.$options.filters.customCurrency(amount, this.currencyFormat, this.thousandSeparator);
                },
                formatWinRate: function(winRate) {
                    return this.$options.filters.winrate(winRate, this.currencyFormat, this.thousandSeparator);
                },
                formatTime: function(time) {
                    return this.$options.filters.time(time);
                }
            }
        };

        function getSortCssClass(orderBy, query) {
            if (orderBy === query)
                return "table-list--sortable__sort-item";
            return "";
        }
    }
);
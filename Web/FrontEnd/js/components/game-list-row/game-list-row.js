define(["vue", "text!components/game-list-row/game-list-row.html", "moment"],
    function(vue, html, moment) {
        "use strict";

        return vue.component("game-list-row", {
            template: html,
            props: ["game", "orderBy", "currencyFormat", "thousandSeparator"],
            created: function () {
                var x = 0;
            },
            computed: {
                displayDate: function() {
                    return moment(this.game.date).format("MMM D");
                },
                dateSortCssClass: function () {
                    return getSortCssClass(this.orderBy, "date");
                },
                playerCountSortCssClass: function () {
                    return getSortCssClass(this.orderBy, "playercount");
                },
                durationSortCssClass: function () {
                    return getSortCssClass(this.orderBy, "duration");
                },
                turnoverSortCssClass: function () {
                    return getSortCssClass(this.orderBy, "turnover");
                },
                averageBuyinSortCssClass: function () {
                    return getSortCssClass(this.orderBy, "averagebuyin");
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
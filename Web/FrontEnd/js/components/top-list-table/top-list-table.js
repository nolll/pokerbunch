define(["vue", "text!components/top-list-table/top-list-table.html"],
    function(vue, html) {
        "use strict";

        return vue.component("top-list-table", {
            template: html,
            props: ["jsonContainer"],
            created: function () {
                var x = 0;
            },
            data: function () {
                var jsonElement = document.getElementById(this.jsonContainer);
                return JSON.parse(jsonElement.innerHTML);
            },
            computed: {
                sortedPlayers: function() {
                    return sortPlayers(this.players, this.orderBy);
                }
            },
            events: {
                'sort-by': function (orderBy) {
                    this.orderBy = orderBy;
                }
            }
        });

        function sortPlayers(players, orderBy) {
            return players.sort(getCompareFunc(orderBy)).reverse();
        }

        function getCompareFunc(orderBy) {
            if (orderBy === "buyin")
                return compareBuyin;
            if (orderBy === "cashout")
                return compareCashout;
            if (orderBy === "time")
                return compareTime;
            if (orderBy === "gamecount")
                return compareGameCount;
            if (orderBy === "winrate")
                return compareWinRate;
            return compareWinnings;
        }

        function compareWinnings(a, b) {
            return compareValues(a.winnings, b.winnings);
        }

        function compareBuyin(a, b) {
            return compareValues(a.buyin, b.buyin);
        }

        function compareCashout(a, b) {
            return compareValues(a.cashout, b.cashout);
        }

        function compareTime(a, b) {
            return compareValues(a.time, b.time);
        }

        function compareGameCount(a, b) {
            return compareValues(a.gameCount, b.gameCount);
        }

        function compareWinRate(a, b) {
            return compareValues(a.winRate, b.winRate);
        }

        function compareValues(a, b) {
            if (a < b)
                return -1;
            else if (a > b)
                return 1;
            else
                return 0;
        }
    }
);
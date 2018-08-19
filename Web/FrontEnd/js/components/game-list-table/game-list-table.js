define(["vue", "./game-list-table.html"],
    function(vue, html) {
        "use strict";

        return {
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
                sortedGames: function() {
                    return sortGames(this.games, this.orderBy);
                }
            },
            events: {
                'sort-by': function (orderBy) {
                    this.orderBy = orderBy;
                }
            }
        };

        function sortGames(games, orderBy) {
            return games.sort(getCompareFunc(orderBy)).reverse();
        }

        function getCompareFunc(orderBy) {
            if (orderBy === "playercount")
                return comparePlayerCount;
            if (orderBy === "duration")
                return compareDuration;
            if (orderBy === "turnover")
                return compareTurnover;
            if (orderBy === "averagebuyin")
                return compareAverageBuyin;
            return compareDate;
        }

        function compareDate(a, b) {
            return compareValues(a.date, b.date);
        }

        function comparePlayerCount(a, b) {
            return compareValues(a.playerCount, b.playerCount);
        }

        function compareDuration(a, b) {
            return compareValues(a.duration, b.duration);
        }

        function compareTurnover(a, b) {
            return compareValues(a.turnover, b.turnover);
        }

        function compareAverageBuyin(a, b) {
            return compareValues(a.averageBuyin, b.averageBuyin);
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
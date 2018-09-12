<template>
    <div>
        <div class="table-list--sortable__sort-order-selector">
            <label for="gamelist-sortorder">Select Data:</label>
            <select id="gamelist-sortorder" v-model="orderBy">
                <option value="date">Date</option>
                <option value="playercount">Players</option>
                <option value="duration">Duration</option>
                <option value="turnover">Turnover</option>
                <option value="averagebuyin">Average Buyin</option>
            </select>
        </div>
        <table class="table-list table-list--sortable">
            <thead>
                <tr>
                    <th is="game-list-column" name="date" title="Date" v-bind:order-by="orderBy" v-bind:is-default="true"></th>
                    <th is="game-list-column" name="playercount" title="Players" v-bind:order-by="orderBy"></th>
                    <th class="table-list__column-header"><span class="table-list__column-header__content">Location</span></th>
                    <th is="game-list-column" name="duration" title="Duration" v-bind:order-by="orderBy"></th>
                    <th is="game-list-column" name="turnover" title="Turnover" v-bind:order-by="orderBy"></th>
                    <th is="game-list-column" name="averagebuyin" title="Average Buyin" v-bind:order-by="orderBy"></th>
                </tr>
            </thead>
            <tbody class="list">
                <tr is="game-list-row" v-for="game in sortedGames" v-bind:game="game" v-bind:order-by="orderBy" v-bind:currency-format="currencyFormat" v-bind:thousand-separator="thousandSeparator"></tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    import { GameListColumn, GameListRow } from ".";

    export default {
        components: {
            GameListColumn,
            GameListRow
        },
        props: ['jsonContainer'],
        created: function () {
            var x = 0;
        },
        data: function () {
            var jsonElement = document.getElementById(this.jsonContainer);
            return JSON.parse(jsonElement.innerHTML);
        },
        computed: {
            sortedGames: function () {
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
        if (orderBy === 'playercount')
            return comparePlayerCount;
        if (orderBy === 'duration')
            return compareDuration;
        if (orderBy === 'turnover')
            return compareTurnover;
        if (orderBy === 'averagebuyin')
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
</script>

<style scoped>

</style>

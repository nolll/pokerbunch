<template>
    <div>
        <div class="table-list--sortable__sort-order-selector">
            <label for="toplist-sortorder">Select Data:</label>
            <select id="toplist-sortorder" v-model="orderBy">
                <option value="winnings">Winnings</option>
                <option value="buyin">Buyin</option>
                <option value="cashout">Cashout</option>
                <option value="time">Time</option>
                <option value="gamecount">Games</option>
                <option value="winrate">Win rate</option>
            </select>
        </div>
        <table class="table-list table-list--sortable">
            <thead>
                <tr>
                    <th class="table-list__column-header table-list--sortable__base-column"></th>
                    <th class="table-list__column-header table-list--sortable__base-column"><span class="table-list__column-header__content">Player</span></th>
                    <th is="top-list-column" name="winnings" title="Winnings" v-bind:order-by="orderBy"></th>
                    <th is="top-list-column" name="buyin" title="Buyin" v-bind:order-by="orderBy"></th>
                    <th is="top-list-column" name="cashout" title="Cashout" v-bind:order-by="orderBy"></th>
                    <th is="top-list-column" name="time" title="Time" v-bind:order-by="orderBy"></th>
                    <th is="top-list-column" name="gamecount" title="Games" v-bind:order-by="orderBy"></th>
                    <th is="top-list-column" name="winrate" title="Win rate" v-bind:order-by="orderBy"></th>
                </tr>
            </thead>
            <tbody class="list">
                <tr is="top-list-row" v-for="player in sortedPlayers" v-bind:player="player" v-bind:order-by="orderBy" v-bind:currency-format="currencyFormat" v-bind:thousand-separator="thousandSeparator"></tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    import { TopListColumn, TopListRow } from ".";

    export default {
        components: {
            TopListColumn,
            TopListRow
        },
        props: ["jsonContainer"],
        created: function () {
        },
        data: function () {
            var jsonElement = document.getElementById(this.jsonContainer);
            return JSON.parse(jsonElement.innerHTML);
        },
        computed: {
            sortedPlayers: function () {
                return sortPlayers(this.players, this.orderBy);
            }
        },
        events: {
            'sort-by': function (orderBy) {
                this.orderBy = orderBy;
            }
        }
    };

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
</script>

<style scoped>

</style>

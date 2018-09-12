<template>
    <tr class="table-list__row">
        <td v-bind:class="'table-list__cell table-list--sortable__base-column ' + dateSortCssClass">
            <a v-bind:href="game.url">{{displayDate}}</a>
        </td>
        <td v-bind:class="'table-list__cell table-list__cell--numeric ' + playerCountSortCssClass">{{game.playerCount}}</td>
        <td class="table-list__cell">{{game.location}}</td>
        <td v-bind:class="'table-list__cell ' + durationSortCssClass" v-text="duration"></td>
        <td v-bind:class="'table-list__cell table-list__cell--numeric ' + turnoverSortCssClass" v-text="formattedTurnover"></td>
        <td v-bind:class="'table-list__cell table-list__cell--numeric ' + averageBuyinSortCssClass" v-text="formattedAverageBuyin"></td>
    </tr>
</template>

<script>
    import moment from 'moment';

    export default {
        props: ['game', 'orderBy', 'currencyFormat', 'thousandSeparator'],
        created: function () {
            var x = 0;
        },
        computed: {
            displayDate: function () {
                return moment(this.game.date).format('MMM D');
            },
            dateSortCssClass: function () {
                return getSortCssClass(this.orderBy, 'date');
            },
            playerCountSortCssClass: function () {
                return getSortCssClass(this.orderBy, 'playercount');
            },
            durationSortCssClass: function () {
                return getSortCssClass(this.orderBy, 'duration');
            },
            turnoverSortCssClass: function () {
                return getSortCssClass(this.orderBy, 'turnover');
            },
            averageBuyinSortCssClass: function () {
                return getSortCssClass(this.orderBy, 'averagebuyin');
            },
            duration: function () {
                return this.$options.filters.time(this.game.duration);
            },
            formattedAverageBuyin: function () {
                return this.formatCurrency(this.game.averageBuyin);
            },
            formattedTurnover: function () {
                return this.formatCurrency(this.game.turnover);
            }
        },
        methods: {
            formatCurrency: function (amount) {
                return this.$options.filters.customCurrency(amount);
            }
        }
    };

    function getSortCssClass(orderBy, query) {
        if (orderBy === query)
            return 'table-list--sortable__sort-item';
        return '';
    }
</script>

<style scoped>

</style>

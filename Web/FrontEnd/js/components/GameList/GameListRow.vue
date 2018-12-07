<template>
    <tr class="table-list__row">
        <td :class="'table-list__cell table-list--sortable__base-column ' + dateSortCssClass">
            <a :href="url">{{displayDate}}</a>
        </td>
        <td :class="'table-list__cell table-list__cell--numeric ' + playerCountSortCssClass">{{game.playerCount}}</td>
        <td class="table-list__cell">{{game.location.name}}</td>
        <td :class="'table-list__cell ' + durationSortCssClass">{{duration}}</td>
        <td :class="'table-list__cell table-list__cell--numeric ' + turnoverSortCssClass">{{formattedTurnover}}</td>
        <td :class="'table-list__cell table-list__cell--numeric ' + averageBuyinSortCssClass">{{formattedAverageBuyin}}</td>
    </tr>
</template>

<script>
    import { mapState } from 'vuex';
    import moment from 'moment';
    import { FormatMixin } from '../../mixins'

    export default {
        mixins: [
            FormatMixin
        ],
        props: ['game'],
        created: function () {
            var x = 0;
        },
        computed: {
            ...mapState('gameArchive', {
                gameSortOrder: state => state.gameSortOrder
            }),
            url: function () {
                return `/cashgame/details/${this.game.id}`;
            },
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
                return this.formatTime(this.game.duration);
            },
            formattedAverageBuyin: function () {
                return this.formatCurrency(this.game.averageBuyin);
            },
            formattedTurnover: function () {
                return this.formatCurrency(this.game.turnover);
            }
        }
    };

    function getSortCssClass(orderBy, query) {
        if (orderBy === query)
            return 'table-list--sortable__sort-item';
        return '';
    }
</script>

<style>

</style>

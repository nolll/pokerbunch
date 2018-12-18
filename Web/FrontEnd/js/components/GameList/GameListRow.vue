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
    import { FormatMixin } from '@/mixins';
    import urls from '@/urls';
    import { GAME_ARCHIVE } from '@/store-names';

    export default {
        mixins: [
            FormatMixin
        ],
        props: ['game'],
        computed: {
            ...mapState(GAME_ARCHIVE, [
                'gameSortOrder'
            ]),
            url() {
                urls.cashgameDetails(this.game.id);
            },
            displayDate() {
                return moment(this.game.date).format('MMM D');
            },
            dateSortCssClass() {
                return getSortCssClass(this.orderBy, 'date');
            },
            playerCountSortCssClass() {
                return getSortCssClass(this.orderBy, 'playercount');
            },
            durationSortCssClass() {
                return getSortCssClass(this.orderBy, 'duration');
            },
            turnoverSortCssClass() {
                return getSortCssClass(this.orderBy, 'turnover');
            },
            averageBuyinSortCssClass() {
                return getSortCssClass(this.orderBy, 'averagebuyin');
            },
            duration() {
                return this.formatTime(this.game.duration);
            },
            formattedAverageBuyin() {
                return this.formatCurrency(this.game.averageBuyin);
            },
            formattedTurnover() {
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

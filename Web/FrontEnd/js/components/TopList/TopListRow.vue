<template>
    <tr class="table-list__row">
        <td class="table-list__cell table-list__cell--numeric table-list--sortable__base-column">{{player.rank}}.</td>
        <td class="table-list__cell table-list--sortable__base-column">
            <a :href="url">{{player.name}}</a>
        </td>
        <td :class="'table-list__cell table-list__cell--numeric ' + winningsCssClass + ' ' + winningsSortCssClass">{{formattedWinnings}}</td>
        <td :class="'table-list__cell table-list__cell--numeric ' + buyinSortCssClass">{{formattedBuyin}}</td>
        <td :class="'table-list__cell table-list__cell--numeric ' + cashoutSortCssClass">{{formattedCashout}}</td>
        <td :class="'table-list__cell ' + timeSortCssClass">{{formattedTime}}</td>
        <td :class="'table-list__cell table-list__cell--numeric ' + gameCountSortCssClass">{{player.gameCount}}</td>
        <td :class="'table-list__cell table-list__cell--numeric ' + winrateSortCssClass">{{formattedWinrate}}</td>
    </tr>
</template>

<script>
    import { mapState } from 'vuex';
    import { FormatMixin } from '../../mixins'

    export default {
        mixins: [
            FormatMixin
        ],
        props: ['player'],
        created: function () {
            var x = 0;
        },
        computed: {
            ...mapState('gameArchive', {
                playerSortOrder: state => state.playerSortOrder
            }),
            url: function () {
                return `/player/details/${this.player.id}`;
            },
            winningsCssClass: function () {
                var winnings = this.player.winnings;
                if (winnings === 0)
                    return '';
                return winnings > 0 ? 'pos-result' : 'neg-result';
            },
            winningsSortCssClass: function () {
                return getSortCssClass(this.orderBy, 'winnings');
            },
            buyinSortCssClass: function () {
                return getSortCssClass(this.orderBy, 'buyin');
            },
            cashoutSortCssClass: function () {
                return getSortCssClass(this.orderBy, 'cashout');
            },
            timeSortCssClass: function () {
                return getSortCssClass(this.orderBy, 'time');
            },
            gameCountSortCssClass: function () {
                return getSortCssClass(this.orderBy, 'gamecount');
            },
            winrateSortCssClass: function () {
                return getSortCssClass(this.orderBy, 'winrate');
            },
            formattedWinnings: function () {
                return this.formatResult(this.player.winnings);
            },
            formattedBuyin: function () {
                return this.formatCurrency(this.player.buyin);
            },
            formattedCashout: function () {
                return this.formatCurrency(this.player.stack);
            },
            formattedWinrate: function () {
                return this.formatWinrate(this.player.winrate);
            },
            formattedTime: function () {
                return this.formatTime(this.player.playedTimeInMinutes);
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

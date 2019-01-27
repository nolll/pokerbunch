<template>
    <tr class="table-list__row">
        <td class="table-list__cell table-list__cell--numeric table-list--sortable__base-column">{{player.rank}}.</td>
        <td class="table-list__cell table-list--sortable__base-column">
            <custom-link :url="url">{{player.name}}</custom-link>
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
    import { mapGetters } from 'vuex';
    import { FormatMixin } from '@/mixins';
    import { GAME_ARCHIVE } from '@/store-names';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import urls from '@/urls';

    export default {
        mixins: [
            FormatMixin
        ],
        components: {
            CustomLink
        },
        props: ['player'],
        computed: {
            ...mapGetters(GAME_ARCHIVE, [
                'playerSortOrder'
            ]),
            url() {
                return urls.player.details(this.player.id);
            },
            winningsCssClass() {
                var winnings = this.player.winnings;
                if (winnings === 0)
                    return '';
                return winnings > 0 ? 'pos-result' : 'neg-result';
            },
            winningsSortCssClass() {
                return getSortCssClass(this.orderBy, 'winnings');
            },
            buyinSortCssClass() {
                return getSortCssClass(this.orderBy, 'buyin');
            },
            cashoutSortCssClass() {
                return getSortCssClass(this.orderBy, 'cashout');
            },
            timeSortCssClass() {
                return getSortCssClass(this.orderBy, 'time');
            },
            gameCountSortCssClass() {
                return getSortCssClass(this.orderBy, 'gamecount');
            },
            winrateSortCssClass() {
                return getSortCssClass(this.orderBy, 'winrate');
            },
            formattedWinnings() {
                return this.formatResult(this.player.winnings);
            },
            formattedBuyin() {
                return this.formatCurrency(this.player.buyin);
            },
            formattedCashout() {
                return this.formatCurrency(this.player.stack);
            },
            formattedWinrate() {
                return this.formatWinrate(this.player.winrate);
            },
            formattedTime() {
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

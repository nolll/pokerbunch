<template>
    <tr class="table-list__row">
        <td class="table-list__cell table-list--sortable__base-column" :class="dateSortClasses">
            <CustomLink :url="url">{{displayDate}}</CustomLink>
        </td>
        <td class="table-list__cell table-list__cell--numeric" :class="playerCountSortClasses">{{game.playerCount}}</td>
        <td class="table-list__cell">{{game.location.name}}</td>
        <td class="table-list__cell" :class="durationSortClasses">{{duration}}</td>
        <td class="table-list__cell table-list__cell--numeric" :class="turnoverSortClasses">{{formattedTurnover}}</td>
        <td class="table-list__cell table-list__cell--numeric" :class="averageBuyinSortClasses">{{formattedAverageBuyin}}</td>
    </tr>
</template>

<script lang="ts">
    import dayjs from 'dayjs';
    import urls from '@/urls';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import { BunchMixin, FormatMixin, GameArchiveMixin } from '@/mixins';
    import { Component, Mixins, Prop } from 'vue-property-decorator';
    import { ArchiveCashgame } from '@/models/ArchiveCashgame';
    import { CashgameSortOrder } from '@/models/CashgameSortOrder';
    import format from '@/format';

    @Component({
        components: {
            CustomLink
        }
    })
    export default class GameListRow extends Mixins(
        BunchMixin,
        FormatMixin,
        GameArchiveMixin
    ){
        @Prop() readonly game!: ArchiveCashgame;

        get url() {
            return urls.cashgame.details(this.$_slug, this.game.id);
        }

        get displayDate() {
            return format.monthDay(this.game.date);
        }

        get dateSortClasses() {
            return this.getSortCssClasses(CashgameSortOrder.Date);
        }

        get playerCountSortClasses() {
            return this.getSortCssClasses(CashgameSortOrder.PlayerCount);
        }

        get durationSortClasses() {
            return this.getSortCssClasses(CashgameSortOrder.Duration);
        }

        get turnoverSortClasses() {
            return this.getSortCssClasses(CashgameSortOrder.Turnover);
        }

        get averageBuyinSortClasses() {
            return this.getSortCssClasses(CashgameSortOrder.AverageBuyin);
        }
        
        get duration() {
            return this.$_formatDuration(this.game.duration);
        }

        get formattedAverageBuyin() {
            return this.$_formatCurrency(this.game.averageBuyin);
        }

        get formattedTurnover() {
            return this.$_formatCurrency(this.game.turnover);
        }

        isSortedBy(col: CashgameSortOrder) {
            return this.$_gameSortOrder === col;
        }

        getSortCssClasses(col: CashgameSortOrder) {
            return {
                'table-list--sortable__sort-item': this.isSortedBy(col)
            }
        }
    }
</script>

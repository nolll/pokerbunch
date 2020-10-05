<template>
    <TableListRow>
        <TableListCell><CustomLink :url="url">{{displayDate}}</CustomLink></TableListCell>
        <TableListCell :is-numeric="true">{{game.playerCount}}</TableListCell>
        <TableListCell>{{game.location.name}}</TableListCell>
        <TableListCell>{{duration}}</TableListCell>
        <TableListCell :is-numeric="true">{{formattedTurnover}}</TableListCell>
        <TableListCell :is-numeric="true">{{formattedAverageBuyin}}</TableListCell>
    </TableListRow>
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
    import TableListRow from '@/components/Common/TableList/TableListRow.vue';
    import TableListCell from '@/components/Common/TableList/TableListCell.vue';

    @Component({
        components: {
            CustomLink,
            TableListRow,
            TableListCell
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

        get duration() {
            return this.$_formatDuration(this.game.duration);
        }

        get formattedAverageBuyin() {
            return this.$_formatCurrency(this.game.averageBuyin);
        }

        get formattedTurnover() {
            return this.$_formatCurrency(this.game.turnover);
        }
    }
</script>

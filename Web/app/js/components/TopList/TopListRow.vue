<template>
    <TableListRow>
        <TableListCell :is-numeric="true">{{player.rank}}.</TableListCell>
        <TableListCell><CustomLink :url="url">{{player.name}}</CustomLink></TableListCell>
        <TableListCell :is-numeric="true"><FormattedResult :text="formattedWinnings" :value="winnings" /></TableListCell>
        <TableListCell :is-numeric="true">{{formattedBuyin}}</TableListCell>
        <TableListCell :is-numeric="true">{{formattedCashout}}</TableListCell>
        <TableListCell>{{formattedTime}}</TableListCell>
        <TableListCell :is-numeric="true">{{player.gameCount}}</TableListCell>
        <TableListCell :is-numeric="true">{{formattedWinrate}}</TableListCell>
    </TableListRow>
</template>

<script lang="ts">
    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import { FormatMixin } from '@/mixins';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import urls from '@/urls';
    import { GameArchiveMixin } from '@/mixins';
    import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
    import { CssClasses } from '@/models/CssClasses';
    import TableListRow from '@/components/Common/TableList/TableListRow.vue';
    import TableListCell from '@/components/Common/TableList/TableListCell.vue';
    import FormattedResult from '@/components/Common/FormattedResult.vue';

    @Component({
        components: {
            CustomLink,
            TableListRow,
            TableListCell,
            FormattedResult
        }
    })
    export default class TopListRow extends Mixins(
        FormatMixin,
        GameArchiveMixin
    ) {
        @Prop() readonly bunchId!: string;
        @Prop() readonly player!: CashgameListPlayerData;

        get url() {
            return urls.player.details(this.bunchId, this.player.id);
        }
        
        get winningsCssClass(): CssClasses {
            const winnings = this.player.winnings;
            return {
                'pos-result': winnings > 0,
                'neg-result': winnings < 0
            }
        }

        get formattedWinnings() {
            return this.$_formatResult(this.winnings);
        }

        get winnings() {
            return this.player.winnings;
        }

        get formattedBuyin() {
            return this.$_formatCurrency(this.player.buyin);
        }

        get formattedCashout() {
            return this.$_formatCurrency(this.player.stack);
        }

        get formattedWinrate() {
            return this.$_formatWinrate(this.player.winrate);
        }

        get formattedTime() {
            return this.$_formatDuration(this.player.playedTimeInMinutes);
        }
    }
</script>

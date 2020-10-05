<template>
    <TableListCell :is-numeric="true">
        <span class="matrix__value" v-if="isInGame"><FormattedResult :text="formattedWinnings" :value="winnings" /></span>
    </TableListCell>
</template>

<script lang="ts">
    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import { FormatMixin } from '@/mixins';
    import { CashgamePlayerData } from '@/models/CashgamePlayerData';
    import { CssClasses } from '@/models/CssClasses';
    import TableListCell from '@/components/Common/TableList/TableListCell.vue';
    import FormattedResult from '@/components/Common/FormattedResult.vue';
    
    @Component({
        components: {
            TableListCell,
            FormattedResult
        }
    })
    export default class OverviewItem extends Mixins(
        FormatMixin
    ) {
        @Prop() readonly game!: CashgamePlayerData;

        get buyin() {
            return this.game.buyin;
        }

        get stack() {
            return this.game.stack;
        }

        get winnings() {
            return this.game.winnings;
        }

        get formattedWinnings() {
            return this.$_formatResult(this.winnings);
        }

        get isInGame() {
            return this.game.playedThisGame;
        }
    }
</script>

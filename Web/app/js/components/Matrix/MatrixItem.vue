<template>
    <TableListCell :is-numeric="true" :custom-classes="winnerClass">
        <template v-if="isInGame">
            <span class="matrix__value"><FormattedResult :text="winningsText" :value="winnings" /></span>
            <span class="matrix__transaction">in {{buyin}}</span>
            <span class="matrix__transaction">out {{stack}}</span>
        </template>
    </TableListCell>
</template>

<script lang="ts">
    import { CashgamePlayerData } from '@/models/CashgamePlayerData';
    import { CssClasses } from '@/models/CssClasses';
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import TableListCell from '@/components/Common/TableList/TableListCell.vue';
    import FormattedResult from '@/components/Common/FormattedResult.vue';
    
    @Component({
        components: {
            TableListCell,
            FormattedResult
        }
    })
    export default class MatrixItem extends Vue {
        @Prop() readonly game!: CashgamePlayerData;

        get buyin() {
            return this.game.buyin;
        }

        get stack() {
            return this.game.stack;
        }

        get winningsText() {
            if (this.winnings > 0)
                return '+' + this.winnings;
            return this.winnings;
        }

        get winnings() {
            return this.game.winnings;
        }

        get winnerClass(): CssClasses {
            return {
                'matrix__winner': this.game.isWinner
            };
        }

        get isInGame() {
            return this.game.playedThisGame;
        }
    }
</script>

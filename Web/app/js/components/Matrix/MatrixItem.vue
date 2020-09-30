<template>
    <td class="table-list__cell table-list__cell--numeric" :class="winnerClass">
        <div v-if="isInGame">
            <span class="matrix__value" :class="resultClass">{{winnings}}</span>
            <span class="matrix__transaction">in {{buyin}}</span>
            <span class="matrix__transaction">out {{stack}}</span>
        </div>
    </td>
</template>

<script lang="ts">
    import { CashgamePlayerData } from '@/models/CashgamePlayerData';
    import { CssClasses } from '@/models/CssClasses';
    import { Component, Prop, Vue } from 'vue-property-decorator';

    @Component
    export default class MatrixItem extends Vue {
        @Prop() readonly game!: CashgamePlayerData;

        get buyin() {
            return this.game.buyin;
        }

        get stack() {
            return this.game.stack;
        }

        get winnings() {
            if (this.game.winnings > 0)
                return '+' + this.game.winnings;
            return this.game.winnings;
        }

        get winnerClass(): CssClasses {
            return {
                'matrix__winner': this.game.isWinner
            };
        }

        get resultClass(): CssClasses {
            return {
                'pos-result': this.winnings > 0,
                'neg-result': this.winnings < 0
            };
        }

        get isInGame() {
            return this.game.playedThisGame;
        }
    }
</script>

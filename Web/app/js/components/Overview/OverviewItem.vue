<template>
    <td class="table-list__cell table-list__cell--numeric">
        <div v-if="isInGame">
            <span class="matrix__value" :class="resultClass">{{formattedWinnings}}</span>
        </div>
    </td>
</template>

<script lang="ts">
    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import { FormatMixin } from '@/mixins';
    import { CashgamePlayerData } from '@/models/CashgamePlayerData';
    import { CssClasses } from '@/models/CssClasses';
    
    @Component
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

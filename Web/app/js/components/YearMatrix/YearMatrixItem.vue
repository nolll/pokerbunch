<template>
    <td class="table-list__cell table-list__cell--numeric">
        <div v-if="playedThisYear">
            <span class="matrix__value" :class="valueCss">{{formattedWinnings}}</span>
        </div>
    </td>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import { CashgamePlayerYearlyResult } from '@/models/CashgamePlayerYearlyResult';
    import { CssClasses } from '@/models/CssClasses';

    @Component
    export default class YearMatrixItem extends Vue {
        @Prop() readonly year!: CashgamePlayerYearlyResult;

        get formattedWinnings() {
            if (this.year.winnings > 0)
                return '+' + this.year.winnings;
            return this.year.winnings.toString();
        }

        get valueCss(): CssClasses {
            return {
                'pos-result': this.year.winnings > 0,
                'neg-result': this.year.winnings < 0
            };
        }

        get playedThisYear() {
            return this.year.playedThisYear;
        }
    }
</script>

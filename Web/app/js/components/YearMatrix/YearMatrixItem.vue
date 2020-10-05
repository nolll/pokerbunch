<template>
    <TableListCell :is-numeric="true">
        <span class="matrix__value" v-if="playedThisYear"><FormattedResult :text="formattedWinnings" :value="winnings" /></span>
    </TableListCell>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import { CashgamePlayerYearlyResult } from '@/models/CashgamePlayerYearlyResult';
    import { CssClasses } from '@/models/CssClasses';
    import TableListCell from '@/components/Common/TableList/TableListCell.vue';
    import FormattedResult from '@/components/Common/FormattedResult.vue';

    @Component({
        components: {
            TableListCell,
            FormattedResult
        }
    })
    export default class YearMatrixItem extends Vue {
        @Prop() readonly year!: CashgamePlayerYearlyResult;

        get formattedWinnings() {
            if (this.winnings > 0)
                return '+' + this.winnings;
            return this.winnings.toString();
        }

        get winnings() {
            return this.year.winnings;
        }

        get playedThisYear() {
            return this.year.playedThisYear;
        }
    }
</script>

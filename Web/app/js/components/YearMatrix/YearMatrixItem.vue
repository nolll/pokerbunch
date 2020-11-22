<template>
    <TableListCell :is-numeric="true">
        <span class="matrix__value" v-if="playedThisYear"><WinningsText :value="winnings" :showCurrency="false" /></span>
    </TableListCell>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import { CashgamePlayerYearlyResult } from '@/models/CashgamePlayerYearlyResult';
    import { CssClasses } from '@/models/CssClasses';
    import TableListCell from '@/components/Common/TableList/TableListCell.vue';
    import WinningsText from '@/components/Common/WinningsText.vue';

    @Component({
        components: {
            TableListCell,
            WinningsText
        }
    })
    export default class YearMatrixItem extends Vue {
        @Prop() readonly year!: CashgamePlayerYearlyResult;

        get winnings() {
            return this.year.winnings;
        }

        get playedThisYear() {
            return this.year.playedThisYear;
        }
    }
</script>

<template>
    <TableListRow>
        <TableListCell :is-numeric="true">{{rank}}.</TableListCell>
        <TableListCell>
            <CustomLink :url="url">{{name}}</CustomLink>
        </TableListCell>
        <TableListCell :is-numeric="true"><WinningsText :value="winnings" /></TableListCell>
        <YearMatrixItem v-for="year in player.years" :year="year" :key="year.year" />
    </TableListRow>
</template>

<script lang="ts">
    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import { FormatMixin } from '@/mixins'
    import YearMatrixItem from './YearMatrixItem.vue';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import urls from '@/urls';
    import { CashgamePlayerYearlyResultCollection } from '@/models/CashgamePlayerYearlyResultCollection';
    import { CssClasses } from '@/models/CssClasses';
    import TableListRow from '@/components/Common/TableList/TableListRow.vue';
    import TableListCell from '@/components/Common/TableList/TableListCell.vue';
    import WinningsText from '@/components/Common/WinningsText.vue';

    @Component({
        components: {
            YearMatrixItem,
            CustomLink,
            TableListRow,
            TableListCell,
            WinningsText
        }
    })
    export default class YearMatrixRow extends Mixins(
        FormatMixin
    ) {
        @Prop() readonly bunchId!: string;
        @Prop() readonly player!: CashgamePlayerYearlyResultCollection;
        @Prop() readonly index!: number;

        get url() {
            return urls.player.details(this.bunchId, this.player.id);
        }

        get name() { return this.player.name };
        get rank() { return this.index + 1; }
        get winnings() { return this.player.winnings; }
    }
</script>

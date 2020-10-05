<template>
    <TableListRow>
        <TableListCell :is-numeric="true">{{rank}}.</TableListCell>
        <TableListCell>
            <CustomLink :url="url">{{name}}</CustomLink>
        </TableListCell>
        <TableListCell :is-numeric="true"><FormattedResult :text="formattedWinnings" :value="winnings" /></TableListCell>
        <MatrixItem v-for="game in player.gameResults" :game="game" :key="game.gameId" />
    </TableListRow>
</template>

<script lang="ts">
    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import urls from '@/urls';
    import { FormatMixin } from '@/mixins'
    import MatrixItem from './MatrixItem.vue';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import TableListRow from '@/components/Common/TableList/TableListRow.vue';
    import TableListCell from '@/components/Common/TableList/TableListCell.vue';
    import FormattedResult from '@/components/Common/FormattedResult.vue';
    import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';

    @Component({
        components: {
            MatrixItem,
            CustomLink,
            TableListRow,
            TableListCell,
            FormattedResult
        }
    })
    export default class MatrixRow extends Mixins(
        FormatMixin
    ) {
        @Prop() readonly player!: CashgameListPlayerData;
        @Prop() readonly index!: number;

        get url() {
            return urls.player.details(this.player.id);
        }

        get name() {
            return this.player.name;
        }

        get rank() {
            return this.index + 1;
        }

        get winnings() {
            return this.player.winnings;
        }

        get formattedWinnings() {
            return this.$_formatResult(this.winnings);
        }
    }
</script>

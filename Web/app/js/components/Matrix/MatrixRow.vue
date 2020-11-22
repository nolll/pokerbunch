<template>
    <TableListRow>
        <TableListCell :is-numeric="true">{{rank}}.</TableListCell>
        <TableListCell>
            <CustomLink :url="url">{{name}}</CustomLink>
        </TableListCell>
        <TableListCell :is-numeric="true"><WinningsText :value="winnings" /></TableListCell>
        <MatrixItem v-for="game in player.gameResults" :game="game" :key="game.gameId" />
    </TableListRow>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import urls from '@/urls';
    import MatrixItem from './MatrixItem.vue';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import TableListRow from '@/components/Common/TableList/TableListRow.vue';
    import TableListCell from '@/components/Common/TableList/TableListCell.vue';
    import WinningsText from '@/components/Common/WinningsText.vue';
    import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';

    @Component({
        components: {
            MatrixItem,
            CustomLink,
            TableListRow,
            TableListCell,
            WinningsText
        }
    })
    export default class MatrixRow extends Vue {
        @Prop() readonly bunchId!: string;
        @Prop() readonly player!: CashgameListPlayerData;
        @Prop() readonly index!: number;

        get url() {
            return urls.player.details(this.bunchId, this.player.id);
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
    }
</script>

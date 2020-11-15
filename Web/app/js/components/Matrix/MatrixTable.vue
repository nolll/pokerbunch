<template>
    <div class="matrix" v-if="hasGames">
        <TableList>
            <thead>
                <tr>
                    <TableListColumnHeader />
                    <TableListColumnHeader>Player</TableListColumnHeader>
                    <TableListColumnHeader>Winnings</TableListColumnHeader>
                    <MatrixColumn v-for="game in games" :game="game" :slug="slug" :key="game.id" />
                </tr>
            </thead>
            <tbody>
                <MatrixRow v-for="(player, index) in players" :player="player" :index="index" :key="player.id" :bunchId="slug" />
            </tbody>
        </TableList>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import MatrixColumn from './MatrixColumn.vue';
    import MatrixRow from './MatrixRow.vue';
    import { BunchMixin, GameArchiveMixin } from '@/mixins';
    import TableList from '@/components/Common/TableList/TableList.vue';
    import TableListColumnHeader from '@/components/Common/TableList/TableListColumnHeader.vue';
    import { ArchiveCashgame } from '@/models/ArchiveCashgame';
    import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
    import archiveHelper from '@/ArchiveHelper';
    import playerSorter from '@/PlayerSorter';

    @Component({
        components: {
            MatrixColumn,
            MatrixRow,
            TableList, 
            TableListColumnHeader
        }
    })
    export default class MatrixTable extends Vue{
        @Prop() readonly slug!: string;
        @Prop() readonly games!: ArchiveCashgame[];

        get hasGames() {
            return this.games.length > 0;
        }

        get players(){
            return playerSorter.sort(archiveHelper.getPlayers(this.games));
        }
    }
</script>

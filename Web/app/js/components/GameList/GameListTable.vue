<template>
    <div class="game-list">
        <TableList>
            <thead>
                <tr>
                    <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="date">Date</TableListColumnHeader>
                    <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="playercount">Players</TableListColumnHeader>
                    <TableListColumnHeader>Location</TableListColumnHeader>
                    <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="duration">Duration</TableListColumnHeader>
                    <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="turnover">Turnover</TableListColumnHeader>
                    <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="averagebuyin">Average Buyin</TableListColumnHeader>
                </tr>
            </thead>
            <tbody class="list">
                <GameListRow v-for="game in $_sortedGames" :game="game" :key="game.id" />
            </tbody>
        </TableList>
    </div>
</template>

<script lang="ts">
    import GameListRow from './GameListRow.vue';
    import { BunchMixin, GameArchiveMixin } from '@/mixins';
    import Component from 'vue-class-component';
    import { Mixins } from 'vue-property-decorator';
    import TableList from '@/components/Common/TableList/TableList.vue';
    import TableListColumnHeader from '@/components/Common/TableList/TableListColumnHeader.vue';

    @Component({
        components: {
            GameListRow,
            TableList,
            TableListColumnHeader
        }
    })
    export default class GameListTable extends Mixins(
        BunchMixin,
        GameArchiveMixin
    ){
        get ready() {
            return this.$_bunchReady && this.$_sortedGames.length > 0;
        }

        get orderedBy(){
            return this.$_gameSortOrder;
        }

        sort(column: string) {
            this.$_sortGames(column);
        }
    }
</script>

<style lang="scss" scoped>
    .game-list{
        overflow: auto;
    }
</style>

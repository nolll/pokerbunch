<template>
    <div class="top-list">
        <TableList>
            <thead>
                <tr>
                    <TableListColumnHeader />
                    <TableListColumnHeader>Player</TableListColumnHeader>
                    <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="winnings">Winnings</TableListColumnHeader>
                    <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="buyin">Buyin</TableListColumnHeader>
                    <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="stack">Cashout</TableListColumnHeader>
                    <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="time">Time</TableListColumnHeader>
                    <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="gamecount">Games</TableListColumnHeader>
                    <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="winrate">Win rate</TableListColumnHeader>
                </tr>
            </thead>
            <tbody class="list">
                <TopListRow v-for="player in players" :player="player" :key="player.id" :bunchId="bunchId" />
            </tbody>
        </TableList>
    </div>
</template>

<script lang="ts">
    import { Component, Mixins, Prop } from 'vue-property-decorator';
    import TopListRow from './TopListRow.vue';
    import { GameArchiveMixin } from '@/mixins';
    import TableList from '@/components/Common/TableList/TableList.vue';
    import TableListColumnHeader from '@/components/Common/TableList/TableListColumnHeader.vue';

    @Component({
        components: {
            TopListRow,
            TableList,
            TableListColumnHeader
        }
    })
    export default class TopListTable extends Mixins(
        GameArchiveMixin
    ) {
        @Prop() readonly bunchId!: string;

        get players(){
            return this.$_sortedPlayers;
        }

        get orderedBy(){
            return this.$_playerSortOrder;
        }

        sort(column: string) {
            this.$_sortPlayers(column);
        }
    }
</script>

<style lang="less" scoped>
    .top-list {
        overflow: auto;
    }
</style>

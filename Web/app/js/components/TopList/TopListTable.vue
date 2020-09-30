<template>
    <div class="top-list">
        <table class="table-list table-list--sortable">
            <thead>
                <tr>
                    <th class="table-list__column-header table-list--sortable__base-column"></th>
                    <th class="table-list__column-header table-list--sortable__base-column"><span class="table-list__column-header__content">Player</span></th>
                    <th is="top-list-column" name="winnings" title="Winnings"></th>
                    <th is="top-list-column" name="buyin" title="Buyin"></th>
                    <th is="top-list-column" name="stack" title="Cashout"></th>
                    <th is="top-list-column" name="time" title="Time"></th>
                    <th is="top-list-column" name="gamecount" title="Games"></th>
                    <th is="top-list-column" name="winrate" title="Win rate"></th>
                </tr>
            </thead>
            <tbody class="list">
                <tr is="top-list-row" v-for="player in players" :player="player" :key="player.id"></tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="ts">
    import { Component, Mixins } from 'vue-property-decorator';
    import TopListColumn from './TopListColumn.vue';
    import TopListRow from './TopListRow.vue';
    import { GameArchiveMixin } from '@/mixins';

    @Component({
        components: {
            TopListColumn,
            TopListRow
        }
    })
    export default class TopListTable extends Mixins(
        GameArchiveMixin
    ) {
        get players(){
            return this.$_sortedPlayers;
        }
    }
</script>

<style lang="less" scoped>
    .top-list {
        overflow: auto;
    }
</style>

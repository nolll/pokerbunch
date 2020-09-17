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
                <tr is="top-list-row" v-for="player in $_sortedPlayers" :player="player" :key="player.id"></tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    import { TopListColumn, TopListRow } from '.';
    import { GameArchiveMixin } from '@/mixins';

    export default {
        components: {
            TopListColumn,
            TopListRow
        },
        mixins: [
            GameArchiveMixin
        ]
    };
</script>

<style lang="less" scoped>
    .top-list {
        overflow: auto;
    }
</style>


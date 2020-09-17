<template>
    <div class="game-list">
        <table class="table-list table-list--sortable">
            <thead>
                <tr>
                    <th is="game-list-column" name="date" title="Date"></th>
                    <th is="game-list-column" name="playercount" title="Players"></th>
                    <th class="table-list__column-header"><span class="table-list__column-header__content">Location</span></th>
                    <th is="game-list-column" name="duration" title="Duration"></th>
                    <th is="game-list-column" name="turnover" title="Turnover"></th>
                    <th is="game-list-column" name="averagebuyin" title="Average Buyin"></th>
                </tr>
            </thead>
            <tbody class="list">
                <tr is="game-list-row" v-for="game in $_sortedGames" :game="game" :key="game.id"></tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    import { GameListColumn, GameListRow } from '.';
    import { BunchMixin, GameArchiveMixin } from '@/mixins';

    export default {
        components: {
            GameListColumn,
            GameListRow
        },
        mixins: [
            BunchMixin,
            GameArchiveMixin
        ],
        computed: {
            ready() {
                return this.$_bunchReady && this.$_sortedGames.length > 0;
            }
        }
    };
</script>

<style lang="less" scoped>
    .game-list{
        overflow: auto;
    }
</style>

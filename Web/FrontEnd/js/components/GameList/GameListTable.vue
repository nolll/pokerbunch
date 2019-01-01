<template>
    <div>
        <div class="table-list--sortable__sort-order-selector">
            <label for="gamelist-sortorder">Select Data:</label>
            <select id="gamelist-sortorder" v-model="gameSortOrder">
                <option value="date">Date</option>
                <option value="playercount">Players</option>
                <option value="duration">Duration</option>
                <option value="turnover">Turnover</option>
                <option value="averagebuyin">Average Buyin</option>
            </select>
        </div>
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
                <tr is="game-list-row" v-for="game in sortedGames" :game="game"></tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    import { mapState, mapGetters } from 'vuex';
    import { GameListColumn, GameListRow } from '.';
    import { BUNCH, GAME_ARCHIVE } from '@/store-names';

    export default {
        components: {
            GameListColumn,
            GameListRow
        },
        computed: {
            ...mapState(GAME_ARCHIVE, [
                'gameSortOrder'
            ]),
            ...mapGetters(BUNCH, [
                'bunchReady'
            ]),
            ...mapGetters(GAME_ARCHIVE, [
                'sortedGames'
            ]),
            ready() {
                return this.bunchReady && this.sortedGames.length > 0;
            }
        }
    };
</script>

<style>

</style>

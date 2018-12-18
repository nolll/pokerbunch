<template>
    <div>
        <div class="table-list--sortable__sort-order-selector">
            <label for="toplist-sortorder">Select Data:</label>
            <select id="toplist-sortorder" v-model="playerSortOrder">
                <option value="winnings">Winnings</option>
                <option value="buyin">Buyin</option>
                <option value="cashout">Cashout</option>
                <option value="time">Time</option>
                <option value="gamecount">Games</option>
                <option value="winrate">Win rate</option>
            </select>
        </div>
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
                <tr is="top-list-row" v-for="player in sortedPlayers" :player="player"></tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    import { mapState, mapGetters } from 'vuex';
    import { TopListColumn, TopListRow } from '.';
    import { BUNCH, GAME_ARCHIVE } from '@/store-names';

    export default {
        components: {
            TopListColumn,
            TopListRow
        },
        computed: {
            ...mapState(GAME_ARCHIVE, [
                'playerSortOrder',
                'gamesReady'
            ]),
            ...mapState(BUNCH, [
                'bunchReady'
            ]),
            ...mapGetters(GAME_ARCHIVE, [
                'sortedPlayers'
            ]),
            ready() {
                return this.bunchReady && this.gamesReady;
            }
        }
    };
</script>

<style>

</style>

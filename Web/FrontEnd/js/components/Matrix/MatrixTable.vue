<template>
    <div class="matrix" v-if="ready">
        <table class="table-list">
            <thead>
                <tr>
                    <th class="table-list__column-header"></th>
                    <th class="table-list__column-header"><span class="table-list__column-header__content">Player</span></th>
                    <th class="table-list__column-header"><span class="table-list__column-header__content">Winnings</span></th>
                    <th is="matrix-column" v-for="game in sortedGames" :game="game"></th>
                </tr>
            </thead>
            <tbody>
                <tr is="matrix-row" v-for="(player, index) in sortedPlayers" :player="player" :index="index"></tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    import { mapState, mapGetters } from 'vuex';
    import { MatrixColumn, MatrixRow } from ".";
    import { BUNCH, GAME_ARCHIVE } from '../../store-names';

    export default {
        components: {
            MatrixColumn,
            MatrixRow
        },
        computed: {
            ...mapState(BUNCH, {
                bunchReady: state => state.bunchReady
            }),
            ...mapGetters(GAME_ARCHIVE, {
                sortedGames: getters => getters.sortedGames,
                sortedPlayers: getters => getters.sortedPlayers
            }),
            ready() {
                return this.bunchReady && this.sortedGames.length > 0;
            }
        }
    };
</script>

<style>

</style>

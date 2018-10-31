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

    export default {
        components: {
            MatrixColumn,
            MatrixRow
        },
        computed: {
            ...mapGetters('gameArchive', ['sortedGames', 'sortedPlayers']),
            ...mapState('bunch', ['bunchReady']),
            ready() {
                return this.bunchReady && this.sortedGames.length > 0;
            }
        }
    };
</script>

<style>

</style>

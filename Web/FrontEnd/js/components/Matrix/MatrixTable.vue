<template>
    <div class="matrix" v-if="ready">
        <table class="table-list">
            <thead>
                <tr>
                    <th class="table-list__column-header"></th>
                    <th class="table-list__column-header"><span class="table-list__column-header__content">Player</span></th>
                    <th class="table-list__column-header"><span class="table-list__column-header__content">Winnings</span></th>
                    <th is="matrix-column" v-for="game in sortedGames" :game="game" :key="game.id"></th>
                </tr>
            </thead>
            <tbody>
                <tr is="matrix-row" v-for="(player, index) in sortedPlayers" :player="player" :index="index" :key="player.id"></tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    import { mapGetters } from 'vuex';
    import { MatrixColumn, MatrixRow } from '.';
    import { BUNCH, GAME_ARCHIVE } from '@/store-names';

    export default {
        components: {
            MatrixColumn,
            MatrixRow
        },
        computed: {
            ...mapGetters(BUNCH, [
                'bunchReady'
            ]),
            ...mapGetters(GAME_ARCHIVE, [
                'sortedGames',
                'sortedPlayers'
            ]),
            ready() {
                return this.bunchReady && this.sortedGames.length > 0;
            }
        }
    };
</script>

<style>

</style>

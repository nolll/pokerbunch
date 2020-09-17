<template>
    <div class="matrix" v-if="ready">
        <table class="table-list">
            <thead>
                <tr>
                    <th class="table-list__column-header"></th>
                    <th class="table-list__column-header"><span class="table-list__column-header__content">Player</span></th>
                    <th class="table-list__column-header"><span class="table-list__column-header__content">Winnings</span></th>
                    <th is="matrix-column" v-for="game in $_sortedGames" :game="game" :slug="$_slug" :key="game.id"></th>
                </tr>
            </thead>
            <tbody>
                <tr is="matrix-row" v-for="(player, index) in $_sortedPlayers" :player="player" :index="index" :key="player.id"></tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    import { MatrixColumn, MatrixRow } from '.';
    import { BunchMixin, GameArchiveMixin } from '@/mixins';

    export default {
        components: {
            MatrixColumn,
            MatrixRow
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

<style>

</style>

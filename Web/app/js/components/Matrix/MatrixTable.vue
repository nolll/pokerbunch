<template>
    <div class="matrix" v-if="ready">
        <table class="table-list">
            <thead>
                <tr>
                    <th class="table-list__column-header"></th>
                    <th class="table-list__column-header"><span class="table-list__column-header__content">Player</span></th>
                    <th class="table-list__column-header"><span class="table-list__column-header__content">Winnings</span></th>
                    <th is="matrix-column" v-for="game in games" :game="game" :slug="slug" :key="game.id"></th>
                </tr>
            </thead>
            <tbody>
                <tr is="matrix-row" v-for="(player, index) in players" :player="player" :index="index" :key="player.id"></tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import MatrixColumn from './MatrixColumn.vue';
    import MatrixRow from './MatrixRow.vue';
    import { BunchMixin, GameArchiveMixin } from '@/mixins';

    @Component({
        components: {
            MatrixColumn,
            MatrixRow
        }
    })
    export default class MatrixTable extends Mixins(
        BunchMixin,
        GameArchiveMixin
    ) {
        get slug(){
            return this.$_slug;
        }

        get games(){
            return this.$_sortedGames;
        }

        get players(){
            return this.$_sortedPlayers;
        }

        get ready() {
            return this.$_bunchReady && this.$_sortedGames.length > 0;
        }
    }
</script>

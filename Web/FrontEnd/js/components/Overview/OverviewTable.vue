<template>
    <div class="matrix" v-if="ready">
        <table class="table-list">
            <thead>
                <tr>
                    <th class="table-list__column-header"></th>
                    <th class="table-list__column-header"><span class="table-list__column-header__content">Player</span></th>
                    <th class="table-list__column-header"><span class="table-list__column-header__content">Total</span></th>
                    <th class="table-list__column-header">
                        <a :href="url" class="table-list__column-header__content">Last game</a>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr is="overview-row" v-for="(player, index) in currentYearPlayers" :player="player" :index="index"></tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    import { mapState, mapGetters } from 'vuex';
    import urls from '../../urls';
    import { OverviewRow } from ".";
    import { BUNCH, GAME_ARCHIVE } from '@/store-names';

    export default {
        components: {
            OverviewRow
        },
        computed: {
            ...mapState(BUNCH, [
                'bunchReady'
            ]),
            ...mapGetters(GAME_ARCHIVE, [
                'currentYearGames',
                'currentYearPlayers'
            ]),
            url() {
                return urls.cashgameDetails(this.lastGame.id);
            },
            lastGame() {
                return this.currentYearGames[0];
            },
            ready() {
                return this.bunchReady && this.currentYearPlayers.length > 0;
            }
        }
    };
</script>

<style>

</style>

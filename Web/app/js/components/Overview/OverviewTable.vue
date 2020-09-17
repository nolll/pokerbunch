<template>
    <div class="matrix" v-if="ready">
        <table class="table-list">
            <thead>
                <tr>
                    <th class="table-list__column-header"></th>
                    <th class="table-list__column-header"><span class="table-list__column-header__content">Player</span></th>
                    <th class="table-list__column-header"><span class="table-list__column-header__content">Total</span></th>
                    <th class="table-list__column-header">
                        <custom-link :url="url" cssClasses="table-list__column-header__content">Last game</custom-link>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr is="overview-row" v-for="(player, index) in $_currentYearPlayers" :player="player" :index="index" :key="player.id"></tr>
            </tbody>
        </table>
    </div>
</template>_

<script>
    import urls from '@/urls';
    import { OverviewRow } from '@/components/Overview';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import { BunchMixin, GameArchiveMixin } from '@/mixins';

    export default {
        components: {
            OverviewRow,
            CustomLink
        },
        mixins: [
            BunchMixin,
            GameArchiveMixin
        ],
        computed: {
            url() {
                return urls.cashgame.details(this.$_slug, this.lastGame.id);
            },
            lastGame() {
                return this.$_currentYearGames[0];
            },
            ready() {
                return this.$_bunchReady && this.$_currentYearPlayers.length > 0;
            }
        }
    };
</script>

<style>

</style>

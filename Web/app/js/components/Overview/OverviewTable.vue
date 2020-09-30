<template>
    <div class="matrix" v-if="ready">
        <table class="table-list">
            <thead>
                <tr>
                    <th class="table-list__column-header"></th>
                    <th class="table-list__column-header"><span class="table-list__column-header__content">Player</span></th>
                    <th class="table-list__column-header"><span class="table-list__column-header__content">Total</span></th>
                    <th class="table-list__column-header">
                        <CustomLink :url="url" cssClasses="table-list__column-header__content">Last game</CustomLink>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr is="overview-row" v-for="(player, index) in players" :player="player" :index="index" :key="player.id"></tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="ts">
    import { Component, Mixins } from 'vue-property-decorator';
    import urls from '@/urls';
    import OverviewRow from '@/components/Overview/OverviewRow.vue';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import { BunchMixin, GameArchiveMixin } from '@/mixins';

    @Component({
        components: {
            OverviewRow,
            CustomLink
        }
    })
    export default class OverviewTable extends Mixins(
        BunchMixin,
        GameArchiveMixin
    ) {
        get players(){
            return this.$_currentYearPlayers;
        }

        get url() {
            return urls.cashgame.details(this.$_slug, this.lastGame.id);
        }

        get lastGame() {
            return this.$_currentYearGames[0];
        }

        get ready() {
            return this.$_bunchReady && this.$_currentYearPlayers.length > 0;
        }
    }
</script>

<template>
    <div class="matrix" v-if="ready">
        <TableList>
            <thead>
                <tr>
                    <TableListColumnHeader />
                    <TableListColumnHeader>Player</TableListColumnHeader>
                    <TableListColumnHeader>Total</TableListColumnHeader>
                    <TableListColumnHeader>
                        <CustomLink :url="url">Last game</CustomLink>
                    </TableListColumnHeader>
                </tr>
            </thead>
            <tbody>
                <OverviewRow v-for="(player, index) in players" :player="player" :index="index" :key="player.id" :bunchId="slug" />
            </tbody>
        </TableList>
    </div>
</template>

<script lang="ts">
    import { Component, Mixins } from 'vue-property-decorator';
    import urls from '@/urls';
    import OverviewRow from '@/components/Overview/OverviewRow.vue';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import { BunchMixin, GameArchiveMixin } from '@/mixins';
    import TableList from '@/components/Common/TableList/TableList.vue';
    import TableListColumnHeader from '@/components/Common/TableList/TableListColumnHeader.vue';

    @Component({
        components: {
            OverviewRow,
            CustomLink,
            TableList,
            TableListColumnHeader
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
            return urls.cashgame.details(this.slug, this.lastGame.id);
        }

        get lastGame() {
            return this.$_currentYearGames[0];
        }

        get slug(){
            return this.$_slug;
        }

        get ready() {
            return this.$_bunchReady && this.$_currentYearPlayers.length > 0;
        }
    }
</script>

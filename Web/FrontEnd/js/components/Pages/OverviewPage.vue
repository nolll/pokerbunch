<template>
    <layout :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <page-section>
            <block>
                <cashgame-navigation page="overview" :year="currentYear" />
            </block>
        </page-section>

        <page-section>
            <template slot="aside">
                <overview-status />
            </template>
            <block>
                <page-heading text="Current Rankings" />
                <overview-table v-if="hasGames" />
                <p v-else>The rankings will be displayed here when you have played your first game.</p>
            </block>
        </page-section>

        <page-section :is-wide="false">
            <block>
                <page-heading text="Yearly Rankings" />
                <year-matrix-table v-if="hasGames" />
            </block>
        </page-section>
    </layout>
</template>

<script>
    import { DataMixin } from '@/mixins';
    import { mapGetters } from 'vuex';
    import { Layout } from '@/components/Layouts';
    import { BunchNavigation, CashgameNavigation } from '@/components/Navigation';
    import { OverviewTable, OverviewStatus, YearMatrixTable } from '@/components';
    import { Block, PageHeading, PageSection } from '@/components/Common';
    import { GAME_ARCHIVE, CURRENT_GAME } from '@/store-names';

    export default {
        components: {
            Layout,
            BunchNavigation,
            CashgameNavigation,
            OverviewTable,
            OverviewStatus,
            YearMatrixTable,
            Block,
            PageHeading,
            PageSection
        },
        mixins: [
            DataMixin
        ],
        computed: {
            ...mapGetters(GAME_ARCHIVE, [
                'currentYear',
                'hasGames'
            ]),
            ...mapGetters(CURRENT_GAME, [
                'currentGames'
            ]),
            ready() {
                return this.bunchReady && this.gamesReady && this.currentGamesReady;
            }
        },
        methods: {
            init() {
                this.loadUser();
                this.loadBunch();
                this.loadGames();
                this.loadCurrentGames();
            }
        },
        watch: {
            '$route'(to, from) {
                this.init();
            }
        },
        mounted: function () {
            this.init();
        }
    };
</script>

<style>

</style>

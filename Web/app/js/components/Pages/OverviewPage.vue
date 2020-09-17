<template>
    <layout :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <page-section>
            <block>
                <cashgame-navigation page="overview" :year="$_currentYear" />
            </block>
        </page-section>

        <page-section>
            <template slot="aside1">
                <overview-status />
            </template>
            <block>
                <page-heading text="Current Rankings" />
                <overview-table v-if="$_hasGames" />
                <p v-else>The rankings will be displayed here when you have played your first game.</p>
            </block>
        </page-section>

        <page-section :is-wide="false">
            <block>
                <page-heading text="Yearly Rankings" />
                <year-matrix-table v-if="$_hasGames" />
            </block>
        </page-section>
    </layout>
</template>

<script>
    import { BunchMixin, UserMixin, GameArchiveMixin, CurrentGameMixin } from '@/mixins';
    import { Layout } from '@/components/Layouts';
    import { BunchNavigation, CashgameNavigation } from '@/components/Navigation';
    import { OverviewTable, OverviewStatus, YearMatrixTable } from '@/components';
    import { Block, PageHeading, PageSection } from '@/components/Common';

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
            BunchMixin,
            UserMixin,
            GameArchiveMixin,
            CurrentGameMixin
        ],
        computed: {
            ready() {
                return this.$_bunchReady && this.$_gamesReady && this.$_currentGamesReady;
            }
        },
        methods: {
            init() {
                this.$_requireUser();
                this.$_loadBunch();
                this.$_loadGames();
                this.$_loadCurrentGames();
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

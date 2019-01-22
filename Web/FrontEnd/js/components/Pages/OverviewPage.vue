<template>
    <two-column :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <template slot="content-nav">
            <cashgame-navigation page="overview" :year="currentYear" />
        </template>

        <template slot="aside">
            <div class="gutter">
                <overview-status />
            </div>
        </template>

        <template slot="main">
            <page-section>
                <page-heading text="Current Rankings" />
            </page-section>
            <page-section>
                <overview-table v-if="hasGames" />
                <p v-else>The rankings will be displayed here when you have played your first game.</p>
            </page-section>

            <page-section>
                <page-heading text="Yearly Rankings" />
            </page-section>
            <page-section>
                <year-matrix-table v-if="hasGames" />
            </page-section>
        </template>
    </two-column>
</template>

<script>
    import { DataMixin } from '@/mixins';
    import { mapGetters } from 'vuex';
    import { TwoColumn } from '@/components/Layouts';
    import { BunchNavigation, CashgameNavigation } from '@/components/Navigation';
    import { OverviewTable, OverviewStatus, YearMatrixTable } from '@/components';
    import { PageHeading, PageSection } from '@/components/Common';
    import { GAME_ARCHIVE } from '@/store-names';

    export default {
        components: {
            TwoColumn,
            BunchNavigation,
            CashgameNavigation,
            OverviewTable,
            OverviewStatus,
            YearMatrixTable,
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
            ready() {
                return this.bunchReady && this.gamesReady && this.currentGameReady;
            }
        },
        methods: {
            init() {
                this.loadUser();
                this.loadBunch();
                this.loadGames();
                this.loadCurrentGame();
            }
        },
        watch: {
            '$route'(to, from) {
                this.init();
            }
        },
        created: function () {
            this.init();
        }
    };
</script>

<style>

</style>

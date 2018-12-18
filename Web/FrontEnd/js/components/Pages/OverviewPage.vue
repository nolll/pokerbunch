<template>
    <two-column>
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
            <div class="block gutter">
                <h1 class="page-heading">Current Rankings</h1>
                <overview-table v-if="hasGames" />
                <p v-else>The rankings will be displayed here when you have played your first game.</p>
            </div>

            <div class="block gutter">
                <h1 class="page-heading">Yearly Rankings</h1>
                <year-matrix-table v-if="hasGames" />
            </div>
        </template>
    </two-column>
</template>

<script>
    import { DataMixin } from '@/mixins';
    import { mapGetters } from 'vuex';
    import { TwoColumn } from "@/components/Layouts";
    import { BunchNavigation, CashgameNavigation } from "@/components/Navigation";
    import { OverviewTable, OverviewStatus, YearMatrixTable } from "@/components";
    import { GAME_ARCHIVE } from '@/store-names';

    export default {
        components: {
            TwoColumn,
            BunchNavigation,
            CashgameNavigation,
            OverviewTable,
            OverviewStatus,
            YearMatrixTable
        },
        mixins: [
            DataMixin
        ],
        computed: {
            ...mapGetters(GAME_ARCHIVE, [
                'currentYear',
                'hasGames'
            ])
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

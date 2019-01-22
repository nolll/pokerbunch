<template>
    <two-column :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <template slot="content-nav">
            <cashgame-navigation page="facts" />
        </template>

        <template slot="aside">
            <page-section>
                <overall-facts />
            </page-section>
        </template>

        <template slot="main">
            <page-section>
                <single-game-facts />
                <total-facts />
            </page-section>
        </template>
    </two-column>
</template>

<script>
    import { DataMixin } from '@/mixins';
    import { TwoColumn } from '@/components/Layouts';
    import { BunchNavigation, CashgameNavigation } from '@/components/Navigation';
    import { SingleGameFacts, TotalFacts, OverallFacts } from '@/components/Facts';
    import { PageSection } from '@/components/Common';

    export default {
        components: {
            TwoColumn,
            BunchNavigation,
            CashgameNavigation,
            SingleGameFacts,
            TotalFacts,
            OverallFacts,
            PageSection
        },
        mixins: [
            DataMixin
        ],
        computed: {
            ready() {
                return this.bunchReady && this.gamesReady;
            }
        },
        methods: {
            init() {
                this.loadUser();
                this.loadBunch();
                this.loadGames();
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

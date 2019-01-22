<template>
    <two-column :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <template slot="content-nav">
            <cashgame-navigation page="chart" />
        </template>

        <template slot="main">
            <page-section>
                <cashgame-chart />
            </page-section>
        </template>
    </two-column>
</template>

<script>
    import { DataMixin } from '@/mixins';
    import { TwoColumn } from '@/components/Layouts';
    import { BunchNavigation, CashgameNavigation } from '@/components/Navigation';
    import { CashgameChart } from '@/components';
    import { PageSection } from '@/components/Common';

    export default {
        components: {
            TwoColumn,
            BunchNavigation,
            CashgameNavigation,
            CashgameChart,
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

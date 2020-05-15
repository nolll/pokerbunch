<template>
    <layout :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <page-section>
            <block>
                <cashgame-navigation page="chart" />
            </block>
        </page-section>

        <page-section>
            <block>
                <cashgame-chart />
            </block>
        </page-section>
    </layout>
</template>

<script>
    import { DataMixin } from '@/mixins';
    import { Layout } from '@/components/Layouts';
    import { BunchNavigation, CashgameNavigation } from '@/components/Navigation';
    import { CashgameChart } from '@/components';
    import { Block, PageSection } from '@/components/Common';

    export default {
        components: {
            Layout,
            BunchNavigation,
            CashgameNavigation,
            CashgameChart,
            Block,
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
                this.requireUser();
                this.loadBunch();
                this.loadGames();
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

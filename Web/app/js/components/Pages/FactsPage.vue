<template>
    <layout :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <page-section>
            <block>
                <cashgame-navigation page="facts" />
            </block>
        </page-section>

        <page-section>
            <block>
                <single-game-facts />
            </block><block>
                <total-facts />
            </block>
            <template slot="aside2">
                <block>
                    <overall-facts />
                </block>
            </template>
        </page-section>

        <template slot="main">
            <page-section>
            </page-section>
        </template>
    </layout>
</template>

<script>
    import { DataMixin } from '@/mixins';
    import { Layout } from '@/components/Layouts';
    import { BunchNavigation, CashgameNavigation } from '@/components/Navigation';
    import { SingleGameFacts, TotalFacts, OverallFacts } from '@/components/Facts';
    import { Block, PageSection } from '@/components/Common';

    export default {
        components: {
            Layout,
            BunchNavigation,
            CashgameNavigation,
            SingleGameFacts,
            TotalFacts,
            OverallFacts,
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

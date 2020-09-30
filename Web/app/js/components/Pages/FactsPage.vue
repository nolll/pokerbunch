<template>
    <Layout :ready="ready">
        <template slot="top-nav">
            <BunchNavigation />
        </template>

        <PageSection>
            <Block>
                <CashgameNavigation page="facts" />
            </Block>
        </PageSection>

        <PageSection>
            <Block>
                <SingleGameFacts />
            </Block>
            <Block>
                <TotalFacts />
            </Block>
            <template slot="aside2">
                <Block>
                    <OverallFacts />
                </Block>
            </template>
        </PageSection>

        <template slot="main">
            <PageSection>
            </PageSection>
        </template>
    </Layout>
</template>

<script lang="ts">
    import { Component, Mixins, Watch } from 'vue-property-decorator';
    import { BunchMixin, UserMixin, GameArchiveMixin } from '@/mixins';
    import Layout from '@/components/Layouts/Layout.vue';
    import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
    import CashgameNavigation from '@/components/Navigation/CashgameNavigation.vue';
    import SingleGameFacts from '@/components/Facts/SingleGameFacts.vue';
    import TotalFacts from '@/components/Facts/TotalFacts.vue';
    import OverallFacts from '@/components/Facts/OverallFacts.vue';
    import Block from '@/components/Common/Block.vue';
    import PageSection from '@/components/Common/PageSection.vue';

    @Component({
        components: {
            Layout,
            BunchNavigation,
            CashgameNavigation,
            SingleGameFacts,
            TotalFacts,
            OverallFacts,
            Block,
            PageSection
        }
    })
    export default class FactsPage extends Mixins(
        BunchMixin,
        UserMixin,
        GameArchiveMixin
    ) {
        get ready() {
            return this.$_bunchReady && this.$_gamesReady;
        }

        init() {
            this.$_requireUser();
            this.$_loadBunch();
            this.$_loadGames();
        }

        mounted() {
            this.init();
        }

        @Watch('$route')
        routeChanged() {
            this.init();
        }
    }
</script>

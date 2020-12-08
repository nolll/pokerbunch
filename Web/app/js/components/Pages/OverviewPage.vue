<template>
    <Layout :ready="ready">
        <template slot="top-nav">
            <BunchNavigation />
        </template>

        <PageSection>
            <Block>
                <CashgameNavigation page="index" />
            </Block>
        </PageSection>

        <PageSection>
            <template slot="aside1">
                <OverviewStatus />
            </template>
            <Block>
                <PageHeading text="Current Rankings" />
                <OverviewTable v-if="$_hasGames" />
                <p v-else>The rankings will be displayed here when you have played your first game.</p>
            </Block>
        </PageSection>

        <PageSection :is-wide="false">
            <Block>
                <PageHeading text="Yearly Rankings" />
                <YearMatrixTable v-if="$_hasGames" />
            </Block>
        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Mixins, Watch } from 'vue-property-decorator';
    import { BunchMixin, UserMixin, GameArchiveMixin, CurrentGameMixin } from '@/mixins';
    import Layout from '@/components/Layouts/Layout.vue';
    import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
    import CashgameNavigation from '@/components/Navigation/CashgameNavigation.vue';
    import OverviewTable from '@/components/Overview/OverviewTable.vue';
    import OverviewStatus from '@/components/Overview/OverviewStatus.vue';
    import YearMatrixTable from '@/components/YearMatrix/YearMatrixTable.vue';
    import Block from '@/components/Common/Block.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';

    @Component({
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
        }
    })
    export default class OverviewPage extends Mixins(
        BunchMixin,
        UserMixin,
        GameArchiveMixin,
        CurrentGameMixin
    ) {
        get ready() {
            return this.$_bunchReady && this.$_gamesReady && this.$_currentGamesReady;
        }

        init() {
            this.$_requireUser();
            this.$_loadBunch();
            this.$_loadGames();
            this.$_loadCurrentGames();
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

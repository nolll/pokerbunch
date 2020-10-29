<template>
    <Layout :ready="ready">
        <template slot="top-nav">
            <BunchNavigation />
        </template>

        <PageSection>
            <Block>
                <CashgameNavigation page="matrix" />
            </Block>
        </PageSection>

        <PageSection>
            <Block>
                <MatrixTable :slug="slug" :games="games" :players="players" />
            </Block>
        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Mixins, Watch } from 'vue-property-decorator';
    import { BunchMixin, UserMixin, GameArchiveMixin } from '@/mixins';
    import Layout from '@/components/Layouts/Layout.vue';
    import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
    import CashgameNavigation from '@/components/Navigation/CashgameNavigation.vue';
    import MatrixTable from '@/components/Matrix/MatrixTable.vue';
    import Block from '@/components/Common/Block.vue';
    import PageSection from '@/components/Common/PageSection.vue';

    @Component({
        components: {
            Layout,
            BunchNavigation,
            CashgameNavigation,
            MatrixTable,
            Block,
            PageSection
        }
    })
    export default class MatrixPage extends Mixins(
        BunchMixin,
        UserMixin,
        GameArchiveMixin
    ) {
        get slug(){
            return this.$_slug;
        }

        get games(){
            return this.$_sortedGames;
        }

        get players(){
            return this.$_sortedPlayers;
        }

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

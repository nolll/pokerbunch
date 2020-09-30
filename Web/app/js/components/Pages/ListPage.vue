<template>
    <Layout :ready="ready">
        <template slot="top-nav">
            <BunchNavigation />
        </template>

        <PageSection>
            <Block>
                <CashgameNavigation page="list" />
            </Block>
            <Block>
                <GameListTable />
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
    import GameListTable from '@/components/GameList/GameListTable.vue';
    import Block from '@/components/Common/Block.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    
    @Component({
        components: {
            Layout,
            BunchNavigation,
            CashgameNavigation,
            GameListTable,
            Block,
            PageSection
        }
    })
    export default class ListPage extends Mixins(
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

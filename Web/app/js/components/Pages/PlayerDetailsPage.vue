<template>
    <Layout :ready="ready">
        <template slot="top-nav">
            <BunchNavigation />
        </template>

        <PageSection>
            <Block>
                <PageHeading :text="playerName" />
            </Block>
        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Mixins, Watch } from 'vue-property-decorator';
    import { BunchMixin, PlayerMixin, UserMixin } from '@/mixins';
    import urls from '@/urls';
    import Layout from '@/components/Layouts/Layout.vue';
    import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
    import Block from '@/components/Common/Block.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import { Player } from '@/models/Player';
    
    @Component({
        components: {
            Layout,
            BunchNavigation,
            Block,
            PageHeading,
            PageSection,
            CustomLink
        }
    })
    export default class PlayerDetailsPage extends Mixins(
        BunchMixin,
        PlayerMixin,
        UserMixin
    ) {
        player: Player | null = null

        get playerName(){
            return this.player?.name;
        }

        get ready() {
            return this.player != null;
        }

        async init() {
            this.$_requireUser();
            this.$_loadBunch();
            await this.$_loadPlayers();
            this.player = this.$_getPlayer(this.$route.params.id);
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

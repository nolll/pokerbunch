<template>
    <Layout :ready="ready">
        <template slot="top-nav">
            <BunchNavigation />
        </template>

        <PageSection>
            <template slot="aside1">
                <Block>
                    <CustomButton :url="addPlayerUrl" type="action" text="Add player" />
                </Block>
            </template>

            <Block>
                <PageHeading text="Players" />
            </Block>

            <Block>
                <PlayerList :bunchId="slug" />
            </Block>
        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Mixins, Watch } from 'vue-property-decorator';
    import { BunchMixin, PlayerMixin, UserMixin } from '@/mixins';
    import Layout from '@/components/Layouts/Layout.vue';
    import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
    import PlayerList from '@/components/PlayerList/PlayerList.vue';
    import Block from '@/components/Common/Block.vue';
    import CustomButton from '@/components/Common/CustomButton.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import urls from '@/urls';
    
    @Component({
        components: {
            Layout,
            BunchNavigation,
            PlayerList,
            CustomButton,
            Block,
            PageHeading,
            PageSection
        }
    })
    export default class PlayerListPage extends Mixins(
        BunchMixin,
        PlayerMixin,
        UserMixin
    ) {

        get addPlayerUrl() {
            return urls.player.add(this.slug);
        }

        get slug(){
            return this.$_slug;
        }

        get ready() {
            return this.$_bunchReady && this.$_playersReady;
        }

        init() {
            this.$_requireUser();
            this.$_loadBunch();
            this.$_loadPlayers();
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

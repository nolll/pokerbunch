<template>
    <layout :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <page-section>
            <template slot="aside1">
                <block>
                    <custom-button :url="addPlayerUrl" type="action" text="Add player" />
                </block>
            </template>

            <block>
                <page-heading text="Players" />
            </block>

            <block>
                <player-list />
            </block>
        </page-section>
    </layout>
</template>

<script>
    import { BunchMixin, PlayerMixin, UserMixin } from '@/mixins';
    import { Layout } from '@/components/Layouts';
    import { BunchNavigation } from '@/components/Navigation';
    import { PlayerList } from '@/components';
    import { Block, CustomButton, PageHeading, PageSection } from '@/components/Common';
    import urls from '@/urls';

    export default {
        components: {
            Layout,
            BunchNavigation,
            PlayerList,
            CustomButton,
            Block,
            PageHeading,
            PageSection
        },
        mixins: [
            BunchMixin,
            PlayerMixin,
            UserMixin
        ],
        computed: {
            addPlayerUrl() {
                return urls.player.add(this.$_slug);
            },
            ready() {
                return this.$_bunchReady && this.$_playersReady;
            }
        },
        methods: {
            init() {
                this.$_requireUser();
                this.$_loadBunch();
                this.$_loadPlayers();
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

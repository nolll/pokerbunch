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
    import { mapGetters } from 'vuex';
    import { DataMixin } from '@/mixins';
    import { Layout } from '@/components/Layouts';
    import { BunchNavigation } from '@/components/Navigation';
    import { PlayerList } from '@/components';
    import { Block, CustomButton, PageHeading, PageSection } from '@/components/Common';
    import urls from '@/urls';
    import { BUNCH } from '@/store-names';

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
            DataMixin
        ],
        computed: {
            ...mapGetters(BUNCH, [
                'slug'
            ]),
            addPlayerUrl() {
                return urls.player.add(this.slug);
            },
            ready() {
                return this.bunchReady && this.playersReady;
            }
        },
        methods: {
            init() {
                this.requireUser();
                this.loadBunch();
                this.loadPlayers();
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

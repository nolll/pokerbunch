<template>
    <two-column :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <template slot="aside">
            <page-section>
                <custom-button :url="addPlayerUrl" type="action" text="Add player" />
            </page-section>
        </template>

        <template slot="main">
            <page-section>
                <page-heading text="Players" />
            </page-section>
            <page-section>
                <player-list />
            </page-section>
        </template>
    </two-column>
</template>

<script>
    import { mapGetters } from 'vuex';
    import { DataMixin } from '@/mixins';
    import { TwoColumn } from '@/components/Layouts';
    import { BunchNavigation } from '@/components/Navigation';
    import { PlayerList } from '@/components';
    import { CustomButton, PageHeading, PageSection } from '@/components/Common';
    import urls from '@/urls';
    import { BUNCH } from '@/store-names';

    export default {
        components: {
            TwoColumn,
            BunchNavigation,
            PlayerList,
            CustomButton,
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
                return urls.addPlayer(this.slug);
            },
            ready() {
                return this.bunchReady;
            }
        },
        methods: {
            init() {
                this.loadUser();
                this.loadBunch();
            }
        },
        watch: {
            '$route'(to, from) {
                this.init();
            }
        },
        created: function () {
            this.init();
        }
    };
</script>

<style>

</style>

<template>
    <layout :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <page-section>
            <block>
                <cashgame-navigation page="matrix" />
            </block>
        </page-section>

        <page-section>
            <block>
                <matrix-table />
            </block>
        </page-section>
    </layout>
</template>

<script>
    import { BunchMixin, UserMixin, GameArchiveMixin } from '@/mixins';
    import { Layout } from '@/components/Layouts';
    import { BunchNavigation, CashgameNavigation } from '@/components/Navigation';
    import { MatrixTable } from '@/components';
    import { Block, PageSection } from '@/components/Common';

    export default {
        components: {
            Layout,
            BunchNavigation,
            CashgameNavigation,
            MatrixTable,
            Block,
            PageSection
        },
        mixins: [
            BunchMixin,
            UserMixin,
            GameArchiveMixin
        ],
        computed: {
            ready() {
                return this.$_bunchReady && this.$_gamesReady;
            }
        },
        methods: {
            init() {
                this.$_requireUser();
                this.$_loadBunch();
                this.$_loadGames();
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

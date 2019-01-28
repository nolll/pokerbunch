<template>
    <two-column :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <template slot="content-nav">
            <cashgame-navigation page="toplist" />
        </template>

        <template slot="main">
            <page-section>
                <top-list-table />
            </page-section>
        </template>
    </two-column>
</template>

<script>
    import { DataMixin } from '@/mixins';
    import { TwoColumn } from '@/components/Layouts';
    import { BunchNavigation, CashgameNavigation } from '@/components/Navigation';
    import { TopListTable } from '@/components';
    import { PageSection } from '@/components/Common';

    export default {
        components: {
            TwoColumn,
            BunchNavigation,
            CashgameNavigation,
            TopListTable,
            PageSection
        },
        mixins: [
            DataMixin
        ],
        computed: {
            ready() {
                return this.bunchReady && this.gamesReady;
            }
        },
        methods: {
            init() {
                this.loadUser();
                this.loadBunch();
                this.loadGames();
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

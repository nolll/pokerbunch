<template>
    <two-column :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <template slot="content-nav">
            <cashgame-navigation page="list" />
        </template>

        <template slot="main">
            <page-section>
                <game-list-table />
            </page-section>
        </template>
    </two-column>
</template>

<script>
    import { DataMixin } from '@/mixins';
    import { TwoColumn } from '@/components/Layouts';
    import { BunchNavigation, CashgameNavigation } from '@/components/Navigation';
    import { GameListTable } from '@/components';
    import { PageSection } from '@/components/Common';

    export default {
        components: {
            TwoColumn,
            BunchNavigation,
            CashgameNavigation,
            GameListTable,
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
        created: function () {
            this.init();
        }
    };
</script>

<style>

</style>

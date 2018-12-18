<template>
    <two-column :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <template slot="content-nav">
            <cashgame-navigation page="toplist" />
        </template>

        <template slot="main">
            <div class="block gutter">
                <top-list-table />
            </div>
        </template>
    </two-column>
</template>

<script>
    import { DataMixin } from '@/mixins';
    import { TwoColumn } from "@/components/Layouts";
    import { BunchNavigation, CashgameNavigation } from "@/components/Navigation";
    import { TopListTable } from "@/components";

    export default {
        components: {
            TwoColumn,
            BunchNavigation,
            CashgameNavigation,
            TopListTable
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

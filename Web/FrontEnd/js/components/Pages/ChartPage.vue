<template>
    <two-column>
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <template slot="content-nav">
            <cashgame-navigation page="chart" />
        </template>

        <template slot="main">
            <div class="block gutter">
                <cashgame-chart />
            </div>
        </template>
    </two-column>
</template>

<script>
    import { DataMixin } from '@/mixins';
    import { TwoColumn } from "../Layouts";
    import { BunchNavigation, CashgameNavigation } from "../Navigation";
    import { CashgameChart } from "../";

    export default {
        components: {
            TwoColumn,
            BunchNavigation,
            CashgameNavigation,
            CashgameChart
        },
        mixins: [
            DataMixin
        ],
        methods: {
            init() {
                this.loadUser();
                this.loadBunch();
                this.loadGames();
                this.loadCurrentGame();
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

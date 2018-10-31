<template>
    <two-column>
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <template slot="content-nav">
            <cashgame-navigation page="facts" />
        </template>

        <template slot="aside">
            <div class="block gutter">
                <overall-facts />
            </div>
        </template>

        <template slot="main">
            <div class="block gutter">
                <single-game-facts />
                <total-facts />
            </div>
        </template>
    </two-column>
</template>

<script>
    import { DataMixin } from '../../mixins';
    import { TwoColumn } from "../Layouts";
    import { BunchNavigation, CashgameNavigation } from "../Navigation";
    import { SingleGameFacts, TotalFacts, OverallFacts } from "../";

    export default {
        components: {
            TwoColumn,
            BunchNavigation,
            CashgameNavigation,
            SingleGameFacts,
            TotalFacts,
            OverallFacts
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

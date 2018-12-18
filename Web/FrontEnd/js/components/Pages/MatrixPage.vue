<template>
    <two-column>
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <template slot="content-nav">
            <cashgame-navigation page="matrix" />
        </template>

        <template slot="main">
            <div class="block gutter">
                <matrix-table />
            </div>
        </template>
    </two-column>
</template>

<script>
    import { DataMixin } from '@/mixins';
    import { TwoColumn } from "@/components/Layouts";
    import { BunchNavigation, CashgameNavigation } from "@/components/Navigation";
    import { MatrixTable } from "@/components";

    export default {
        components: {
            TwoColumn,
            BunchNavigation,
            CashgameNavigation,
            MatrixTable
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

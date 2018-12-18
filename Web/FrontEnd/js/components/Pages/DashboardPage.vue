<template>
    <two-column>
        <template slot="aside">
            <div class="gutter">
                <game-chart :players="players"></game-chart>
            </div>
        </template>

        <template slot="main">
            <div v-if="initialized" class="region width2">
                <div class="block gutter">
                    <h1 class="page-heading">Running Cashgame</h1>
                </div>

                <div class="block gutter" v-if="!hasPlayers">
                    No one has joined the game yet.
                </div>

                <div class="standings block gutter" v-if="hasPlayers">
                    <player-table :players="sortedPlayers" :currency-format="currencyFormat"></player-table>
                </div>

                <div class="block gutter">
                    <dl class="value-list">
                        <dt class="value-list__key" v-if="hasPlayers">Start Time</dt>
                        <dd class="value-list__value" v-if="hasPlayers">{{formattedStartTime}}</dd>
                        <dt class="value-list__key">Location</dt>
                        <dd class="value-list__value">{{locationName}}</dd>
                    </dl>
                </div>
            </div>
            <spinner v-else></spinner>
        </template>
    </two-column>
</template>

<script>
    import { DataMixin } from '@/mixins';
    import { TwoColumn } from "@/components/Layouts";
    import { mapGetters, mapState } from 'vuex';
    import { PlayerTable, GameChart } from "@/components/CurrentGame";
    import { Spinner } from "@/components/Common";
    import { CURRENT_GAME } from '@/store-names';

    export default {
        components: {
            TwoColumn,
            PlayerTable,
            GameChart,
            Spinner
        },
        mixins: [
            DataMixin
        ],
        props: ['slug'],
        created: function () {
            this.init();
        },
        computed: {
            ...mapState(CURRENT_GAME, [
                'initialized',
                'players',
                'locationName',
                'currencyFormat'
            ]),
            ...mapGetters(CURRENT_GAME, [
                'startTime',
                'sortedPlayers',
                'hasPlayers'
            ]),
            hasPlayers() {
                return this.players.length > 0;
            },
            formattedStartTime() {
                return this.startTime.format('HH:mm');
            }
        },
        methods: {
            loadCurrentGame() {
                const slug = this.slug;
                this.$store.dispatch('currentGame/loadCurrentGame', { slug });
            },
            init() {
                this.loadUser();
                this.loadBunch();
                this.loadCurrentGame();
            }
        },
    };
</script>

<style>
</style>

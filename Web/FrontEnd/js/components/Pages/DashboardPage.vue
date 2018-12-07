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
    import { DataMixin } from '../../mixins';
    import { TwoColumn } from "../Layouts";
    import { mapGetters, mapState } from 'vuex';
    import { PlayerTable, GameChart } from "../CurrentGame";
    import { Spinner } from "../Common";

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
            ...mapState('currentGame', {
                initialized: state => state.initialized,
                players: state => state.players,
                locationName: state => state.locationName,
                currencyFormat: state => state.currencyFormat,
            }),
            ...mapGetters('currentGame', {
                startTime: getters => getters.startTime,
                sortedPlayers: getters => getters.sortedPlayers,
                hasPlayers: getters => getters.hasPlayers
            }),
            hasPlayers: function () {
                return this.players.length > 0;
            },
            formattedStartTime: function () {
                return this.startTime.format('HH:mm');
            }
        },
        methods: {
            loadCurrentGame: function () {
                const slug = this.slug;
                this.$store.dispatch('currentGame/loadCurrentGame', { slug });
            },
            init: function () {
                this.loadUser();
                this.loadBunch();
                this.loadCurrentGame();
            }
        },
    };
</script>

<style>
</style>

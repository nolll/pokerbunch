<template>
    <div>
        <div v-if="initialized">
            <div class="region width3">
                <div class="block gutter">
                    <h1 class="page-heading">Running Cashgame</h1>
                </div>

                <div class="block gutter" v-if="!hasPlayers">
                    No one has joined the game yet.
                </div>
            </div>
            <div class="region width1">
                <div class="standings block gutter" v-if="hasPlayers">
                    <player-table v-bind:players="sortedPlayers" v-bind:currency-format="currencyFormat"></player-table>
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

            <div class="region width2 aside2">
                <div class="block gutter" v-if="hasPlayers">
                    <game-chart v-bind:players="players"></game-chart>
                </div>
            </div>
        </div>
        <spinner v-else></spinner>
    </div>
</template>

<script>
    import { mapGetters, mapState } from 'vuex';
    import { PlayerTable, GameChart } from ".";
    import { Spinner } from "../Common";

    var longRefresh = 30000,
        shortRefresh = 10000;

    export default {
        components: {
            PlayerTable,
            GameChart,
            Spinner
        },
        props: ['slug'],
        mounted: function () {
            var self = this;
            self.$nextTick(function () {
                self.loadCurrentGame(self.slug);
            });
        },
        computed: {
            ...mapState('currentGame', [
                'initialized',
                'players',
                'locationName',
                'currencyFormat',
            ]),
            ...mapGetters('currentGame', [
                'startTime',
                'sortedPlayers',
                'hasPlayers'
            ]),
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
            }
        }
    };
</script>

<style>
</style>

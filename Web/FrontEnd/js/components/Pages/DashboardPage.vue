<template>
    <two-column :ready="ready">
        <template slot="aside">
            <div class="gutter">
                <game-chart :players="players"></game-chart>
            </div>
        </template>

        <template slot="main">
            <page-section>
                <page-heading text="Running Cashgame" />
            </page-section>

            <page-section v-if="!hasPlayers">
                No one has joined the game yet.
            </page-section>

            <page-section>
                <div class="standings" v-if="hasPlayers">
                    <player-table :players="sortedPlayers" :currency-format="currencyFormat"></player-table>
                </div>
            </page-section>

            <page-section>
                <dl class="value-list">
                    <dt class="value-list__key" v-if="hasPlayers">Start Time</dt>
                    <dd class="value-list__value" v-if="hasPlayers">{{formattedStartTime}}</dd>
                    <dt class="value-list__key">Location</dt>
                    <dd class="value-list__value">{{locationName}}</dd>
                </dl>
            </page-section>
        </template>
    </two-column>
</template>

<script>
    import { DataMixin } from '@/mixins';
    import { TwoColumn } from '@/components/Layouts';
    import { mapGetters } from 'vuex';
    import { PlayerTable, GameChart } from '@/components/CurrentGame';
    import { PageHeading, PageSection, Spinner } from '@/components/Common';
    import { CURRENT_GAME } from '@/store-names';

    export default {
        components: {
            TwoColumn,
            PlayerTable,
            GameChart,
            PageHeading,
            PageSection,
            Spinner
        },
        mixins: [
            DataMixin
        ],
        mounted: function () {
            this.init();
        },
        computed: {
            ...mapGetters(CURRENT_GAME, [
                'locationName',
                'startTime',
                'sortedPlayers',
                'hasPlayers',
                'players'
            ]),
            hasPlayers() {
                return this.players.length > 0;
            },
            formattedStartTime() {
                return this.startTime.format('HH:mm');
            },
            ready() {
                return this.bunchReady && this.currentGameReady;
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

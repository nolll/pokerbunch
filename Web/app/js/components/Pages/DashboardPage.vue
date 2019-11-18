<template>
    <layout :ready="ready">
        <page-section>
            <block>
                <page-heading text="Running Cashgame" />
            </block>

            <block v-if="!hasPlayers">
                No one has joined the game yet.
            </block>

            <block>
                <div class="standings" v-if="hasPlayers">
                    <player-table :players="sortedPlayers" :currency-format="currencyFormat"></player-table>
                </div>
            </block>

            <block>
                <dl class="value-list">
                    <dt class="value-list__key" v-if="hasPlayers">Start Time</dt>
                    <dd class="value-list__value" v-if="hasPlayers">{{formattedStartTime}}</dd>
                    <dt class="value-list__key">Location</dt>
                    <dd class="value-list__value">{{locationName}}</dd>
                </dl>
            </block>

            <template slot="aside2">
                <block>
                    <game-chart :players="players"></game-chart>
                </block>
            </template>
        </page-section>
    </layout>
</template>

<script>
    import { DataMixin } from '@/mixins';
    import { Layout } from '@/components/Layouts';
    import { mapGetters } from 'vuex';
    import { PlayerTable, GameChart } from '@/components/CurrentGame';
    import { Block, PageHeading, PageSection, Spinner } from '@/components/Common';
    import { CASHGAME } from '@/store-names';

    export default {
        components: {
            Layout,
            PlayerTable,
            GameChart,
            Block,
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
            ...mapGetters(CASHGAME, [
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
                this.$store.dispatch('cashgame/loadCurrentGame', { slug });
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

<template>
    <two-column>
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <template slot="aside">
            <div class="block gutter">
                <dl class="value-list">
                    <dt class="value-list__key" v-if="hasPlayers">Start Time</dt>
                    <dd class="value-list__value" v-if="hasPlayers">{{formattedStartTime}}</dd>
                    <dt class="value-list__key">Location</dt>
                    <dd class="value-list__value"><a :href="locationUrl">{{locationName}}</a></dd>
                    <dt class="value-list__key" v-if="isManager">Player</dt>
                    <dd class="value-list__value" v-if="isManager">
                        <player-dropdown :player-id="playerId" :players="bunchPlayers"></player-dropdown>
                    </dd>
                </dl>
            </div>
        </template>

        <template slot="main">
            <div v-if="initialized" class="region width2">
                <div class="block gutter">
                    <h1 class="page-heading">Running Cashgame</h1>
                </div>

                <div class="button-list" v-if="areButtonsVisible">
                    <game-button text="Report" icon="reorder" v-show="canReport" v-on:click.native="showReportForm"></game-button>
                    <game-button text="Buy In" icon="money" v-show="canBuyin" v-on:click.native="showBuyinForm"></game-button>
                    <game-button text="Cash Out" icon="signout" v-show="canCashout" v-on:click.native="showCashoutForm"></game-button>
                </div>

                <div class="block gutter">
                    <report-form v-show="reportFormVisible" :is-active="reportFormVisible"></report-form>
                    <buyin-form v-show="buyinFormVisible" :is-active="buyinFormVisible"></buyin-form>
                    <cashout-form v-show="cashoutFormVisible" :is-active="cashoutFormVisible"></cashout-form>
                </div>

                <div class="standings block gutter" v-if="hasPlayers">
                    <player-table :players="sortedPlayers"></player-table>
                </div>
                <div class="block gutter" v-else>
                    No one has joined the game yet.
                </div>

                <div class="block gutter" v-if="hasPlayers">
                    <game-chart :players="players"></game-chart>
                </div>
            </div>
            <spinner v-else></spinner>
        </template>
    </two-column>
</template>

<script>
    import { DataMixin } from '@/mixins';
    import { mapState, mapGetters } from 'vuex';
    import urls from '../../urls';
    import { TwoColumn } from "../Layouts";
    import { BunchNavigation } from "../Navigation";
    import { GameButton, ReportForm, BuyinForm, CashoutForm, PlayerDropdown, PlayerTable, GameChart } from "../CurrentGame";
    import { Spinner } from "../Common";
    import { CURRENT_GAME } from '@/store-names';

    export default {
        components: {
            TwoColumn,
            BunchNavigation,
            GameButton,
            ReportForm,
            BuyinForm,
            CashoutForm,
            PlayerDropdown,
            PlayerTable,
            GameChart,
            Spinner
        },
        mixins: [
            DataMixin
        ],
        props: {
            apiHost: {
                type: String
            }
        },
        created: function () {
            this.init();
        },
        computed: {
            ...mapState(CURRENT_GAME, {
                slug: state => state.slug,
                players: state => state.players,
                bunchPlayers: state => state.bunchPlayers,
                initialized: state => state.initialized,
                locationUrl: state => state.locationUrl,
                locationName: state => state.locationName,
                isManager: state => state.isManager,
                currencyFormat: state => state.currencyFormat,
                playerId: state => state.playerId,
                currentStack: state => state.currentStack,
                reportFormVisible: state => state.reportFormVisible,
                buyinFormVisible: state => state.buyinFormVisible,
                cashoutFormVisible: state => state.cashoutFormVisible
            }),
            ...mapGetters(CURRENT_GAME, {
                hasPlayers: getters => getters.hasPlayers,
                startTime: getters => getters.startTime,
                sortedPlayers: getters => getters.sortedPlayers,
                isInGame: getters => getters.isInGame,
                hasCashedOut: getters => getters.hasCashedOut,
                canCashout: getters => getters.canCashout,
                canEndGame: getters => getters.canEndGame,
                canReport: getters => getters.canReport,
                canBuyin: getters => getters.canBuyin
            }),
            formattedStartTime: function () {
                return this.startTime.format('HH:mm');
            },
            areButtonsVisible: function () {
                return !this.isAnyFormVisible;
            },
            isAnyFormVisible: function () {
                return this.reportFormVisible || this.buyinFormVisible || this.cashoutFormVisible;
            }
        },
        methods: {
            showReportForm: function () {
                this.$store.dispatch('currentGame/showReportForm');
            },
            showBuyinForm: function () {
                this.$store.dispatch('currentGame/showBuyinForm');
            },
            showCashoutForm: function () {
                this.$store.dispatch('currentGame/showCashoutForm');
            },
            redirectIfGameHasEnded: function () {
                if (this.canEndGame) {
                    this.redirect();
                }
            },
            redirect: function () {
                location.href = urls.cashgameIndex(this.slug);
            },
            init: function () {
                this.loadUser();
                this.loadBunch();
                this.loadCurrentGame();
            }
        }
    };
</script>

<style>

</style>

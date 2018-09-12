<template>
    <div>
        <div v-if="initialized" class="region width2">
            <div class="block gutter">
                <h1 class="page-heading">Running Cashgame</h1>
            </div>

            <div class="button-list" v-if="areButtonsVisible">
                <game-button text="Report" icon="reorder" v-if="canReport" v-on:click.native="showReportForm"></game-button>
                <game-button text="Buy In" icon="money" v-if="canBuyin" v-on:click.native="showBuyinForm"></game-button>
                <game-button text="Cash Out" icon="signout" v-if="canCashout" v-on:click.native="showCashoutForm"></game-button>
            </div>

            <div class="block gutter">
                <report-form v-if="reportFormVisible" v-bind:is-active="reportFormVisible"></report-form>
                <buyin-form v-if="buyinFormVisible" v-bind:is-active="buyinFormVisible" v-bind:is-in-game="isInGame"></buyin-form>
                <cashout-form v-if="cashoutFormVisible" v-bind:is-active="cashoutFormVisible"></cashout-form>
            </div>

            <div class="standings block gutter" v-if="hasPlayers">
                <player-table v-bind:players="sortedPlayers" v-bind:currency-format="currencyFormat"></player-table>
            </div>
            <div class="block gutter" v-else>
                No one has joined the game yet.
            </div>

            <div class="block gutter" v-if="hasPlayers">
                <game-chart v-bind:players="players"></game-chart>
            </div>
        </div>
        <spinner v-else></spinner>

        <div class="region width1 aside2">
            <div class="block gutter">
                <dl class="value-list">
                    <dt class="value-list__key" v-if="hasPlayers">Start Time</dt>
                    <dd class="value-list__value" v-if="hasPlayers">{{startTime}}</dd>
                    <dt class="value-list__key">Location</dt>
                    <dd class="value-list__value"><a v-bind:href="locationUrl">{{locationName}}</a></dd>
                    <dt class="value-list__key" v-if="isManager">Player</dt>
                    <dd class="value-list__value" v-if="isManager">
                        <player-dropdown v-bind:player-id="playerId" v-bind:players="bunchPlayers"></player-dropdown>
                    </dd>
                </dl>
            </div>
        </div>
    </div>
</template>

<script>
    import moment from 'moment';
    import { GameButton, ReportForm, BuyinForm, CashoutForm, PlayerDropdown, PlayerTable, GameChart, Spinner } from ".";

    export default {
        components: {
            GameButton,
            ReportForm,
            BuyinForm,
            CashoutForm,
            PlayerDropdown,
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
            players: function () {
                return this.$store.state.currentGame.players;
            },
            bunchPlayers: function () {
                return this.$store.state.currentGame.bunchPlayers;
            },
            hasPlayers: function () {
                return this.$store.getters['currentGame/hasPlayers'];
            },
            startTime: function () {
                const t = this.$store.getters['currentGame/startTime'];
                return t.format('HH:mm');
            },
            sortedPlayers: function () {
                return this.$store.getters['currentGame/sortedPlayers'];
            },
            isInGame: function () {
                return this.$store.getters['currentGame/isInGame'];
            },
            hasCashedOut: function () {
                return this.$store.getters['currentGame/hasCashedOut'];
            },
            canCashout: function () {
                return this.$store.getters['currentGame/canCashout'];
            },
            canEndGame: function () {
                return this.$store.getters['currentGame/canEndGame'];
            },
            canReport: function () {
                return this.$store.getters['currentGame/canReport'];
            },
            canBuyin: function () {
                return this.$store.getters['currentGame/canBuyin'];
            },
            initialized: function () {
                return this.$store.state.currentGame.initialized;
            },
            locationUrl: function () {
                return this.$store.state.currentGame.locationUrl;
            },
            locationName: function () {
                return this.$store.state.currentGame.locationName;
            },
            isManager: function () {
                return this.$store.state.currentGame.isManager;
            },
            currencyFormat: function () {
                return this.$store.state.currentGame.currencyFormat;
            },
            playerId: function () {
                return this.$store.state.currentGame.playerId;
            },
            currentStack: function () {
                return this.$store.state.currentGame.currentStack;
            },
            reportFormVisible: function () {
                return this.$store.state.currentGame.reportFormVisible;
            },
            buyinFormVisible: function () {
                return this.$store.state.currentGame.buyinFormVisible;
            },
            cashoutFormVisible: function () {
                return this.$store.state.currentGame.cashoutFormVisible;
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
                location.href = this.cashgameIndexUrl;
            },
            loadCurrentGame: function () {
                const slug = this.slug;
                this.$store.dispatch('currentGame/loadCurrentGame', { slug });
            }
        }
    };
</script>

<style scoped>

</style>

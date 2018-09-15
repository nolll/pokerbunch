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
                <buyin-form v-if="buyinFormVisible" v-bind:is-active="buyinFormVisible"></buyin-form>
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
                    <dd class="value-list__value" v-if="hasPlayers">{{formattedStartTime}}</dd>
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
    import { mapState, mapGetters } from 'vuex';
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
            ...mapState('currentGame', [
                'players',
                'bunchPlayers',
                'initialized',
                'locationUrl',
                'locationName',
                'isManager',
                'currencyFormat',
                'playerId',
                'currentStack',
                'reportFormVisible',
                'buyinFormVisible',
                'cashoutFormVisible']),
            ...mapGetters('currentGame', [
                'hasPlayers',
                'startTime',
                'sortedPlayers',
                'isInGame',
                'hasCashedOut',
                'canCashout',
                'canEndGame',
                'canReport',
                'canBuyin']),
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
                location.href = this.cashgameIndexUrl;
            },
            loadCurrentGame: function () {
                const slug = this.slug;
                this.$store.dispatch('currentGame/loadCurrentGame', { slug });
            }
        }
    };
</script>

<style>

</style>

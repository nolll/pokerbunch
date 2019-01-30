<template>
    <two-column :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <template slot="aside">
            <page-section>
                <dl class="value-list">
                    <dt class="value-list__key" v-if="hasPlayers">Start Time</dt>
                    <dd class="value-list__value" v-if="hasPlayers">{{formattedStartTime}}</dd>
                    <dt class="value-list__key">Location</dt>
                    <dd class="value-list__value"><custom-link :url="locationUrl">{{locationName}}</custom-link></dd>
                    <dt class="value-list__key" v-if="isManager">Player</dt>
                    <dd class="value-list__value" v-if="isManager">
                        <player-dropdown />
                    </dd>
                </dl>
            </page-section>
        </template>

        <template slot="main">
            <page-section>
                <page-heading text="Running Cashgame" />
            </page-section>

            <div class="button-list" v-if="areButtonsVisible">
                <game-button text="Report" icon="reorder" v-show="canReport" v-on:click.native="showReportForm"></game-button>
                <game-button text="Buy In" icon="money" v-show="canBuyin" v-on:click.native="showBuyinForm"></game-button>
                <game-button text="Cash Out" icon="signout" v-show="canCashout" v-on:click.native="showCashoutForm"></game-button>
            </div>

            <page-section>
                <report-form v-show="reportFormVisible" :is-active="reportFormVisible"></report-form>
                <buyin-form v-show="buyinFormVisible" :is-active="buyinFormVisible"></buyin-form>
                <cashout-form v-show="cashoutFormVisible" :is-active="cashoutFormVisible"></cashout-form>
            </page-section>

            <page-section v-if="hasPlayers">
                <div class="standings">
                    <player-table :players="sortedPlayers"></player-table>
                </div>
            </page-section>

            <page-section v-else>
                No one has joined the game yet.
            </page-section>
        </template>

        <template slot="main-wide">
            <page-section v-if="hasPlayers">
                <game-chart :players="players" />
            </page-section>
        </template>
    </two-column>
</template>

<script>
    import { DataMixin } from '@/mixins';
    import { mapGetters } from 'vuex';
    import urls from '@/urls';
    import { TwoColumn } from '@/components/Layouts';
    import { BunchNavigation } from '@/components/Navigation';
    import { GameButton, ReportForm, BuyinForm, CashoutForm, PlayerDropdown, PlayerTable, GameChart } from '@/components/CurrentGame';
    import { CustomLink, PageHeading, PageSection, Spinner } from '@/components/Common';
    import { BUNCH, CURRENT_GAME, PLAYER } from '@/store-names';

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
            PageHeading,
            PageSection,
            Spinner,
            CustomLink
        },
        mixins: [
            DataMixin
        ],
        props: {
            apiHost: {
                type: String
            }
        },
        watch: {
            '$route'(to, from) {
                this.init();
            },
            canEndGame(to) {
                if (to) {
                    this.redirect();
                }
            },
            ready(to) {
                if (to && !this.isRunning) {
                    this.redirect();
                }
            }
        },
        mounted: function () {
            this.init();
        },
        computed: {
            ...mapGetters(BUNCH, {
                slug: 'slug',
            }),
            ...mapGetters(CURRENT_GAME, [
                'playerId',
                'locationUrl',
                'locationName',
                'reportFormVisible',
                'buyinFormVisible',
                'cashoutFormVisible',
                'isManager',
                'players',
                'hasPlayers',
                'startTime',
                'sortedPlayers',
                'isInGame',
                'hasCashedOut',
                'canCashout',
                'canEndGame',
                'canReport',
                'canBuyin',
                'isRunning'
            ]),
            ...mapGetters(PLAYER, {
                bunchPlayers: 'players'
            }),
            formattedStartTime() {
                return this.startTime.format('HH:mm');
            },
            areButtonsVisible() {
                return !this.isAnyFormVisible;
            },
            isAnyFormVisible() {
                return this.reportFormVisible || this.buyinFormVisible || this.cashoutFormVisible;
            },
            ready() {
                return this.bunchReady && this.currentGameReady;
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
            redirect: function () {
                this.$router.push(urls.cashgame.index(this.slug));
            },
            init: function () {
                this.loadUser();
                this.loadBunch();
                this.loadCurrentGame();
                this.loadPlayers();
            }
        }
    };
</script>

<style>
</style>

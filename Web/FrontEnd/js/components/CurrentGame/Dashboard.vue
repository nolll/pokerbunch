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
                        <dd class="value-list__value"><a v-bind:href="locationUrl">{{locationName}}</a></dd>
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
    import { mapGetters } from 'vuex';
    import ajax from '../../ajax';
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
        data: defaultData,
        props: ['url'],
        mounted: function () {
            this.initData(this.url);
        },
        computed: {
            ...mapGetters('currentGame', ['startTime', 'sortedPlayers']),
            hasPlayers: function () {
                return this.players.length > 0;
            },
            formattedStartTime: function () {
                return this.startTime.format('HH:mm');
            }
        },
        methods: {
            loadComplete: function (data) {
                this.slug = data.slug;
                this.playerId = data.playerId;
                this.refreshUrl = data.refreshUrl;
                this.reportUrl = data.reportUrl;
                this.buyinUrl = data.buyinUrl;
                this.cashoutUrl = data.cashoutUrl;
                this.endGameUrl = data.endGameUrl;
                this.cashgameIndexUrl = data.cashgameIndexUrl;
                this.locationUrl = data.locationUrl;
                this.defaultBuyin = data.defaultBuyin;
                this.locationName = data.locationName;
                this.isManager = data.isManager;
                this.bunchPlayers = data.bunchPlayers;
                this.players = data.players;
                this.buyinAmount = data.defaultBuyin;
                this.loadedPlayerId = data.playerId;
                this.initialized = true;
                this.setupRefresh(longRefresh);
            },
            loadError: function () {
                this.setupRefresh(shortRefresh);
            },
            initData: function (url) {
                ajax.get(url, this.loadComplete, this.loadError);
            },
            setupRefresh: function (refreshTimeout) {
                window.setTimeout(this.refresh, refreshTimeout);
            },
            getBunchPlayer: function () {
                var i,
                    bp = this.bunchPlayers;
                for (i = 0; i < bp.length; i++) {
                    if (bp[i].id === this.playerId) {
                        return bp[i];
                    }
                }
                return null;
            },
            refresh: function () {
                var callback = this.setPlayers;
                ajax.get(this.refreshUrl,
                    function (playerData) {
                        callback(playerData);
                    });
            },
            setPlayers: function (data) {
                this.players = data.players;
                this.setupRefresh(longRefresh);
            }
        }
    };

    function defaultData() {
        return {
            slug: '',
            playerId: 0,
            refreshUrl: '',
            reportUrl: '',
            buyinUrl: '',
            cashoutUrl: '',
            endGameUrl: '',
            cashgameIndexUrl: '',
            locationUrl: '',
            defaultBuyin: 0,
            locationName: '',
            isManager: false,
            bunchPlayers: [],
            players: [],
            currentStack: 0,
            beforeBuyinStack: 0,
            buyinAmount: 0,
            loadedPlayerId: 0,
            currencyFormat: '{0} kr',
            initialized: false
        }
    }
</script>

<style>

</style>

<template>
    <layout :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <page-section>
            <template slot="aside">
                <block>
                    <value-list>
                        <value-list-key v-if="showStartTime">Start Time</value-list-key>
                        <value-list-value v-if="showStartTime">{{formattedStartTime}}</value-list-value>
                        <value-list-key v-if="showEndTime">End Time</value-list-key>
                        <value-list-value v-if="showEndTime">{{formattedEndTime}}</value-list-value>
                        <value-list-key v-if="showDuration">Duration</value-list-key>
                        <value-list-value v-if="showDuration">{{formattedDuration}}</value-list-value>
                        <value-list-key>Location</value-list-key>
                        <value-list-value><custom-link :url="locationUrl">{{locationName}}</custom-link></value-list-value>
                        <value-list-key v-if="isPlayerSelectionVisible">Player</value-list-key>
                        <value-list-value v-if="isPlayerSelectionVisible"><player-dropdown /></value-list-value>
                    </value-list>
                </block>
                <block v-if="canEdit">
                    <custom-link :url="editUrl" cssClasses="button button--action">Edit Cashgame</custom-link>
                </block>
            </template>
            <block>
                <page-heading :text="title" />
            </block>
            <block class="button-list" v-if="areButtonsVisible">
                <game-button text="Report" icon="reorder" v-show="canReport" v-on:click.native="showReportForm" />
                <game-button text="Buy In" icon="money" v-show="canBuyin" v-on:click.native="showBuyinForm" />
                <game-button text="Cash Out" icon="signout" v-show="canCashout" v-on:click.native="showCashoutForm" />
            </block>
            <block>
                <report-form v-show="reportFormVisible" :is-active="reportFormVisible" />
                <buyin-form v-show="buyinFormVisible" :is-active="buyinFormVisible" />
                <cashout-form v-show="cashoutFormVisible" :is-active="cashoutFormVisible" />
            </block>
            <block v-if="hasPlayers">
                <div class="standings">
                    <player-table :players="sortedPlayers" />
                </div>
            </block>
            <block v-else>
                No one has joined the game yet.
            </block>
        </page-section>

        <page-section v-if="hasPlayers">
            <block>
                <game-chart :players="players" />
            </block>
        </page-section>
    </layout>
</template>

<script>
    import { DataMixin, FormatMixin } from '@/mixins';
    import { mapGetters } from 'vuex';
    import urls from '@/urls';
    import timeFunctions from '@/time-functions';
    import { Layout } from '@/components/Layouts';
    import { BunchNavigation } from '@/components/Navigation';
    import { GameButton, ReportForm, BuyinForm, CashoutForm, PlayerDropdown, PlayerTable, GameChart } from '@/components/CurrentGame';
    import { Block, CustomLink, PageHeading, PageSection, Spinner } from '@/components/Common';
    import ValueList from '@/components/Common/ValueList/ValueList.vue';
    import ValueListKey from '@/components/Common/ValueList/ValueListKey.vue';
    import ValueListValue from '@/components/Common/ValueList/ValueListValue.vue';
    import { BUNCH, CASHGAME, PLAYER } from '@/store-names';

    export default {
        components: {
            Layout,
            BunchNavigation,
            GameButton,
            ReportForm,
            BuyinForm,
            CashoutForm,
            PlayerDropdown,
            PlayerTable,
            GameChart,
            Block,
            PageHeading,
            PageSection,
            Spinner,
            CustomLink,
            ValueList,
            ValueListKey,
            ValueListValue
        },
        mixins: [
            DataMixin,
            FormatMixin
        ],
        props: {
            apiHost: {
                type: String
            }
        },
        watch: {
            '$route'(to, from) {
                this.init();
            }
        },
        mounted: function () {
            this.init();
        },
        computed: {
            ...mapGetters(BUNCH, {
                slug: 'slug',
                isManager: 'isManager'
            }),
            ...mapGetters(CASHGAME, [
                'id',
                'playerId',
                'locationId',
                'locationName',
                'reportFormVisible',
                'buyinFormVisible',
                'cashoutFormVisible',
                'players',
                'hasPlayers',
                'startTime',
                'updatedTime',
                'sortedPlayers',
                'isInGame',
                'canCashout',
                'canEndGame',
                'canReport',
                'canBuyin',
                'isRunning'
            ]),
            ...mapGetters(PLAYER, {
                bunchPlayers: 'players'
            }),
            title() {
                return `Cashgame ${this.formattedDate}`;
            },
            formattedDate() {
                return this.startTime.format('MMM D YYYY');
            },
            formattedStartTime() {
                return this.startTime.format('HH:mm');
            },
            formattedEndTime() {
                return this.updatedTime.format('HH:mm');
            },
            formattedDuration() {
                return this.formatTime(this.durationMinutes);
            },
            durationMinutes() {
                return timeFunctions.diffInMinutes(this.startTime, this.updatedTime);
            },
            showStartTime() {
                return this.hasPlayers;
            },
            showEndTime() {
                return this.isEnded;
            },
            showDuration() {
                return this.isEnded;
            },
            isEnded() {
                return this.hasPlayers && !this.isRunning;
            },
            areButtonsVisible() {
                return this.isRunning && !this.isAnyFormVisible;
            },
            isAnyFormVisible() {
                return this.isRunning && this.reportFormVisible || this.buyinFormVisible || this.cashoutFormVisible;
            },
            isPlayerSelectionVisible() {
                return this.isRunning && this.isManager;
            },
            locationUrl() {
                return urls.location.details(this.locationId);
            },
            canEdit() {
                return this.isManager;
            },
            editUrl() {
                return urls.cashgame.edit(this.id);
            },
            ready() {
                return this.bunchReady && this.cashgameReady;
            }
        },
        methods: {
            showReportForm: function () {
                this.$store.dispatch('cashgame/showReportForm');
            },
            showBuyinForm: function () {
                this.$store.dispatch('cashgame/showBuyinForm');
            },
            showCashoutForm: function () {
                this.$store.dispatch('cashgame/showCashoutForm');
            },
            redirect: function () {
                this.$router.push(urls.cashgame.index(this.slug));
            },
            init: function () {
                this.loadUser();
                this.loadBunch();
                this.loadPlayers();
                this.loadCashgame();
            }
        }
    };
</script>

<style>
</style>

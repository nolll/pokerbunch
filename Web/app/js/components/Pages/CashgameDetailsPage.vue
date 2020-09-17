<template>
    <layout :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <page-section>
            <block>
                <page-heading :text="title" />
            </block>
            <block class="button-list" v-if="areButtonsVisible">
                <game-button text="Report" icon="reorder" v-show="$_canReport" v-on:click.native="$_showReportForm" />
                <game-button text="Buy In" icon="money" v-show="$_canBuyin" v-on:click.native="$_showBuyinForm" />
                <game-button text="Cash Out" icon="signout" v-show="$_canCashout" v-on:click.native="$_showCashoutForm" />
            </block>
            <block>
                <report-form v-show="$_reportFormVisible" :is-active="$_reportFormVisible" />
                <buyin-form v-show="$_buyinFormVisible" :is-active="$_buyinFormVisible" />
                <cashout-form v-show="$_cashoutFormVisible" :is-active="$_cashoutFormVisible" />
            </block>
            <block v-if="$_hasPlayers">
                <div class="standings">
                    <player-table/>
                </div>
            </block>
            <block v-else>
                No one has joined the game yet.
            </block>
            <template slot="aside2">
                <block>
                    <value-list>
                        <value-list-key v-if="showStartTime">Start Time</value-list-key>
                        <value-list-value v-if="showStartTime">{{formattedStartTime}}</value-list-value>
                        <value-list-key v-if="showEndTime">End Time</value-list-key>
                        <value-list-value v-if="showEndTime">{{formattedEndTime}}</value-list-value>
                        <value-list-key v-if="showDuration">Duration</value-list-key>
                        <value-list-value v-if="showDuration">{{formattedDuration}}</value-list-value>
                        <value-list-key>Location</value-list-key>
                        <value-list-value><custom-link :url="locationUrl">{{$_locationName}}</custom-link></value-list-value>
                        <value-list-key v-if="isPlayerSelectionVisible">Player</value-list-key>
                        <value-list-value v-if="isPlayerSelectionVisible"><player-dropdown /></value-list-value>
                    </value-list>
                </block>
                <block v-if="canEdit">
                    <custom-link :url="editUrl" cssClasses="button button--action">Edit Cashgame</custom-link>
                </block>
            </template>
        </page-section>

        <page-section v-if="$_hasPlayers">
            <block>
                <game-chart :players="$_cashgamePlayers" />
            </block>
        </page-section>
    </layout>
</template>

<script>
    import { BunchMixin, CashgameMixin, PlayerMixin, FormatMixin, UserMixin} from '@/mixins';
    import urls from '@/urls';
    import timeFunctions from '@/time-functions';
    import { Layout } from '@/components/Layouts';
    import { BunchNavigation } from '@/components/Navigation';
    import { GameButton, ReportForm, BuyinForm, CashoutForm, PlayerDropdown, PlayerTable, GameChart } from '@/components/CurrentGame';
    import { Block, CustomLink, PageHeading, PageSection, Spinner } from '@/components/Common';
    import ValueList from '@/components/Common/ValueList/ValueList.vue';
    import ValueListKey from '@/components/Common/ValueList/ValueListKey.vue';
    import ValueListValue from '@/components/Common/ValueList/ValueListValue.vue';

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
            BunchMixin,
            CashgameMixin,
            PlayerMixin,
            FormatMixin,
            UserMixin
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
            title() {
                return `Cashgame ${this.formattedDate}`;
            },
            formattedDate() {
                return this.$_startTime.format('MMM D YYYY');
            },
            formattedStartTime() {
                return this.$_startTime.format('HH:mm');
            },
            formattedEndTime() {
                return this.$_updatedTime.format('HH:mm');
            },
            formattedDuration() {
                return this.$_formatTime(this.durationMinutes);
            },
            durationMinutes() {
                return timeFunctions.diffInMinutes(this.$_startTime, this.$_updatedTime);
            },
            showStartTime() {
                return this.$_hasPlayers;
            },
            showEndTime() {
                return this.isEnded;
            },
            showDuration() {
                return this.isEnded;
            },
            isEnded() {
                return this.$_hasPlayers && !this.$_isRunning;
            },
            areButtonsVisible() {
                return this.$_isRunning && !this.isAnyFormVisible;
            },
            isAnyFormVisible() {
                return this.$_isRunning && this.$_reportFormVisible || this.$_buyinFormVisible || this.$_cashoutFormVisible;
            },
            isPlayerSelectionVisible() {
                return this.$_isRunning && this.$_isManager;
            },
            locationUrl() {
                return urls.location.details(this.$_locationId);
            },
            canEdit() {
                return this.$_isManager;
            },
            editUrl() {
                return urls.cashgame.edit(this.$_cashgameId);
            },
            ready() {
                return this.$_bunchReady && this.$_cashgameReady;
            }
        },
        methods: {
            redirect: function () {
                this.$router.push(urls.cashgame.index(this.$_slug));
            },
            init: function () {
                this.$_requireUser();
                this.$_loadBunch();
                this.$_loadPlayers();
                this.$_loadCashgame();
            }
        }
    };
</script>

<style>
</style>

<template>
    <Layout :ready="ready">
        <template slot="top-nav">
            <BunchNavigation />
        </template>

        <PageSection>
            <Block>
                <PageHeading :text="title" />
            </Block>
            <Block class="button-list" v-if="areButtonsVisible">
                <GameButton text="Report" icon="reorder" v-show="$_canReport" v-on:click.native="$_showReportForm" />
                <GameButton text="Buy In" icon="money" v-show="$_canBuyin" v-on:click.native="$_showBuyinForm" />
                <GameButton text="Cash Out" icon="signout" v-show="$_canCashout" v-on:click.native="$_showCashoutForm" />
            </Block>
            <Block>
                <ReportForm v-show="$_reportFormVisible" :is-active="$_reportFormVisible" />
                <BuyinForm v-show="$_buyinFormVisible" :is-active="$_buyinFormVisible" />
                <CashoutForm v-show="$_cashoutFormVisible" :is-active="$_cashoutFormVisible" />
            </Block>
            <Block v-if="$_hasPlayers">
                <div class="standings">
                    <PlayerTable/>
                </div>
            </Block>
            <Block v-else>
                No one has joined the game yet.
            </Block>
            <template slot="aside2">
                <Block>
                    <ValueList>
                        <ValueListKey v-if="showStartTime">Start Time</ValueListKey>
                        <ValueListValue v-if="showStartTime">{{formattedStartTime}}</ValueListValue>
                        <ValueListKey v-if="showEndTime">End Time</ValueListKey>
                        <ValueListValue v-if="showEndTime">{{formattedEndTime}}</ValueListValue>
                        <ValueListKey v-if="showDuration">Duration</ValueListKey>
                        <ValueListValue v-if="showDuration">{{formattedDuration}}</ValueListValue>
                        <ValueListKey>Location</ValueListKey>
                        <ValueListValue><CustomLink :url="locationUrl">{{$_locationName}}</CustomLink></ValueListValue>
                        <ValueListKey v-if="isPlayerSelectionVisible">Player</ValueListKey>
                        <ValueListValue v-if="isPlayerSelectionVisible"><PlayerDropdown /></ValueListValue>
                    </ValueList>
                </Block>
                <Block v-if="canEdit">
                    <CustomLink :url="editUrl" cssClasses="button button--action">Edit Cashgame</CustomLink>
                </Block>
            </template>
        </PageSection>

        <PageSection v-if="$_hasPlayers">
            <Block>
                <GameChart :players="$_cashgamePlayers" />
            </Block>
        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Prop, Mixins, Watch } from 'vue-property-decorator';
    import { BunchMixin, CashgameMixin, PlayerMixin, FormatMixin, UserMixin } from '@/mixins';
    import urls from '@/urls';
    import timeFunctions from '@/time-functions';
    import Layout from '@/components/Layouts/Layout.vue';
    import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
    import GameButton from '@/components/CurrentGame/GameButton.vue';
    import ReportForm from '@/components/CurrentGame/ReportForm.vue';
    import BuyinForm from '@/components/CurrentGame/BuyinForm.vue';
    import CashoutForm from '@/components/CurrentGame/CashoutForm.vue';
    import PlayerDropdown from '@/components/CurrentGame/PlayerDropdown.vue';
    import PlayerTable from '@/components/CurrentGame/PlayerTable.vue';
    import GameChart from '@/components/CurrentGame/GameChart.vue';
    import Block from '@/components/Common/Block.vue';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import ValueList from '@/components/Common/ValueList/ValueList.vue';
    import ValueListKey from '@/components/Common/ValueList/ValueListKey.vue';
    import ValueListValue from '@/components/Common/ValueList/ValueListValue.vue';
    import format from '@/format';

    @Component({
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
            CustomLink,
            ValueList,
            ValueListKey,
            ValueListValue
        }
    })
    export default class CashgameDetailsPage extends Mixins(
        BunchMixin,
        CashgameMixin,
        PlayerMixin,
        FormatMixin,
        UserMixin
    ) {
        @Prop() readonly apiHost!: string;

        get title() {
            return `Cashgame ${this.formattedDate}`;
        }

        get formattedDate() {
            return format.monthDayYear(this.$_startTime);
        }

        get formattedStartTime() {
            return format.hourMinute(this.$_startTime);
        }

        get formattedEndTime() {
            return format.hourMinute(this.$_updatedTime);
        }

        get formattedDuration() {
            return this.$_formatDuration(this.durationMinutes);
        }

        get durationMinutes() {
            return timeFunctions.diffInMinutes(this.$_startTime, this.$_updatedTime);
        }

        get showStartTime() {
            return this.$_hasPlayers;
        }

        get showEndTime() {
            return this.isEnded;
        }

        get showDuration() {
            return this.isEnded;
        }

        get isEnded() {
            return this.$_hasPlayers && !this.$_isRunning;
        }

        get areButtonsVisible() {
            return this.$_isRunning && !this.isAnyFormVisible;
        }

        get isAnyFormVisible() {
            return this.$_isRunning && this.$_reportFormVisible || this.$_buyinFormVisible || this.$_cashoutFormVisible;
        }

        get isPlayerSelectionVisible() {
            return this.$_isRunning && this.$_isManager;
        }

        get locationUrl() {
            return urls.location.details(this.$_slug, this.$_locationId);
        }

        get canEdit() {
            return this.$_isManager;
        }

        get editUrl() {
            return urls.cashgame.edit(this.$_cashgameId);
        }

        get ready() {
            return this.$_bunchReady && this.$_cashgameReady;
        }

        mounted() {
            this.init();
        }

        redirect() {
            this.$router.push(urls.cashgame.index(this.$_slug));
        }

        init() {
            this.$_requireUser();
            this.$_loadBunch();
            this.$_loadPlayers();
            this.$_loadCashgame();
        }

        @Watch('$route')
        routeChanged() {
            this.init();
        }
    }
</script>

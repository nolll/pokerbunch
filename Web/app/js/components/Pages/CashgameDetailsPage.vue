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
                <GameButton text="Report" icon="reorder" v-show="canReport" @click.native="showReportForm" />
                <GameButton text="Buy In" icon="money" v-show="canBuyin" @click.native="showBuyinForm" />
                <GameButton text="Cash Out" icon="signout" v-show="canCashout" @click.native="showCashoutForm" />
            </Block>
            <Block>
                <ReportForm v-show="reportFormVisible" :defaultBuyin="defaultBuyin" @report="report" @cancel="hideForms" />
                <BuyinForm v-show="buyinFormVisible" :defaultBuyin="defaultBuyin" @buyin="buyin" @cancel="hideForms" :isPlayerInGame="isInGame" />
                <CashoutForm v-show="cashoutFormVisible" :defaultBuyin="defaultBuyin" @cashout="cashout" @cancel="hideForms" />
            </Block>
            <Block v-if="hasPlayers">
                <div class="standings">
                    <PlayerTable :players="cashgame.players" :isCashgameRunning="cashgame.isRunning" @playerSelected="onSelectPlayer" @deleteAction="onDeleteAction" @saveAction="onSaveAction" :canEdit="canEdit" />
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
                        <ValueListValue><CustomLink :url="locationUrl">{{locationName}}</CustomLink></ValueListValue>
                        <ValueListKey v-if="isPartOfEvent">Event</ValueListKey>
                        <ValueListValue v-if="isPartOfEvent"><CustomLink :url="eventUrl">{{eventName}}</CustomLink></ValueListValue>
                        <ValueListKey v-if="isPlayerSelectionEnabled">Player</ValueListKey>
                        <ValueListValue v-if="isPlayerSelectionEnabled"><PlayerDropdown :players="allPlayers" v-model="selectedPlayerId" /></ValueListValue>
                    </ValueList>
                </Block>
                <Block v-if="canEdit">
                    <CustomButton @click="onEdit" type="action" text="Edit Cashgame" />
                </Block>
            </template>
        </PageSection>

        <PageSection v-if="hasPlayers">
            <Block>
                <GameChart :players="playersInGame" />
            </Block>
        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Prop, Mixins, Watch } from 'vue-property-decorator';
    import { BunchMixin, PlayerMixin, FormatMixin, UserMixin } from '@/mixins';
    import urls from '@/urls';
    import timeFunctions from '@/time-functions';
    import Layout from '@/components/Layouts/Layout.vue';
    import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
    import GameButton from '@/components/CurrentGame/GameButton.vue';
    import ReportForm from '@/components/CurrentGame/ReportForm.vue';
    import BuyinForm from '@/components/CurrentGame/BuyinForm.vue';
    import CashoutForm from '@/components/CurrentGame/CashoutForm.vue';
    import PlayerDropdown from '@/components/PlayerDropdown.vue';
    import PlayerTable from '@/components/CurrentGame/PlayerTable.vue';
    import GameChart from '@/components/CurrentGame/GameChart.vue';
    import Block from '@/components/Common/Block.vue';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import CustomButton from '@/components/Common/CustomButton.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import ValueList from '@/components/Common/ValueList/ValueList.vue';
    import ValueListKey from '@/components/Common/ValueList/ValueListKey.vue';
    import ValueListValue from '@/components/Common/ValueList/ValueListValue.vue';
    import format from '@/format';
    import dayjs from 'dayjs';
    import { DetailedCashgame } from '@/models/DetailedCashgame';
    import api from '@/api';

    const longRefresh = 30000;

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
            CustomButton,
            ValueList,
            ValueListKey,
            ValueListValue
        }
    })
    export default class CashgameDetailsPage extends Mixins(
        BunchMixin,
        PlayerMixin,
        FormatMixin,
        UserMixin
    ) {
        @Prop() readonly apiHost!: string;

        cashgame: DetailedCashgame | null = null;
        reportFormVisible = false;
        buyinFormVisible = false;
        cashoutFormVisible = false;
        selectedPlayerId: string = '';
        refreshHandle = 0;
        isEditing = false;

        get title() {
            return `Cashgame ${this.formattedDate}`;
        }

        get formattedDate() {
            return format.monthDayYear(this.startTime);
        }

        get formattedStartTime() {
            return format.hourMinute(this.startTime);
        }

        get formattedEndTime() {
            if(!this.cashgame)
                return '';
            return format.hourMinute(this.cashgame.updatedTime);
        }

        get formattedDuration() {
            return this.$_formatDuration(this.durationMinutes);
        }

        get durationMinutes() {
            if(!this.cashgame)
                return 0;
            return timeFunctions.diffInMinutes(this.startTime, this.cashgame.updatedTime);
        }

        get showStartTime() {
            return this.hasPlayers;
        }

        get showEndTime() {
            return this.isEnded;
        }

        get showDuration() {
            return this.isEnded;
        }

        get isRunning() {
            return !!this.cashgame?.isRunning;
        }

        get isInGame(){
            return !!this.playerInGame;
        }

        get canReport(){
            return this.isInGame && !this.hasCachedOut;
        }

        get canBuyin(){
            return !this.hasCachedOut;
        }

        get canCashout(){
            return this.isInGame;
        }

        get hasCachedOut(){
            if (!this.playerInGame)
                return false;
            return this.playerInGame.hasCashedOut();
        }

        get isEnded() { 
            return this.hasPlayers && !this.isRunning;
        }

        get areButtonsVisible() {
            return this.isRunning && !this.isAnyFormVisible;
        }

        get isAnyFormVisible() {
            return this.isRunning && this.reportFormVisible || this.buyinFormVisible || this.cashoutFormVisible;
        }

        get isPlayerSelectionEnabled() {
            return this.isRunning && this.$_isManager;
        }

        get locationName(){
            return this.cashgame?.location.name || '';
        }

        get locationUrl() {
            if(!this.cashgame)
                return '';
            return urls.location.details(this.$_slug, this.cashgame.location.id);
        }

        get isPartOfEvent(){
            return !!this.cashgame?.event;
        }

        get eventName(){
            return this.cashgame?.event?.name || '';
        }

        get eventUrl() {
            if(!this.cashgame)
                return '';

            if(!this.cashgame.event)
                return '';
            
            return urls.event.details(this.$_slug, this.cashgame.event.id);
        }

        get canEdit() {
            return this.$_isManager;
        }

        get playersInGame(){
            if(!this.cashgame)
                return [];
            const sortedPlayers = this.cashgame.players.slice().sort((left, right) => right.getWinnings() - left.getWinnings());
            return this.cashgame.players;
        }

        get allPlayers(){
            return this.$_players;
        }

        get hasPlayers(){
            return !!this.playersInGame.length;
        }

        get userPlayer() {
            return this.$_getPlayer(this.$_playerId);
        }

        get playerName() {
            return this.player?.name || '';
        }

        get playerColor() {
            return this.player?.color || '#9e9e9e';
        }

        get defaultBuyin(){
            return this.$_defaultBuyin;
        }

        get player(){
            return this.$_getPlayer(this.selectedPlayerId);
        }

        get playerInGame(){
            return this.getPlayerInGame(this.selectedPlayerId);
        }

        get startTime(){
            let first;
            let t = dayjs().utc();
            const p = this.playersInGame;

            if (p.length === 0)
                return t.toDate();
            for (let i = 0; i < p.length; i++) {
                first = p[i].actions[0];
                if (first) {
                    const firstTime = dayjs(first.time);
                    if (firstTime.isBefore(t)) {
                        t = firstTime;
                    }
                }
            }
            return t.toDate();
        }

        get updatedTime(){
            return this.cashgame?.updatedTime || null;
        }

        get ready() {
            return this.$_bunchReady && this.cashgameReady && this.$_playersReady;
        }

        setupRefresh(refreshTimeout: number) {
            if (this.isRunning) {
                window.setInterval(() => {
                    this.refresh();
                }, refreshTimeout);
            }
        }

        async report(stack: number){
            if(!this.cashgame)
                return;

            this.cashgame.report(this.selectedPlayerId, stack);
            const reportData = { type: 'report', playerId: this.selectedPlayerId, stack: stack };
            this.resetSelectedPlayerId();
            this.hideForms();
            await api.report(this.cashgame.id, reportData);
        }

        async buyin(amount: number, stack: number){
            if (!this.cashgame)
                return;
                
            if (!this.isInGame) {
                const player = this.cashgame.addPlayer(this.selectedPlayerId, this.playerName, this.playerColor);
            }

            this.cashgame.buyin(this.selectedPlayerId, amount, stack);
            const buyinData = { type: 'buyin', playerId: this.selectedPlayerId, stack: stack, added: amount };
            this.resetSelectedPlayerId();
            this.hideForms();
            await api.buyin(this.cashgame.id, buyinData);
        }

        async cashout(stack: number){
            if(!this.cashgame)
                return;
            
            this.cashgame.cashout(this.selectedPlayerId, stack);
            const cashoutData = { type: 'cashout', playerId: this.selectedPlayerId, stack: stack };
            this.resetSelectedPlayerId();
            this.hideForms();
            await api.cashout(this.cashgame.id, cashoutData);
        }

        showReportForm(){
            this.reportFormVisible = true;
        }

        showBuyinForm(){
            this.buyinFormVisible = true;
        }

        showCashoutForm(){
            this.cashoutFormVisible = true;
        }

        hideForms(){
            this.reportFormVisible = false;
            this.buyinFormVisible = false;
            this.cashoutFormVisible = false;
        }

        resetSelectedPlayerId(){
            this.selectedPlayerId = this.$_playerId;
        }

        getPlayerInGame(id: string){
            if (!id)
                return null;
            return this.playersInGame.find(p => p.id.toString() === id.toString()) || null;
        }

        onSelectPlayer(id: string){
            if(this.isPlayerSelectionEnabled)
                this.selectedPlayerId = id;
        }

        onEdit(){
            this.isEditing = true;
        }

        async onDeleteAction(id: string){
            if(!this.cashgame)
                return;
            
            this.cashgame.deleteAction(id);
            await api.deleteAction(this.cashgame.id, id);
        }

        async onSaveAction(data: any){
            if(!this.cashgame)
                return;

            this.cashgame.updateAction(data.id, data);
            const updateData = {
                added: data.added,
                stack: data.stack,
                timestamp: data.time
            };
            await api.updateAction(this.cashgame.id, data.id, updateData);
        }

        mounted() {
            this.init();
        }

        beforeDestroy(){
            if(this.refreshHandle)
                window.clearInterval(this.refreshHandle);
        }

        redirect() {
            this.$router.push(urls.cashgame.index(this.$_slug));
        }

        async loadCashgame(){
            const response = await api.getCashgame(this.$route.params.id);
            this.cashgame = response.status === 200
                ? new DetailedCashgame(response.data)
                : null;
        }

        async refresh(){
            await this.loadCashgame();
        }

        get cashgameReady(){
            return !!this.cashgame;
        }

        async init() {
            this.selectedPlayerId = this.$_playerId;
            this.$_requireUser();
            this.$_loadBunch();
            this.$_loadPlayers();
            await this.loadCashgame();
            this.setupRefresh(longRefresh);
        }

        @Watch('$_playerId')
        playerIdChanged() {
            this.selectedPlayerId = this.$_playerId;
        }

        @Watch('$route')
        routeChanged() {
            this.init();
        }
    }
</script>

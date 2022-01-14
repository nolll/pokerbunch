<template>
    <div class="player-row">
        <div class="row-wrapper">
            <div class="name-and-time">
                <div>
                    <div class="player-color-box" :style="{backgroundColor: player.color}" @click="onSelected"></div>
                    <a href="#" @click="toggle">{{player.name}}</a>
                    <i title="Cashed out" class="icon-ok-sign" v-if="showCheckmark"></i>
                </div>
                <div class="time" v-if="isReportTimeEnabled"><i title="Last report" class="icon-time"></i> <span>{{lastReportTime}}</span></div>
            </div>
            <div class="amounts">
                <div><i title="Buy in" class="icon-signin"></i> <CurrencyText :value="calculatedBuyin" /></div>
                <div><i title="Stack" class="icon-reorder"></i> <CurrencyText :value="stack" /></div>
                <div><WinningsText :value="winnings" /></div>
            </div>
        </div>
        <div v-if="showDetails">
            <div class="chart">
                <CashgameActionChart :player="player" />
            </div>
            <div class="link">
                <CustomLink :url="url">View player</CustomLink>
            </div>
            <div class="actions">
                <PlayerAction v-for="action in player.actions" :action="action" :key="action.id" @deleteAction="onDeleteAction" @saveAction="onSaveAction" :canEdit="canEdit" />
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import { FormatMixin } from '@/mixins';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import PlayerAction from './PlayerAction.vue';
    import urls from '@/urls';
    import CashgameActionChart from '@/components/CashgameActionChart.vue';
    import CurrencyText from '@/components/Common/CurrencyText.vue';
    import WinningsText from '@/components/Common/WinningsText.vue';
    import { CssClasses } from '@/models/CssClasses';
    import { DetailedCashgamePlayer } from '@/models/DetailedCashgamePlayer';

    @Component({
        components: {
            CashgameActionChart,
            CurrencyText,
            CustomLink,
            PlayerAction,
            WinningsText
        }
    })
    export default class PlayerRow extends Mixins(
        FormatMixin
    ) {
        @Prop() readonly bunchId!: string;
        @Prop() readonly player!: DetailedCashgamePlayer;
        @Prop() readonly isCashgameRunning!: boolean;
        @Prop({default: false}) readonly canEdit!: boolean;

        isExpanded = false;

        get hasCashedOut() {
            return this.player.hasCashedOut();
        }

        get showCheckmark() {
            return this.isCashgameRunning && this.hasCashedOut;
        }

        get isReportTimeEnabled(){
            return this.isCashgameRunning;
        }

        get lastReportTime() {
            return this.player.getLastReportTime();
        }

        get calculatedBuyin() {
            return this.player.getBuyin();
        }

        get stack() {
            return this.player.getStack();
        }

        get winnings() {
            return this.player.getWinnings();
        }

        get url() {
            return urls.player.details(this.bunchId, this.player.id);
        }

        get showDetails() {
            return this.isExpanded;
        }

        expand() {
            this.isExpanded = true;
        }

        collapse() {
            this.isExpanded = false;
        }

        toggle() {
            this.isExpanded = !this.isExpanded;
        }

        onSelected(){
            this.$emit('selected', this.player.id);
        }

        onDeleteAction(id: string){
            this.$emit('deleteAction', id);
        }

        onSaveAction(data: any){
            this.$emit('saveAction', data);
        }

        click() {
            this.toggle();
        }
    }
</script>

<style lang="scss" scoped>
    .player-row {
        padding: 5px 0;
        border-bottom: 1px solid #eee;
    }

    .row-wrapper{
        display: flex;
    }

    .link{
        padding: 8px;
    }

    .name-and-time {
        flex: 9;
    }

    .amounts {
        flex: 11;
    }

    .chart,
    .actions{
        padding: 8px;
    }
</style>

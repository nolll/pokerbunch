<template>
    <div class="player-row">
        <div class="row-wrapper">
            <div class="name-and-time">
                <div>
                    <div class="player-color-box" :style="{backgroundColor: player.color}"></div>
                    <a href="#" @click="toggle">{{player.name}}</a>
                    <i title="Cashed out" class="icon-ok-sign" v-if="showCheckmark"></i>
                </div>
                <div class="time" v-if="isReportTimeEnabled"><i title="Last report" class="icon-time"></i> <span>{{lastReportTime}}</span></div>
            </div>
            <div class="amounts">
                <div><i title="Buy in" class="icon-signin"></i> <span>{{formattedBuyin}}</span></div>
                <div><i title="Stack" class="icon-reorder"></i> <span>{{formattedStack}}</span></div>
                <div :class="winningsCssClasses">{{formattedWinnings}}</div>
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
                <PlayerAction v-for="action in player.actions" :action="action" :key="action.id" />
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import { CashgameMixin, FormatMixin } from '@/mixins';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import PlayerAction from './PlayerAction.vue';
    import playerCalculator from '@/PlayerCalculator';
    import urls from '@/urls';
    import CashgameActionChart from '@/components/CashgameActionChart.vue';
    import { DetailedCashgameResponsePlayer } from '@/response/DetailedCashgameResponsePlayer';
    import { CssClasses } from '@/models/CssClasses';

    @Component({
        components: {
            CustomLink,
            PlayerAction,
            CashgameActionChart
        }
    })
    export default class PlayerRow extends Mixins(
        CashgameMixin,
        FormatMixin
    ) {
        @Prop() readonly player!: DetailedCashgameResponsePlayer;
        @Prop() readonly isReportTimeEnabled!: boolean;
        @Prop() readonly isCheckmarkEnabled!: boolean;

        isExpanded = false;

        get hasCashedOut() {
            return playerCalculator.hasCashedOut(this.player);
        }

        get showCheckmark() {
            return this.isCheckmarkEnabled && this.hasCashedOut;
        }

        get lastReportTime() {
            return playerCalculator.getLastReportTime(this.player);
        }

        get calculatedBuyin() {
            return playerCalculator.getBuyin(this.player);
        }

        get stack() {
            return playerCalculator.getStack(this.player);
        }

        get winnings() {
            return playerCalculator.getWinnings(this.player);
        }

        get winningsCssClasses(): CssClasses {
            return {
                'pos-result': this.winnings > 0,
                'neg-result': this.winnings < 0
            };
        }

        get formattedBuyin() {
            return this.$_formatCurrency(this.calculatedBuyin);
        }

        get formattedStack() {
            return this.$_formatCurrency(this.stack);
        }

        get formattedWinnings() {
            return this.$_formatResult(this.winnings);
        }

        get url() {
            return urls.player.details(this.player.id);
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

        click() {
            this.toggle();
        }
    }
</script>

<style lang="less" scoped>
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

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

<script>
    import { CashgameMixin, FormatMixin } from '@/mixins';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import { PlayerAction } from '.';
    import playerCalculator from '@/player-calculator';
    import urls from '@/urls';
    import CashgameActionChart from '@/components/CashgameActionChart.vue';

    export default {
        mixins: [
            CashgameMixin,
            FormatMixin
        ],
        components: {
            CustomLink,
            PlayerAction,
            CashgameActionChart
        },
        props: {
            player: {
                type: Object
            },
            isReportTimeEnabled: {
                type: Boolean
            },
            isCheckmarkEnabled: {
                type: Boolean
            }
        },
        data: function () {
            return {
                isExpanded: false
            }
        },
        computed: {
            hasCashedOut() {
                return playerCalculator.hasCashedOut(this.player);
            },
            showCheckmark() {
                return this.isCheckmarkEnabled && this.hasCashedOut;
            },
            lastReportTime() {
                return playerCalculator.getLastReportTime(this.player);
            },
            calculatedBuyin() {
                return playerCalculator.getBuyin(this.player);
            },
            stack() {
                return playerCalculator.getStack(this.player);
            },
            winnings() {
                return playerCalculator.getWinnings(this.player);
            },
            winningsCssClasses() {
                return {
                    'pos-result': this.winnings > 0,
                    'neg-result': this.winnings < 0
                };
            },
            formattedBuyin() {
                return this.$_formatCurrency(this.calculatedBuyin);
            },
            formattedStack() {
                return this.$_formatCurrency(this.stack);
            },
            formattedWinnings() {
                return this.$_formatResult(this.winnings);
            },
            url() {
                return urls.player.details(this.player.id);
            },
            showDetails() {
                return this.isExpanded;
            }
        },
        methods: {
            expand() {
                this.isExpanded = true;
            },
            collapse() {
                this.isExpanded = false;
            },
            toggle() {
                this.isExpanded = !this.isExpanded;
            },
            click() {
                this.toggle();
            }
        }
    };
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

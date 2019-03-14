<template>
    <div class="standings-item">
        <div class="name">
            <div class="player-color-box" :style="{backgroundColor: player.color}"></div>
            <custom-link :url="player.url">{{player.name}}</custom-link>
            <i title="Cashed out" class="icon-ok-sign" v-if="showCheckmark"></i>
        </div>
        <div class="amounts">
            <div><i title="Buy in" class="icon-signin"></i> <span>{{formattedBuyin}}</span></div>
            <div><i title="Stack" class="icon-reorder"></i> <span>{{formattedStack}}</span></div>
            <div :class="winningsCssClasses">{{formattedWinnings}}</div>
        </div>
        <div class="time" v-if="isReportTimeEnabled"><i title="Last report" class="icon-time"></i> <span>{{lastReportTime}}</span></div>
    </div>
</template>

<script>
    import { FormatMixin } from '@/mixins';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import playerCalculator from '@/player-calculator';

    export default {
        mixins: [
            FormatMixin
        ],
        components: {
            CustomLink
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
            buyin() {
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
                return this.formatCurrency(this.buyin);
            },
            formattedStack() {
                return this.formatCurrency(this.stack);
            },
            formattedWinnings() {
                return this.formatResult(this.winnings);
            }
        }
    };
</script>

<style>

</style>

<template>
    <div class="standings-item">
        <div class="name">
            <div class="player-color-box" :style="{backgroundColor: player.color}"></div>
            <a :href="player.url">{{player.name}}</a>
            <i title="Cashed out" class="icon-ok-sign" v-if="hasCashedOut"></i>
        </div>
        <div class="amounts">
            <div><i title="Buy in" class="icon-signin"></i> <span>{{formattedBuyin}}</span></div>
            <div><i title="Stack" class="icon-reorder"></i> <span>{{formattedStack}}</span></div>
            <div :class="winningsCssClasses">{{formattedWinnings}}</div>
        </div>
        <div class="time"><i title="Last report" class="icon-time"></i> <span>{{lastReportTime}}</span></div>
    </div>
</template>

<script>
    import { mapGetters } from 'vuex';
    import { FormatMixin } from '@/mixins';
    import { CURRENT_GAME } from '@/store-names';

    export default {
        mixins: [
            FormatMixin
        ],
        props: {
            player: {
                type: Object
            }
        },
        computed: {
            ...mapGetters(CURRENT_GAME, [
                'getLastReportTime',
                'getBuyin',
                'getStack',
                'getWinnings'
            ]),
            hasCashedOut() {
                return this.player.hasCashedOut;
            },
            lastReportTime() {
                return this.getLastReportTime(this.player);
            },
            buyin() {
                return this.getBuyin(this.player);
            },
            stack() {
                return this.getStack(this.player);
            },
            winnings() {
                return this.getWinnings(this.player);
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

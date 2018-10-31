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
            <div :class="winningsCssClass">{{formattedWinnings}}</div>
        </div>
        <div class="time"><i title="Last report" class="icon-time"></i> <span>{{lastReportTime}}</span></div>
    </div>
</template>

<script>
    import { mapState, mapGetters } from 'vuex';
    import { FormatMixin } from '../../mixins';

    export default {
        mixins: [
            FormatMixin
        ],
        props: ['player'],
        computed: {
            ...mapGetters('currentGame', [
                'getLastReportTime',
                'getBuyin',
                'getStack',
                'getWinnings']),
            hasCashedOut: function () {
                return this.player.hasCashedOut;
            },
            lastReportTime: function () {
                return this.getLastReportTime(this.player);
            },
            buyin: function () {
                return this.getBuyin(this.player);
            },
            stack: function () {
                return this.getStack(this.player);
            },
            winnings: function () {
                return this.getWinnings(this.player);
            },
            winningsCssClass: function () {
                var winnings = this.winnings;
                if (winnings === 0)
                    return '';
                return winnings > 0 ? 'pos-result' : 'neg-result';
            },
            formattedBuyin: function () {
                return this.formatCurrency(this.buyin);
            },
            formattedStack: function () {
                return this.formatCurrency(this.stack);
            },
            formattedWinnings: function () {
                return this.formatResult(this.winnings);
            }
        }
    };
</script>

<style>

</style>

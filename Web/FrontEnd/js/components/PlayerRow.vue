<template>
    <div class="standings-item">
        <div class="name">
            <div class="player-color-box" v-bind:style="{backgroundColor: player.color}"></div>
            <a v-bind:href="player.url">{{player.name}}</a>
            <i title="Cashed out" class="icon-ok-sign" v-if="hasCashedOut"></i>
        </div>
        <div class="amounts">
            <div><i title="Buy in" class="icon-signin"></i> <span v-text="formattedBuyin"></span></div>
            <div><i title="Stack" class="icon-reorder"></i> <span v-text="formattedStack"></span></div>
            <div v-bind:class="winningsCssClass" v-text="formattedWinnings"></div>
        </div>
        <div class="time"><i title="Last report" class="icon-time"></i> <span>{{lastReportTime}}</span></div>
    </div>
</template>

<script>
    import gameService from '../game-service';

    export default {
        props: ['player', 'currencyFormat'],
        computed: {
            hasCashedOut: function () {
                return this.player.hasCashedOut;
            },
            lastReportTime: function () {
                return gameService.getLastReportTime(this.player);
            },
            buyin: function () {
                return gameService.getBuyin(this.player);
            },
            stack: function () {
                return gameService.getStack(this.player);
            },
            winnings: function () {
                return gameService.getWinnings(this.player);
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
        },
        methods: {
            formatCurrency: function (amount) {
                return this.$options.filters.customCurrency(amount, this.currencyFormat);
            },
            formatResult: function (result) {
                return this.$options.filters.result(result, this.currencyFormat);
            }
        }
    };
</script>

<style scoped>

</style>

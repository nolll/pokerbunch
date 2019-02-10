<template>
    <div>
        <line-chart :chart-data="chartData" :chart-options="chartOptions"></line-chart>
    </div>
</template>

<script>
    import moment from 'moment';
    import { mapGetters } from 'vuex'
    import { LineChart } from '@/components';
    import { CURRENT_GAME } from '@/store-names';
    import playerCalculator from '@/player-calculator';

    export default {
        components: {
            LineChart
        },
        watch: {
            'players': function () {
                this.drawChart();
            },
            'currentGameReady': function (isReady) {
                if (isReady) {
                    this.drawChart();
                }
            }
        },
        mounted: function () {
            var self = this;
            self.$nextTick(function () {
                if (this.currentGameReady) {
                    self.drawChart();
                }
            });
        },
        computed: {
            ...mapGetters(CURRENT_GAME, [
                'players',
                'currentGameReady'
            ])
        },
        methods: {
            drawChart() {
                this.chartData = this.getGameChartData();
                this.chartOptions = { colors: this.getColors() };
            },
            getGameChartData() {
                return {
                    cols: this.getColumns(),
                    rows: this.getRows(),
                    p: null
                };
            },
            getColors() {
                var i,
                    p,
                    colors = [];
                for (i = 0; i < this.players.length; i++) {
                    p = this.players[i];
                    colors.push(p.color);
                }
                return colors;
            },
            getColumns() {
                var i,
                    p,
                    cols = [{ type: 'datetime', label: 'Time', pattern: 'HH:mm' }];
                for (i = 0; i < this.players.length; i++) {
                    p = this.players[i];
                    cols.push({ type: 'number', label: p.name, pattern: null });
                }
                return cols;
            },
            getRows() {
                var i, j, p, r;
                var rows = [];
                for (i = 0; i < this.players.length; i++) {
                    p = this.players[i];
                    r = this.getPlayerResults(p);
                    for (j = 0; j < r.length; j++) {
                        rows.push(this.getRow(r[j], p.id));
                    }
                    if (!playerCalculator.hasCashedOut(p)) {
                        var currentResult = this.createPlayerResult(moment().utc(), r[r.length - 1].winnings);
                        rows.push(this.getRow(currentResult, p.id));
                    }
                }
                return rows;
            },
            getPlayerResults(player) {
                var i,
                    c,
                    winnings,
                    addedMoney = 0,
                    results = [],
                    checkpoints = player.checkpoints;
                for (i = 0; i < checkpoints.length; i++) {
                    c = checkpoints[i];
                    addedMoney += c.addedMoney;
                    winnings = c.stack - addedMoney;
                    results.push(this.createPlayerResult(c.time, winnings));
                }
                return results;
            },
            createPlayerResult(time, winnings) {
                return { time: time, winnings: winnings };
            },
            getRow(result, playerId) {
                var values = [{ v: moment(result.time).toDate(), f: null }];
                for (var i = 0; i < this.players.length; i++) {
                    var val = null;
                    if (this.players[i].id === playerId) {
                        val = result.winnings + '';
                    }
                    values.push({ v: val, f: null });
                }
                return { c: values };
            }
        },
        data: function () {
            return {
                chartData: null,
                chartOptions: {
                    pointSize: 0,
                    vAxis: { minValue: 0 },
                    hAxis: { format: 'HH:mm' },
                    legend: {
                        position: 'none'
                    }
                }
            }
        },
    };
</script>

<style>

</style>

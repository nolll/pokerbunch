<template>
    <div>
        <line-chart :chart-data="chartData" :chart-options="chartOptions"></line-chart>
    </div>
</template>

<script>
    import moment from 'moment';
    import { LineChart } from '@/components';
    import playerCalculator from '@/player-calculator';
    import { CashgameMixin } from '@/mixins';

    export default {
        components: {
            LineChart
        },
        mixins: [
            CashgameMixin
        ],
        watch: {
            $_cashgamePlayers: function () {
                this.drawChart();
            },
            $_cashgameReady: function (isReady) {
                if (isReady) {
                    this.drawChart();
                }
            }
        },
        mounted: function () {
            var self = this;
            self.$nextTick(function () {
                if (this.$_cashgameReady) {
                    self.drawChart();
                }
            });
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
                for (i = 0; i < this.$_cashgamePlayers.length; i++) {
                    p = this.$_cashgamePlayers[i];
                    colors.push(p.color);
                }
                return colors;
            },
            getColumns() {
                var i,
                    p,
                    cols = [{ type: 'datetime', label: 'Time', pattern: 'HH:mm' }];
                for (i = 0; i < this.$_cashgamePlayers.length; i++) {
                    p = this.$_cashgamePlayers[i];
                    cols.push({ type: 'number', label: p.name, pattern: null });
                }
                return cols;
            },
            getRows() {
                var i, j, p, r;
                var rows = [];
                for (i = 0; i < this.$_cashgamePlayers.length; i++) {
                    p = this.$_cashgamePlayers[i];
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
                    a,
                    winnings,
                    added = 0,
                    results = [],
                    actions = player.actions;
                for (i = 0; i < actions.length; i++) {
                    a = actions[i];
                    added += a.added || 0;
                    winnings = a.stack - added;
                    results.push(this.createPlayerResult(a.time, winnings));
                }
                return results;
            },
            createPlayerResult(time, winnings) {
                return { time: time, winnings: winnings };
            },
            getRow(result, playerId) {
                var values = [{ v: moment(result.time).toDate(), f: null }];
                for (var i = 0; i < this.$_cashgamePlayers.length; i++) {
                    var val = null;
                    if (this.$_cashgamePlayers[i].id === playerId) {
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

<template>
    <div>
        <LineChart :chart-data="chartData" :chart-options="chartOptions" />
    </div>
</template>

<script lang="ts">
    import { Component, Mixins, Watch } from 'vue-property-decorator';
    import dayjs from 'dayjs';
    import LineChart from '@/components/LineChart.vue';
    import playerCalculator from '@/PlayerCalculator';
    import { CashgameMixin } from '@/mixins';
    import { ChartData } from '@/models/ChartData';
    import { ChartRow } from '@/models/ChartRow';
    import { ChartColumn } from '@/models/ChartColumn';
    import { ChartColumnType } from '@/models/ChartColumnType';
    import { ChartColumnPattern } from '@/models/ChartColumnPattern';
    import { ChartRowData } from '@/models/ChartRowData';
    import { ChartOptions } from '@/models/ChartOptions';
    import { DetailedCashgamePlayer } from '@/models/DetailedCashgamePlayer';

    @Component({
        components: {
            LineChart
        }
    })
    export default class GameChart extends Mixins(
        CashgameMixin
    ) {
        chartData: ChartData | null = null;
        chartOptions: ChartOptions = {
            pointSize: 0,
            vAxis: { minValue: 0 },
            hAxis: { format: 'HH:mm' },
            legend: {
                position: 'none'
            },
            colors: []
        };

        drawChart() {
            this.chartData = this.getGameChartData();
            this.chartOptions = { ...this.chartOptions, colors: this.getColors() };
        }

        getGameChartData(): ChartData {
            return {
                cols: this.getColumns(),
                rows: this.getRows(),
                p: null
            };
        }

        getColors() {
            const colors = [];
            for (let i = 0; i < this.$_cashgamePlayers.length; i++) {
                const p = this.$_cashgamePlayers[i];
                colors.push(p.color);
            }
            return colors;
        }

        getColumns(): ChartColumn[] {
            const cols: ChartColumn[] = [{ type: ChartColumnType.DateTime, label: 'Time', pattern: ChartColumnPattern.HoursAndMinutes }];
            for (let i = 0; i < this.$_cashgamePlayers.length; i++) {
                const p = this.$_cashgamePlayers[i];
                cols.push({ type: ChartColumnType.Number, label: p.name, pattern: null });
            }
            return cols;
        }

        getRows(): ChartRow[] {
            var rows = [];
            for (let i = 0; i < this.$_cashgamePlayers.length; i++) {
                const p = this.$_cashgamePlayers[i];
                const r = this.getPlayerResults(p);
                for (let j = 0; j < r.length; j++) {
                    rows.push(this.getRow(r[j], p.id));
                }
                if (!playerCalculator.hasCashedOut(p)) {
                    var currentResult = this.createPlayerResult(dayjs().utc().toDate(), r[r.length - 1].winnings);
                    rows.push(this.getRow(currentResult, p.id));
                }
            }
            return rows;
        }

        getPlayerResults(player: DetailedCashgamePlayer) {
            let added = 0;
            const results = [];
            const actions = player.actions;
            for (let i = 0; i < actions.length; i++) {
                const a = actions[i];
                added += a.added || 0;
                const winnings = a.stack - added;
                results.push(this.createPlayerResult(a.time, winnings));
            }
            return results;
        }

        createPlayerResult(time: Date, winnings: number): ChartPlayerResult {
            return {
                time: time,
                winnings: winnings
            };
        }

        getRow(result: ChartPlayerResult, playerId: string) {
            const values: ChartRowData[] = [{ v: result.time, f: null }];
            for (var i = 0; i < this.$_cashgamePlayers.length; i++) {
                var val = null;
                if (this.$_cashgamePlayers[i].id === playerId) {
                    val = result.winnings + '';
                }
                values.push({ v: val, f: null });
            }
            return { c: values };
        }

        mounted() {
            const self = this;
            self.$nextTick(function () {
                if (this.$_cashgameReady) {
                    self.drawChart();
                }
            });
        }

        @Watch('$_cashgamePlayers')
        cashgamePlayersChanged() {
            this.drawChart();
        }

        @Watch('$_cashgameReady')
        cashgameReadyChanged(isReady: boolean) {
            if (isReady)
                this.drawChart();
        }
    }

    interface ChartPlayerResult{
        time: Date;
        winnings: number;
    }
</script>

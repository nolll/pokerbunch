<template>
    <div>
        <LineChart :chart-data="chartData" :chart-options="chartOptions" />
    </div>
</template>

<script lang="ts">
    import { Component, Mixins, Prop, Watch } from 'vue-property-decorator';
    import dayjs from 'dayjs';
    import utc from 'dayjs/plugin/utc';
    import LineChart from '@/components/LineChart.vue';
    import { ChartData } from '@/models/ChartData';
    import { ChartRow } from '@/models/ChartRow';
    import { ChartColumn } from '@/models/ChartColumn';
    import { ChartColumnType } from '@/models/ChartColumnType';
    import { ChartColumnPattern } from '@/models/ChartColumnPattern';
    import { ChartRowData } from '@/models/ChartRowData';
    import { ChartOptions } from '@/models/ChartOptions';
    import { DetailedCashgamePlayer } from '@/models/DetailedCashgamePlayer';
    import { DetailedCashgameResponsePlayer } from '@/response/DetailedCashgameResponsePlayer';

    dayjs.extend(utc);

    @Component({
        components: {
            LineChart
        }
    })
    export default class GameChart extends Mixins(
        
    ) {
        @Prop() readonly players!: DetailedCashgamePlayer[];

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
            for (const p of this.players) {
                colors.push(p.color);
            }
            return colors;
        }

        getColumns(): ChartColumn[] {
            const cols: ChartColumn[] = [{ type: ChartColumnType.DateTime, label: 'Time', pattern: ChartColumnPattern.HoursAndMinutes }];
            for (const p of this.players) {
                cols.push({ type: ChartColumnType.Number, label: p.name, pattern: null });
            }
            return cols;
        }

        getRows(): ChartRow[] {
            var rows = [];
            for (const player of this.players) {
                if(player.actions.length){
                    const results = this.getPlayerResults(player);
                    for (let j = 0; j < results.length; j++) {
                        rows.push(this.getRow(results[j], player.id));
                    }
                    if (!player.hasCashedOut()) {
                        var currentResult = this.createPlayerResult(dayjs.utc().toDate(), results[results.length - 1].winnings);
                        rows.push(this.getRow(currentResult, player.id));
                    }
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
            for (var i = 0; i < this.players.length; i++) {
                var val = null;
                if (this.players[i].id === playerId) {
                    val = result.winnings + '';
                }
                values.push({ v: val, f: null });
            }
            return { c: values };
        }

        mounted() {
            const self = this;
            self.$nextTick(function () {
                if (this.players.length > 0) {
                    self.drawChart();
                }
            });
        }

        @Watch('players')
        cashgamePlayersChanged() {
            if(!!this.players)
                this.drawChart();
        }
    }

    interface ChartPlayerResult{
        time: Date;
        winnings: number;
    }
</script>

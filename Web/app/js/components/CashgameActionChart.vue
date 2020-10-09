<template>
    <div>
        <LineChart :chart-data="chartData" :chart-options="chartOptions" />
    </div>
</template>

<script lang="ts">
    import { ChartOptions } from '@/models/ChartOptions';
    import { DetailedCashgameResponsePlayer } from '@/response/DetailedCashgameResponsePlayer';
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import LineChart from './LineChart.vue'
    
    @Component({
        components: {
            LineChart
        }
    })
    export default class CashgameActionChart extends Vue {
        @Prop() readonly player!: DetailedCashgameResponsePlayer;

        chartOptions: ChartOptions = {
            colors: ['#000', '#ABA493'],
            series: { 1: { type: 'area' } },
            vAxis: { minValue: 0 },
            hAxis: { format: 'HH:mm' },
            pointSize: 0
        }

        get chartData() {
            return this.getChartData();
        }

        getChartData() {
            return {
                colors: null,
                cols: [
                    {
                        type: 'datetime',
                        label: 'Time',
                        pattern: 'HH:mm'
                    },
                    {
                        type: 'number',
                        label: 'Stack',
                        pattern: null
                    },
                    {
                        type: 'number',
                        label: 'Buyin',
                        pattern: null
                    }
                ],
                rows: this.getChartRows()
            };
        }

        getChartRows() {
            var buyin = 0;
            var rows = [];
            for (let i = 0; i < this.player.actions.length; i++) {
                let action = this.player.actions[i];
                if (action.added) {
                    let stackBeforeBuyin = action.stack - action.added;
                    rows.push(this.getChartRow(action.time, stackBeforeBuyin, buyin));
                    buyin += action.added;
                }
                rows.push(this.getChartRow(action.time, action.stack, buyin));
            }
            return rows;
        }

        getChartRow(time: Date, stack: number, buyin: number) {
            return {
                c: [
                    {
                        v: new Date(time),
                        f: null
                    },
                    {
                        v: stack,
                        f: null
                    },
                    {
                        v: buyin,
                        f: null
                    }
                ]
            };
        }
    }
</script>

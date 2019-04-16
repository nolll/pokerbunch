<template>
    <div>
        <line-chart :chart-data="chartData" :chart-options="chartOptions"></line-chart>
        <div ref="datawrapper">
            <slot></slot>
        </div>
    </div>
</template>

<script>
    import { LineChart } from '.'

    export default {
        components: {
            LineChart
        },
        props: {
            player: {
                type: Object
            }
        },
        computed: {
            chartData() {
                if (this.player)
                    return this.getChartData();
                return this.chartDataFromJson;
            }
        },
        mounted: function () {
            var self = this;
            self.$nextTick(function () {
                this.chartDataFromJson = self.getChartDataFromJson();
            });
        },
        methods: {
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
            },
            getChartRows() {
                var buyin = 0;
                var rows = [];
                for (let i = 0; i < this.player.actions.length; i++) {
                    let action = this.player.actions[i];
                    if (action.added) {
                        let stackBeforeBuyin = action.stack - action.buyin;
                        rows.push(this.getChartRow(action.time, stackBeforeBuyin, buyin));
                        buyin += action.added;
                    }
                    rows.push(this.getChartRow(action.time, action.stack, buyin));
                }
                return rows;
            },
            getChartRow(time, stack, buyin) {
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
            },
            getChartDataFromJson() {
                if (this.$refs.datawrapper && this.$refs.datawrapper.firstChild)
                    return JSON.parse(this.$refs.datawrapper.firstChild.innerHTML);
                return null;
            }
        },
        data: function () {
            return {
                chartDataFromJson: null,
                chartOptions: {
                    colors: ['#000', '#ABA493'],
                    series: { 1: { type: 'area' } },
                    vAxis: { minValue: 0 },
                    hAxis: { format: 'HH:mm' },
                    pointSize: 0
                }
            }
        }
    };
</script>

<style>

</style>

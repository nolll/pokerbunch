<template>
    <div>
        <line-chart :chart-data="chartData" :chart-options="chartOptions"></line-chart>
        <div ref="datawrapper">
            <slot></slot>
        </div>
    </div>
</template>

<script>
    import { mapGetters } from 'vuex';
    import { LineChart } from '.'

    export default {
        components: {
            LineChart
        },
        mounted: function () {
            var self = this;
            self.$nextTick(function () {
                this.chartData = JSON.parse(self.$refs.datawrapper.firstChild.innerHTML);
            });
        },
        data: function () {
            return {
                chartData: null,
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

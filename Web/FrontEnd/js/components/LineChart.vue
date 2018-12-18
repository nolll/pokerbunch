<template>
    <div ref="container">
        <div ref="placeholder"></div>
        <spinner v-show="!ready"></spinner>
    </div>
</template>

<script>
    import { debounce } from 'debounce';
    import extender from '@/extender';
    import { GoogleCharts } from 'google-charts';
    import { Spinner } from '@/components/Common';

    export default {
        props: ['chartData', 'chartOptions'],
        components: {
            Spinner
        },
        created: function() {
            this.chart = null;
        },
        mounted: function () {
            var self = this;
            self.$nextTick(function () {
                self.loadCharts();
            });
        },
        computed: {
            ready() {
                return this.dataLoaded && this.chartsLoaded;
            },
            dataLoaded() {
                return !!this.chartData;
            }
        },
        watch: {
            'chartsLoaded': function (val) {
                if (this.ready) {
                    this.draw();
                }
            },
            'chartData': function (val) {
                if (this.ready) {
                    this.draw();
                }
            }
        },
        methods: {
            loadCharts() {
                var self = this;
                GoogleCharts.load(function () {
                    self.createChart();
                });
            },
            createChart() {
                this.chart = new GoogleCharts.api.visualization.LineChart(this.$refs.container);
                this.initResizeHandler();
                this.chartsLoaded = true;
            },
            draw() {
                var dataTable = new GoogleCharts.api.visualization.DataTable(this.chartData);
                this.chart.draw(dataTable, this.getConfig());
            },
            initResizeHandler() {
                var self = this;
                window.addEventListener('resize', debounce(function () {
                    self.draw();
                }, 150));
            },
            getConfig() {
                var width = parseInt(window.getComputedStyle(this.$refs.container).width);
                var height = width / 2;

                var conf = {
                    backgroundColor: '#fff',
                    fontSize: 16,
                    fontName: 'arial',
                    interpolateNulls: true,
                    lineWidth: 1,
                    pointSize: 2,
                    theme: 'maximized',
                    seriesType: 'line',
                    width: width,
                    height: height
                };

                if (typeof this.chartOptions == 'object') {
                    return extender.extend(conf, this.chartOptions);
                }

                return conf;
            }
        },
        data: function () {
            return {
                chartsLoaded: false
            }
        }
    };
</script>

<style>

</style>

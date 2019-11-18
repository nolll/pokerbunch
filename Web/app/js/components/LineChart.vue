<template>
    <div ref="container">
        <div ref="placeholder"></div>
        <spinner v-show="!ready"></spinner>
    </div>
</template>

<script>
    import { debounce } from 'debounce';
    import { GoogleCharts } from 'google-charts';
    import { Spinner } from '@/components/Common';

    export default {
        props: {
            chartData: {
                type: Object
            },
            chartOptions: {
                type: Object
            }
        },
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
                var conf = {
                    backgroundColor: '#fff',
                    fontSize: 16,
                    fontName: 'arial',
                    interpolateNulls: true,
                    lineWidth: 1,
                    pointSize: 2,
                    theme: 'maximized',
                    seriesType: 'line',
                    width: this.getWidth(),
                    height: this.getHeight()
                };

                if (typeof this.chartOptions == 'object') {
                    return Object.assign(conf, this.chartOptions);
                }

                return conf;
            },
            getWidth() {
                if (this.$refs.container)
                    return parseInt(window.getComputedStyle(this.$refs.container).width);
                return 0;
            },
            getHeight() {
                return this.getWidth() / 2;
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

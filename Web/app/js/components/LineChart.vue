<template>
    <div ref="container">
        <div ref="placeholder"></div>
        <LoadingSpinner v-show="!ready"></LoadingSpinner>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Vue, Watch } from 'vue-property-decorator';
    import { debounce } from 'ts-debounce';
    import { GoogleCharts } from 'google-charts';
    import LoadingSpinner from '@/components/Common/LoadingSpinner.vue';
    import { ChartData } from '@/models/ChartData';
    import { ChartOptions } from '@/models/ChartOptions';

    @Component({
        components: {
            LoadingSpinner
        }
    })
    export default class LineChart extends Vue {
        @Prop() readonly chartData!: ChartData;
        @Prop() readonly chartOptions!: ChartOptions;

        chart: any = null;
        chartsLoaded = false;
        
        get ready() {
            return this.dataLoaded && this.chartsLoaded;
        }

        get dataLoaded() {
            return !!this.chartData;
        }

        loadCharts() {
            GoogleCharts.load(() => {
                this.createChart();
            });
        }

        createChart() {
            this.chart = new GoogleCharts.api.visualization.LineChart(this.$refs.container);
            this.initResizeHandler();
            this.chartsLoaded = true;
        }

        draw() {
            var dataTable = new GoogleCharts.api.visualization.DataTable(this.chartData);
            this.chart.draw(dataTable, this.getConfig());
        }

        initResizeHandler() {
            window.addEventListener('resize', debounce((event: Event) => {
                this.draw();
            }, 150));
        }

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
        }

        getWidth() {
            if (this.$refs.container)
                return parseInt(window.getComputedStyle(this.$refs.container as Element).width);
            return 0;
        }

        getHeight() {
            return this.getWidth() / 2;
        }

        created() {
            this.chart = null;
        }

        mounted() {
            var self = this;
            self.$nextTick(function () {
                self.loadCharts();
            });
        }

        @Watch('chartsLoaded')
        chartsLoadedChanged() {
            if (this.ready) {
                this.draw();
            }
        }

        @Watch('chartData')
        chartDataChanged() {
            if (this.ready) {
                this.draw();
            }
        }
    }
</script>

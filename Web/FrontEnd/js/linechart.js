import debouncer from './debouncer';
import extender from './extender';
import { GoogleCharts } from 'google-charts';

function LineChart(el, config, data, options) {
    var self = this;
    this.el = el;
    this.config = configure(config);

    GoogleCharts.load(function () {
        self.chartsLoaded();
        self.draw(data, options);

        window.addEventListener('resize', debouncer.debounce(function () {
            self.draw();
        }, 150));
    });
}

LineChart.prototype.chartsLoaded = function () {
    var self = this;
    self.chart = new GoogleCharts.api.visualization.LineChart(self.el);
}

LineChart.prototype.draw = function (data, options) {
   this.data = data;
    var width = parseInt(window.getComputedStyle(this.el).width),
        height = width / 2; 
    this.config.width = width;
    this.config.height = height;
    if (options && options.colors) {
        this.config.colors = options.colors;
    }
    if (!this.config.colors && this.data.colors) {
        this.config.colors = this.data.colors;
    }
    var chartData = new GoogleCharts.api.visualization.DataTable(this.data);
    this.chart.draw(chartData, this.config);
}

function configure(config){
    var defaultConfig = {
        backgroundColor: '#fff',
        fontSize: 16,
        fontName: 'arial',
        height: 400,
        interpolateNulls: true,
        lineWidth: 1,
        pointSize: 2,
        theme: 'maximized',
        seriesType: 'line'
    };
    if (typeof config == 'object') {
        return extender.extend(defaultConfig, config);
    }
    return defaultConfig;
}

LineChart.prototype.loadSuccess = function (loadedData) {
    this.data = loadedData;
    this.draw();
}

LineChart.prototype.loadError = function () {
    this.el.innerHTML = 'failed to load chart data';
}

function init(el, config, callback) {
    return new LineChart(el, config, callback);
}

export default{
    init: init
};

define(["./ajax", "./debouncer", "./extender", "google-charts"],
    function (ajax, debouncer, extender, googleCharts) {
        "use strict";

        function LineChart(el, config){
            var me = this;
                me.el = el;
            this.chart = new google.visualization.LineChart(el);
            this.config = this.configure(config);
            window.addEventListener('resize', debouncer.debounce(function () {
                me.draw();
            }, 150));
        }

        LineChart.prototype.draw = function (data, options) {
            if (data)
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
            var chartData = new google.visualization.DataTable(this.data);
            this.chart.draw(chartData, this.config);
        }

        LineChart.prototype.configure = function(config){
            var defaultConfig = {
                backgroundColor: "#fff",
                fontSize: 16,
                fontName: "arial",
                height: 400,
                interpolateNulls: true,
                lineWidth: 1,
                pointSize: 2,
                theme: "maximized",
                seriesType: "line"
            };
            if (typeof config == "object") {
                return extender.extend(defaultConfig, config);
            }
            return defaultConfig;
        }

        LineChart.prototype.loadDataAndDraw = function() {
            var me = this,
                url = this.el.getAttribute("data-url");

            if (url !== null) {
                ajax.get(url, me.loadSuccess, me.loadError);
            } else {
                me.data = JSON.parse(me.el.querySelector("[type='application/json']").innerHTML);
                me.draw();
            }
        }

        LineChart.prototype.loadSuccess = function (loadedData) {
            this.data = loadedData;
            this.draw();
        }

        LineChart.prototype.loadError = function () {
            this.el.innerHTML = "failed to load chart data";
        }

        function init(el, config) {
            googleCharts.load();

            return new LineChart(el, config);
        }

        return {
            init: init
        };
    }
);
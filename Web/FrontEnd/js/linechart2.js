define(['jquery', 'debouncedresize', 'goog!visualization,1,packages:[corechart]'],
    function ($) {
        "use strict";

        function LineChart(el, config){
            var me = this;
                me.el = el;
                me.$el = $(el);
            this.chart = new google.visualization.LineChart(el);
            this.config = this.configure(config);
            $(window).on("debouncedresize", function() {
                me.draw();
            });
        }

        LineChart.prototype.draw = function (data) {
            if (data)
                this.data = new google.visualization.DataTable(data);;
            var width = this.$el.width(),
                height = width / 2;
            this.config.width = width;
            this.config.height = height;
            this.chart.draw(this.data, this.config);
        }

        LineChart.prototype.configure = function(config){
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
                return $.extend(defaultConfig, config);
            }
            return defaultConfig;
        }

        function init(el, config) {
            return new LineChart(el, config);
        }

        return {
            init: init
        };
    }
);
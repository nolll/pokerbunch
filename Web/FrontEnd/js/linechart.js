define(['jquery', 'debouncedresize', 'goog!visualization,1,packages:[corechart]'],
    function ($) {
        "use strict";

        function LineChart(el, config){
            var me = this;
                me.el = el;
                me.$el = $(el);
            this.chart = new google.visualization.LineChart(el);
            this.config = this.configure(config);
            $(window).on("debouncedresize", function( event ) {
                me.draw();
            });
        }

        LineChart.prototype.draw = function (data) {
            if (data)
                this.data = data;
            var width = this.$el.width(),
                height = width / 2;
            this.config.width = width;
            this.config.height = height;
            if (!this.config.colors && this.data.colors) {
                this.config.colors = this.data.colors;
            }
            var chartData = new google.visualization.DataTable(this.data);
            this.chart.draw(chartData, this.config);
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

        LineChart.prototype.loadDataAndDraw = function() {
            var me = this,
                url = this.$el.data('url');

            if (url != undefined) {
                $.ajax({
                    dataType: 'json',
                    url: url,
                    success: function(loadedData) {
                        me.data = loadedData;
                        me.draw();
                    },
                    error: function() {
                        me.$el.html('failed to load chart data');
                    }
                });
            } else {
                me.data = JSON.parse(me.$el.find('[type="application/json"]').html());
                me.draw();
            }
        }

        function init(el, config) {
            return new LineChart(el, config);
        }

        return {
            init: init
        };
    }
);
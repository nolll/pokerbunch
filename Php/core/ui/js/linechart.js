define(['jquery', 'debouncedresize', 'goog!visualization,1,packages:[corechart]'],
    function ($) {
        "use strict";

        function LineChart(el, config, data){
            var me = this;
                me.el = el;
                me.$el = $(el);
            this.chart = new google.visualization.LineChart(el);
            this.config = this.configure(config);
            if(data != null){
                this.data = data;
                this.draw();
            } else {
                this.loadDataAndDraw();
            }
            $(window).on("debouncedresize", function( event ) {
                me.draw();
            });
        }

        LineChart.prototype.draw = function(){
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

        LineChart.prototype.loadDataAndDraw = function(){
            var me = this,
                url = this.$el.data('url');
            $.ajax({
                dataType: 'json',
                url: url,
                success: function(data) {
                    me.data = new google.visualization.DataTable(data);
                    me.draw();
                },
                error: function(xhr, status, error){
                    me.$el.html('failed to load chart data');
                }
            });
        }

        function init(el, config, data) {
            return new LineChart(el, config, data);
        }

        return {
            init: init
        };
    }
);
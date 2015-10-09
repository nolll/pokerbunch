define(['linechart'],
    function (lineChart) {
        "use strict";

        function init() {
            var config = {
                pointSize:0,
                vAxis: {minValue: 0},
                hAxis: {format: 'HH:mm'}
            };
            var chart = lineChart.init(this, config);
            chart.loadAndDraw();
        }

        return {
            init: init
        };
    }
);
define(['linechart'],
    function (lineChart) {
        "use strict";

        function init() {
            var config = {
                colors: ['#000', '#ABA493'],
                series: {1: {type: "area"}},
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
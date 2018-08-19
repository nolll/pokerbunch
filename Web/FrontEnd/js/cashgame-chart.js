define(["./linechart"],
    function (lineChart) {
        "use strict";

        function init() {
            var config = {
                pointSize: 0,
                legend: {
                    position: "none"
                }
            };
            var chart = lineChart.init(this, config);
            chart.loadDataAndDraw();
        }

        return {
            init: init
        };
    }
);
define(['linechart'],
    function (lineChart) {
        "use strict";

        function init() {
            var config = {
                pointSize:0,
                vAxis: {minValue: 0},
                hAxis: {format: 'HH:mm'}
            };
            return lineChart.init(this, config);
        }

        return {
            init: init
        };
    }
);
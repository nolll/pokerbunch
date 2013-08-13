define(['linechart'],
    function (lineChart) {
        "use strict";

        function init() {
            var config = {
                legend: {
                    position: 'none'
                }
            };
            return lineChart.init(this, config);
        }

        return {
            init: init
        };
    }
);
import lineChart from './linechart';

function init() {
    var config = {
        pointSize: 0,
        vAxis: { minValue: 0 },
        hAxis: { format: 'HH:mm' },
        legend: {
            position: 'none'
        }
    };
    var data = JSON.parse(this.querySelector('[type="application/json"]').innerHTML);
    lineChart.init(this, config, data);
}

export default {
    init: init
};
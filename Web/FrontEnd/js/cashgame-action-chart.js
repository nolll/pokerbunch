import lineChart from './linechart';

function init() {
    var config = {
        colors: ['#000', '#ABA493'],
        series: { 1: { type: 'area' } },
        vAxis: { minValue: 0 },
        hAxis: { format: 'HH:mm' },
        pointSize: 0
    };
    var data = JSON.parse(this.querySelector('[type="application/json"]').innerHTML);
    lineChart.init(this, config, data);
}

export default {
    init: init
};
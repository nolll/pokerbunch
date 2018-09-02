import vue from 'vue';
import gameControl from './game-control/game-control';
import dashboard from './dashboard/dashboard';
import playerDropdown from './player-dropdown/player-dropdown';
import gameButton from './game-button/game-button';
import playerTable from './player-table/player-table';
import playerRow from './player-row/player-row';
import reportForm from './report-form/report-form';
import buyinForm from './buyin-form/buyin-form';
import cashoutForm from './cashout-form/cashout-form';
import spinner from './spinner/spinner';
import gameChart from './game-chart/game-chart';
import topListTable from './top-list-table/top-list-table';
import topListColumn from './top-list-column/top-list-column';
import topListRow from './top-list-row/top-list-row';
import gameListTable from './game-list-table/game-list-table';
import gameListColumn from './game-list-column/game-list-column';
import gameListRow from './game-list-row/game-list-row';

function init() {
    vue.config.debug = true;
    vue.prototype.eventHub = new vue();

    vue.component('game-control', gameControl);
    vue.component('dashboard', dashboard);
    vue.component('player-dropdown', playerDropdown);
    vue.component('game-button', gameButton);
    vue.component('player-table', playerTable);
    vue.component('player-row', playerRow);
    vue.component('report-form', reportForm);
    vue.component('cashout-form', cashoutForm);
    vue.component('buyin-form', buyinForm);
    vue.component('spinner', spinner);
    vue.component('game-chart', gameChart);
    vue.component('top-list-table', topListTable);
    vue.component('top-list-column', topListColumn);
    vue.component('top-list-row', topListRow);
    vue.component('game-list-table', gameListTable);
    vue.component('game-list-column', gameListColumn);
    vue.component('game-list-row', gameListRow);

    vue.filter('customCurrency', function (value, format, separator) {
        return formatCurrency(value, format, separator);
    });

    vue.filter('result', function (value, format, separator) {
        var absValue = Math.abs(value);
        var currencyValue = formatCurrency(absValue, format, separator);
        if (value > 0)
            return '+' + currencyValue;
        if (value < 0)
            return '-' + currencyValue;
        return currencyValue;
    });

    vue.filter('winrate', function (value, format, separator) {
        var currencyValue = formatCurrency(value, format, separator);
        return currencyValue + '/h';
    });

    vue.filter('time', function (minutes) {
        var h = Math.floor(minutes / 60);
        var m = minutes % 60;
        if (h > 0 && m > 0)
            return h + 'h ' + m + 'm';
        if (h > 0)
            return h + 'h';
        return m + 'm';
    });

    initComponents();
}

function formatCurrency(value, format, separator) {
    var f = format !== undefined ? format : '${0}';
    var s = separator !== undefined ? separator : ',';
    var v = value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, s);
    return f.replace('{0}', v);
}

function initComponent(elementId) {
    var element = document.getElementById(elementId);

    if (element) {
        new vue({
            el: element
        });
    }
}

function initComponents() {
    initComponent('vue-game-control');
    initComponent('vue-game-list-table');
    initComponent('vue-dashboard');
    initComponent('vue-top-list-table');
}

export default {
    init
};
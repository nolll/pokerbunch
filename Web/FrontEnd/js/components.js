import Vue from 'vue';
import gameControl from './components/game-control/game-control';
import dashboard from './components/dashboard/dashboard';
import playerDropdown from './components/player-dropdown/player-dropdown';
import gameButton from './components/game-button/game-button';
import playerTable from './components/player-table/player-table';
import playerRow from './components/player-row/player-row';
import reportForm from './components/report-form/report-form';
import buyinForm from './components/buyin-form/buyin-form';
import cashoutForm from './components/cashout-form/cashout-form';
import spinner from './components/spinner/spinner';
import gameChart from './components/game-chart/game-chart';
import topListTable from './components/top-list-table/top-list-table';
import topListColumn from './components/top-list-column/top-list-column';
import topListRow from './components/top-list-row/top-list-row';
import gameListTable from './components/game-list-table/game-list-table';
import gameListColumn from './components/game-list-column/game-list-column';
import gameListRow from './components/game-list-row/game-list-row';

function init() {
    Vue.config.debug = true;
    Vue.prototype.eventHub = new Vue();

    Vue.component('game-control', gameControl);
    Vue.component('dashboard', dashboard);
    Vue.component('player-dropdown', playerDropdown);
    Vue.component('game-button', gameButton);
    Vue.component('player-table', playerTable);
    Vue.component('player-row', playerRow);
    Vue.component('report-form', reportForm);
    Vue.component('cashout-form', cashoutForm);
    Vue.component('buyin-form', buyinForm);
    Vue.component('spinner', spinner);
    Vue.component('game-chart', gameChart);
    Vue.component('top-list-table', topListTable);
    Vue.component('top-list-column', topListColumn);
    Vue.component('top-list-row', topListRow);
    Vue.component('game-list-table', gameListTable);
    Vue.component('game-list-column', gameListColumn);
    Vue.component('game-list-row', gameListRow);

    Vue.filter('customCurrency', function (value, format, separator) {
        return formatCurrency(value, format, separator);
    });

    Vue.filter('result', function (value, format, separator) {
        var absValue = Math.abs(value);
        var currencyValue = formatCurrency(absValue, format, separator);
        if (value > 0)
            return '+' + currencyValue;
        if (value < 0)
            return '-' + currencyValue;
        return currencyValue;
    });

    Vue.filter('winrate', function (value, format, separator) {
        var currencyValue = formatCurrency(value, format, separator);
        return currencyValue + '/h';
    });

    Vue.filter('time', function (minutes) {
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
        new Vue({
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

export {
    init
};
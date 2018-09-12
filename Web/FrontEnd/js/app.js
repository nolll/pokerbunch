import vue from 'vue';
import vuex from 'vuex';
import store from './store';

import { Dashboard, GameControl, GameListTable, TopListTable } from './components';

function init() {
    vue.config.debug = true;
    vue.prototype.eventHub = new vue();

    initComponents();
    initFilters();
    initStartpoints();
}

function initComponents() {
    vue.component('dashboard', Dashboard);
    vue.component('game-control', GameControl);
    vue.component('game-list-table', GameListTable);
    vue.component('top-list-table', TopListTable);
}

function initFilters() {
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
}

function formatCurrency(value, format, separator) {
    var f = format !== undefined ? format : '${0}';
    var s = separator !== undefined ? separator : ',';
    var v = value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, s);
    return f.replace('{0}', v);
}

function initStartpoints() {
    initStartpoint('vue-game-control');
    initStartpoint('vue-game-list-table');
    initStartpoint('vue-dashboard');
    initStartpoint('vue-top-list-table');
}

function initStartpoint(elementId) {
    var element = document.getElementById(elementId);

    if (element) {
        new vue({
            el: element,
            store: store
        });
    }
}

export default {
    init
};
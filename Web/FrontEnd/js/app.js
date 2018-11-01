import vue from 'vue';
import vuex from 'vuex';
import store from './store';
import router from './router';

import {
    CashgameActionChart,
    CashgameDetailsChart
} from './components';

function init() {
    vue.config.debug = true;
    vue.prototype.eventHub = new vue();

    initComponents();
    initStartpoints();
}

function initComponents() {
    vue.component('cashgame-details-chart', CashgameActionChart);
    vue.component('cashgame-action-chart', CashgameDetailsChart);
}

function initStartpoints() {
    initStartpoint('vue-cashgame-details-chart');
    initStartpoint('vue-cashgame-action-chart');
    initStartpoint('app');
}

function initStartpoint(elementId) {
    var element = document.getElementById(elementId);

    if (element) {
        var options = {
            el: element,
            router,
            store
        };
        new vue(options);
    }
}

export default {
    init
};
import vue from 'vue';
import vuex from 'vuex';
import store from './store';
import router from './router';

import {
    LoginForm,
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
    vue.component('login-form', LoginForm);
}

function initStartpoints() {
    initStartpoint('vue-cashgame-details-chart', false);
    initStartpoint('vue-cashgame-action-chart', false);
    initStartpoint('vue-login-form', false);
    initStartpoint('app', true);
}

function initStartpoint(elementId, useRouter) {
    var element = document.getElementById(elementId);

    if (element) {
        var options = getOptions(element, useRouter);
        new vue(options);
    }
}

function getOptions(element, useRouter) {
    if (useRouter) {
        return {
            el: element,
            router,
            store
        };
    }

    return {
        el: element,
        store
    };
}

export default {
    init
};
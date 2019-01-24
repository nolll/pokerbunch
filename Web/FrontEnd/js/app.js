import vue from 'vue';
import vuex from 'vuex';
import store from './store';
import router from './router';
import { CashgameActionChart, CashgameDetailsChart } from './components';

function init() {
    vue.config.debug = true;
    vue.prototype.eventHub = new vue();

    const appElement = document.getElementById('app');
    if (appElement)
        initApp(appElement);
    else
        initStandalone();
}

function initStandalone() {
    initElement('vue-cashgame-details-chart');
    initElement('vue-cashgame-action-chart');
}

function initApp(appElement) {
    const options = {
        el: appElement,
        router,
        store
    };
    new vue(options);
}

function initElement(elementId) {
    const element = document.getElementById(elementId);

    if (element) {
        const options = {
            el: element,
            components: {
                CashgameActionChart,
                CashgameDetailsChart
            },
            router,
            store
        };
        new vue(options);
    }
}

export default {
    init
};
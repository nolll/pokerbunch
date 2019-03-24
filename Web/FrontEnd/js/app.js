import vue from 'vue';
import store from './store';
import router from './router';
import { CashgameActionChart, CashgameDetailsChart } from './components';

function init() {
    vue.prototype.eventHub = new vue();

    const appElement = document.getElementById('app');
    if (appElement)
        initApp(appElement);
    else
        initStandalone();
}

function initStandalone() {
    initElement('vue-component');
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
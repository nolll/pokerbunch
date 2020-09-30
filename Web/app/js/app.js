import vue from 'vue';
import RootStore from './store/RootStore';
import router from './router';

Vue.use(Vuex);
const store = new Vuex.Store(RootStore);

function init() {
    vue.prototype.eventHub = new vue();

    const appElement = document.getElementById('app');
    if (appElement)
        initApp(appElement);
}

function initApp(appElement) {
    const options = {
        el: appElement,
        router,
        store
    };
    new vue(options);
}

export default {
    init
};
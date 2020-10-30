import vue from 'vue';
import vuex from 'vuex';
import VueRouter from 'vue-router';
import rootStore from './store/RootStore';
import routerOptions from './routes';

vue.use(vuex);
const store = new vuex.Store(rootStore);

vue.use(VueRouter);
const router = new VueRouter(routerOptions);

function init() {
    vue.prototype.eventHub = new vue();

    const appElement = document.getElementById('app');
    if (appElement)
        initApp(appElement);
}

function initApp(appElement: HTMLElement) {
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
import vue from 'vue';
import vuex from 'vuex';
import VueRouter from 'vue-router';
import rootStore from './store/RootStore';
import routes from './routes';
import Root from './components/Root.vue';

vue.use(vuex);
const store = new vuex.Store(rootStore);

vue.use(VueRouter);
const router = new VueRouter(routes);

function initApp() {
    //const options = {
    //    el: '#app',
    //    router,
    //    store
    //};
    //new vue(options);
    new vue({
        router,
        store,
        render(createElement) {
            return createElement(Root);
        }
    }).$mount('#app');
}

function init() {
    vue.prototype.eventHub = new vue();
    initApp();
}

export default {
    init
};
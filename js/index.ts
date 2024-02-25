import browser from './browser';
import { createApp } from 'vue';
import { createStore } from 'vuex';
import { createRouter } from 'vue-router';
import { VueQueryPlugin } from '@tanstack/vue-query';
import rootStore from './store/RootStore';
import routes from './routes';
import Root from './components/Root.vue';
import './styles';

if (!browser.isCapable()) {
  alert('PokerBunch requires a better browser');
}

const store = createStore(rootStore);
const router = createRouter(routes);
const app = createApp(Root);

app.use(store);
app.use(router);
app.use(VueQueryPlugin);
app.mount('#app');

import browser from './browser';
import { createApp } from 'vue';
import PrimeVue from 'primevue/config';
import './styles';
import Material from '@primeuix/themes/material';
import { createRouter } from 'vue-router';
import { VueQueryPlugin } from '@tanstack/vue-query';
import routes from './routes';
import Root from './components/Root.vue';
import { OhVueIcon, addIcons } from 'oh-vue-icons';
import {
  IoCashOutline,
  IoArrowForwardCircleOutline,
  IoMenuOutline,
  IoTimeOutline,
  IoCheckmarkOutline,
  IoTrashOutline,
} from 'oh-vue-icons/icons';

addIcons(IoCashOutline, IoArrowForwardCircleOutline, IoMenuOutline, IoTimeOutline, IoCheckmarkOutline, IoTrashOutline);

if (!browser.isCapable()) {
  alert('PokerBunch requires a better browser');
}

const router = createRouter(routes);
const app = createApp(Root);
app.component('v-icon', OhVueIcon);
app.use(router);
app.use(VueQueryPlugin);
app.use(PrimeVue, {
  theme: {
    preset: Material,
  },
});
app.mount('#app');

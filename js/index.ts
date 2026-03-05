import browser from './browser';
import { createApp } from 'vue';
import './styles';
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
  IoCreateOutline,
  IoCloseOutline,
} from 'oh-vue-icons/icons';

addIcons(
  IoCashOutline,
  IoArrowForwardCircleOutline,
  IoMenuOutline,
  IoTimeOutline,
  IoCheckmarkOutline,
  IoTrashOutline,
  IoCreateOutline,
  IoCloseOutline,
);

if (!browser.isCapable()) {
  alert('PokerBunch requires a better browser');
}

const router = createRouter(routes);
const app = createApp(Root);
app.component('v-icon', OhVueIcon);
app.use(router);
app.use(VueQueryPlugin);
app.mount('#app');

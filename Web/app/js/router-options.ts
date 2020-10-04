import { RouteConfig, RouterOptions } from 'vue-router';
import HomePage from '@/components/Pages/HomePage.vue';
import LoginPage from '@/components/Pages/LoginPage.vue';
import UserListPage from '@/components/Pages/UserListPage.vue';
import CashgameDetailsPage from '@/components/Pages/CashgameDetailsPage.vue';
import OverviewPage from '@/components/Pages/OverviewPage.vue';
import MatrixPage from '@/components/Pages/MatrixPage.vue';
import ToplistPage from '@/components/Pages/ToplistPage.vue';
import ChartPage from '@/components/Pages/ChartPage.vue';
import ListPage from '@/components/Pages/ListPage.vue';
import FactsPage from '@/components/Pages/FactsPage.vue';
import PlayerListPage from '@/components/Pages/PlayerListPage.vue';
import EventListPage from '@/components/Pages/EventListPage.vue';
import NotFoundPage from '@/components/Pages/NotFoundPage.vue';
import BunchDetailsPage from '@/components/Pages/BunchDetailsPage.vue';

const routes: RouteConfig[] = [
    { path: '/', component: HomePage },
    { path: '/auth/login', component: LoginPage },
    { path: '/bunches/:slug', component: BunchDetailsPage },
    { path: '/bunches/:slug/cashgames/chart/:year?', component: ChartPage },
    { path: '/bunches/:slug/cashgames/facts/:year?', component: FactsPage },
    { path: '/bunches/:slug/cashgames', component: OverviewPage },
    { path: '/bunches/:slug/cashgames/list/:year?', component: ListPage },
    { path: '/bunches/:slug/cashgames/matrix/:year?', component: MatrixPage },
    { path: '/bunches/:slug/cashgames/:id', component: CashgameDetailsPage },
    { path: '/bunches/:slug/cashgames/toplist/:year?', component: ToplistPage },
    { path: '/bunches/:slug/players', component: PlayerListPage },
    { path: '/bunches/:slug/events', component: EventListPage },
    { path: '/users', component: UserListPage },
    { path: '*', component: NotFoundPage, name: '404' }
];

const redirects: RouteConfig[] = [
    { path: '/bunch/details/:slug', redirect: '/bunches/:slug' },
    { path: '/cashgame/chart/:slug/:year?', redirect: '/bunches/:slug/cashgames/chart/:year?' },
    { path: '/cashgame/facts/:slug/:year?', redirect: '/bunches/:slug/cashgames/facts/:year?' },
    { path: '/cashgame/index/:slug', redirect: '/bunches/:slug/cashgames' },
    { path: '/cashgame/list/:slug/:year?', redirect: '/bunches/:slug/cashgames/list/:year?' },
    { path: '/cashgame/matrix/:slug/:year?', redirect: '/bunches/:slug/cashgames/matrix/:year?' },
    { path: '/cashgame/details/:slug/:id', redirect: '/bunches/:slug/cashgames/:id' },
    { path: '/cashgame/toplist/:slug/:year?', redirect: '/bunches/:slug/cashgames/toplist/:year?' },
    { path: '/player/list/:slug', redirect: '/bunches/:slug/players' },
    { path: '/user/list', redirect: '/users' }
];

export default {
    mode: 'history',
    routes: routes.concat(redirects)
} as RouterOptions;
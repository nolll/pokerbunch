import Vue from 'vue';
import VueRouter from 'vue-router';

import {
    HomePage,
    LoginPage,
    UserListPage,
    CashgameDetailsPage,
    DashboardPage,
    OverviewPage,
    MatrixPage,
    ToplistPage,
    ChartPage,
    ListPage,
    FactsPage,
    PlayerListPage,
    NotFoundPage,
    BunchDetailsPage
} from './components/Pages';

Vue.use(VueRouter);

const routes = [
    { path: '/', component: HomePage },
    { path: '/auth/login', component: LoginPage },
    { path: '/bunches/:slug', component: BunchDetailsPage },
    { path: '/bunches/:slug/cashgames/chart/:year?', component: ChartPage },
    { path: '/bunches/:slug/cashgames/dashboard', component: DashboardPage },
    { path: '/bunches/:slug/cashgames/facts/:year?', component: FactsPage },
    { path: '/bunches/:slug/cashgames', component: OverviewPage },
    { path: '/bunches/:slug/cashgames/list/:year?', component: ListPage },
    { path: '/bunches/:slug/cashgames/matrix/:year?', component: MatrixPage },
    { path: '/bunches/:slug/cashgames/:id', component: CashgameDetailsPage },
    { path: '/bunches/:slug/cashgames/toplist/:year?', component: ToplistPage },
    { path: '/bunches/:slug/players', component: PlayerListPage },
    { path: '/users', component: UserListPage },
    { path: '*', component: NotFoundPage, name: '404' }
];

const redirects = [
    { path: '/bunch/details/:slug', redirect: '/bunches/:slug' },
    { path: '/cashgame/chart/:slug/:year?', redirect: '/bunches/:slug/cashgames/chart/:year?' },
    { path: '/cashgame/dashboard/:slug', redirect: '/bunches/:slug/cashgames/dashboard' },
    { path: '/cashgame/facts/:slug/:year?', redirect: '/bunches/:slug/cashgames/facts/:year?' },
    { path: '/cashgame/index/:slug', redirect: '/bunches/:slug/cashgames' },
    { path: '/cashgame/list/:slug/:year?', redirect: '/bunches/:slug/cashgames/list/:year?' },
    { path: '/cashgame/matrix/:slug/:year?', redirect: '/bunches/:slug/cashgames/matrix/:year?' },
    { path: '/cashgame/details/:slug/:id', redirect: '/bunches/:slug/cashgames/:id' },
    { path: '/cashgame/toplist/:slug/:year?', redirect: '/bunches/:slug/cashgames/toplist/:year?' },
    { path: '/player/list/:slug', redirect: '/bunches/:slug/players' },
    { path: '/user/list', redirect: '/users' }
];

export default new VueRouter({
    mode: 'history',
    routes: routes.concat(redirects)
});
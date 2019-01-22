import Vue from 'vue';
import VueRouter from 'vue-router';

import {
    LoginPage,
    UserListPage,
    CurrentGamePage,
    DashboardPage,
    OverviewPage,
    MatrixPage,
    ToplistPage,
    ChartPage,
    ListPage,
    FactsPage,
    PlayerListPage,
    NotFoundPage
} from './components/Pages';

Vue.use(VueRouter);

const routes = [
    { path: '/auth/login', component: LoginPage },
    { path: '/user/list', component: UserListPage },
    { path: '/cashgame/running/:slug', component: CurrentGamePage },
    { path: '/cashgame/dashboard/:slug', component: DashboardPage },
    { path: '/cashgame/index/:slug/:year?', component: OverviewPage },
    { path: '/cashgame/matrix/:slug/:year?', component: MatrixPage },
    { path: '/cashgame/toplist/:slug/:year?', component: ToplistPage },
    { path: '/cashgame/chart/:slug/:year?', component: ChartPage },
    { path: '/cashgame/list/:slug/:year?', component: ListPage },
    { path: '/cashgame/facts/:slug/:year?', component: FactsPage },
    { path: '/player/list/:slug', component: PlayerListPage },
    { path: '*', component: NotFoundPage }
];

export default new VueRouter({
    mode: 'history',
    routes
});
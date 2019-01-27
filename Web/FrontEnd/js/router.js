import Vue from 'vue';
import VueRouter from 'vue-router';

import {
    HomePage,
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
    NotFoundPage,
    BunchDetailsPage
} from './components/Pages';

Vue.use(VueRouter);

const routes = [
    { path: '/', component: HomePage },
    { path: '/auth/login', component: LoginPage },
    { path: '/bunch/details/:slug', component: BunchDetailsPage },
    { path: '/cashgame/chart/:slug/:year?', component: ChartPage },
    { path: '/cashgame/dashboard/:slug', component: DashboardPage },
    { path: '/cashgame/facts/:slug/:year?', component: FactsPage },
    { path: '/cashgame/index/:slug/:year?', component: OverviewPage },
    { path: '/cashgame/list/:slug/:year?', component: ListPage },
    { path: '/cashgame/matrix/:slug/:year?', component: MatrixPage },
    { path: '/cashgame/running/:slug', component: CurrentGamePage },
    { path: '/cashgame/toplist/:slug/:year?', component: ToplistPage },
    { path: '/player/list/:slug', component: PlayerListPage },
    { path: '/user/list', component: UserListPage },
    { path: '*', component: NotFoundPage, name: '404' }
];

export default new VueRouter({
    mode: 'history',
    routes
});
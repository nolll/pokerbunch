import urls from '@/urls';
import { createWebHistory, RouteRecordRaw, RouterOptions } from 'vue-router';
// import HomePage from '@/components/Pages/HomePage.vue';
// import LoginPage from '@/components/Pages/LoginPage.vue';
// import UserListPage from '@/components/Pages/UserListPage.vue';
// import UserDetailsPage from '@/components/Pages/UserDetailsPage.vue';
// import AddUserPage from '@/components/Pages/AddUserPage.vue';
// import ChangePasswordPage from '@/components/Pages/ChangePasswordPage.vue';
// import ResetPasswordPage from '@/components/Pages/ResetPasswordPage.vue';
// import CashgameDetailsPage from '@/components/Pages/CashgameDetailsPage.vue';
// import AddCashgamePage from '@/components/Pages/AddCashgamePage.vue';
// import OverviewPage from '@/components/Pages/OverviewPage.vue';
// import MatrixPage from '@/components/Pages/MatrixPage.vue';
// import ToplistPage from '@/components/Pages/ToplistPage.vue';
// import AddPlayerPage from '@/components/Pages/AddPlayerPage.vue';
// import PlayerDetailsPage from '@/components/Pages/PlayerDetailsPage.vue';
// import ChartPage from '@/components/Pages/ChartPage.vue';
// import ListPage from '@/components/Pages/ListPage.vue';
// import FactsPage from '@/components/Pages/FactsPage.vue';
// import PlayerListPage from '@/components/Pages/PlayerListPage.vue';
// import AddEventPage from '@/components/Pages/AddEventPage.vue';
// import EventListPage from '@/components/Pages/EventListPage.vue';
// import EventDetailsPage from '@/components/Pages/EventDetailsPage.vue';
// import LocationListPage from '@/components/Pages/LocationListPage.vue';
// import LocationDetailsPage from '@/components/Pages/LocationDetailsPage.vue';
// import AddLocationPage from '@/components/Pages/AddLocationPage.vue';
// import NotFoundPage from '@/components/Pages/NotFoundPage.vue';
// import BunchListPage from '@/components/Pages/BunchListPage.vue';
// import BunchDetailsPage from '@/components/Pages/BunchDetailsPage.vue';
// import AddBunchPage from '@/components/Pages/AddBunchPage.vue';
// import JoinBunchPage from '@/components/Pages/JoinBunchPage.vue';
// import AdminToolsPage from '@/components/Pages/AdminToolsPage.vue';
import ApiDocsPage from '@/components/Pages/ApiDocsPage.vue';

const routes: RouteRecordRaw[] = [
  // { path: urls.home, component: HomePage },
  // { path: urls.admin.tools, component: AdminToolsPage },
  { path: urls.api.docs, component: ApiDocsPage },
  // { path: '/auth/login', component: LoginPage },
  // { path: '/bunch/add', component: AddBunchPage },
  // { path: '/bunches/:slug/cashgames/add', component: AddCashgamePage },
  // { path: '/bunches/:slug/cashgames/chart/:year?', component: ChartPage },
  // { path: '/bunches/:slug/cashgames/facts/:year?', component: FactsPage },
  // { path: '/bunches/:slug/cashgames/list/:year?', component: ListPage },
  // { path: '/bunches/:slug/cashgames/matrix/:year?', component: MatrixPage },
  // { path: '/bunches/:slug/cashgames/toplist/:year?', component: ToplistPage },
  // { path: '/bunches/:slug/cashgames/:id', component: CashgameDetailsPage },
  // { path: '/bunches/:slug/cashgames', component: OverviewPage },
  // { path: '/bunches/:slug/players/add', component: AddPlayerPage },
  // { path: '/bunches/:slug/players/:id', component: PlayerDetailsPage },
  // { path: '/bunches/:slug/players', component: PlayerListPage },
  // { path: '/bunches/:slug/events/add', component: AddEventPage },
  // { path: '/bunches/:slug/events/:id', component: EventDetailsPage },
  // { path: '/bunches/:slug/events', component: EventListPage },
  // { path: '/bunches/:slug/locations/add', component: AddLocationPage },
  // { path: '/bunches/:slug/locations/:id', component: LocationDetailsPage },
  // { path: '/bunches/:slug/locations', component: LocationListPage },
  // { path: '/bunches/:slug/join/:code?', component: JoinBunchPage },
  // { path: '/bunches/:slug', component: BunchDetailsPage },
  // { path: '/bunches', component: BunchListPage },
  // { path: '/users/:userName', component: UserDetailsPage },
  // { path: '/users', component: UserListPage },
  // { path: '/user/add', component: AddUserPage },
  // { path: '/user/changepassword', component: ChangePasswordPage },
  // { path: '/user/resetpassword', component: ResetPasswordPage },
  // { path: '*', component: NotFoundPage, name: '404' },
];

const redirects: RouteRecordRaw[] = [
  { path: '/bunch/details/:slug', redirect: '/bunches/:slug' },
  { path: '/cashgame/chart/:slug/:year?', redirect: '/bunches/:slug/cashgames/chart/:year?' },
  { path: '/cashgame/facts/:slug/:year?', redirect: '/bunches/:slug/cashgames/facts/:year?' },
  { path: '/cashgame/index/:slug', redirect: '/bunches/:slug/cashgames' },
  { path: '/cashgame/list/:slug/:year?', redirect: '/bunches/:slug/cashgames/list/:year?' },
  { path: '/cashgame/matrix/:slug/:year?', redirect: '/bunches/:slug/cashgames/matrix/:year?' },
  { path: '/cashgame/details/:slug/:id', redirect: '/bunches/:slug/cashgames/:id' },
  { path: '/cashgame/toplist/:slug/:year?', redirect: '/bunches/:slug/cashgames/toplist/:year?' },
  { path: '/player/list/:slug', redirect: '/bunches/:slug/players' },
  { path: '/user/list', redirect: '/users' },
];

const options: RouterOptions = {
  history: createWebHistory(),
  routes: routes.concat(redirects),
};

export default options;

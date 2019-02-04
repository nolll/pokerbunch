import apiClient from './api-client';
import apiUrls from './api-urls';
import ajaxClient from './ajax-client';
import ajaxUrls from './ajax-urls';

export default {
    getToken(data) {
        return ajaxClient.post(ajaxUrls.auth.token, data);
    },
    getCurrentGame(slug) {
        return ajaxClient.get(ajaxUrls.cashgame.current(slug));
    },
    getCurrentGames(slug) {
        const url = `/bunches/${slug}/cashgames/current`;
        return apiClient.get(url);
    },
    getGame(id) {
        const url = `/cashgames/${id}`;
        return apiClient.get(url);
    },
    getBunch(slug) {
        const url = `/bunches/${slug}`;
        return apiClient.get(url);
    },
    getUserBunches() {
        const url = '/user/bunches';
        return apiClient.get(url);
    },
    getPlayers(slug) {
        const url = `/bunches/${slug}/players`;
        return apiClient.get(url);
    },
    getGames(slug, year) {
        const url = year
            ? `/bunches/${slug}/cashgames/${year}`
            : `/bunches/${slug}/cashgames`;
        return apiClient.get(url);
    },
    buyin(slug, data) {
        const url = `/cashgame/buyin/${slug}`;
        return ajaxClient.post(url, data);
    },
    report(slug, data) {
        const url = `/cashgame/report/${slug}`;
        return ajaxClient.post(url, data);
    },
    cashout(slug, data) {
        const url = `/cashgame/cashout/${slug}`;
        return ajaxClient.post(url, data);
    },
    getUser() {
        return apiClient.get(apiUrls.user.current);
    },
    getUsers() {
        return apiClient.get(apiUrls.user.list);
    }
};

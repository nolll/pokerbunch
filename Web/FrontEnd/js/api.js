import apiClient from './api-client';
import ajaxClient from './ajax-client';

export default {
    getToken: function(data) {
        const url = `/auth/token`;
        return ajaxClient.post(url, data);
    },
    getCurrentGame: function(slug) {
        const url = `/cashgame/runninggamejson/${slug}`;
        return ajaxClient.get(url);
    },
    getCurrentGames: function (slug) {
        const url = `/bunches/${slug}/cashgames/current`;
        return apiClient.get(url);
    },
    getGame: function (id) {
        const url = `/cashgames/${id}`;
        return apiClient.get(url);
    },
    getBunch: function (slug) {
        const url = `/bunches/${slug}`;
        return apiClient.get(url);
    },
    getUserBunches: function () {
        const url = '/user/bunches';
        return apiClient.get(url);
    },
    getPlayers: function (slug) {
        const url = `/bunches/${slug}/players`;
        return apiClient.get(url);
    },
    getGames: function (slug, year) {
        const url = year
            ? `/bunches/${slug}/cashgames/${year}`
            : `/bunches/${slug}/cashgames`;
        return apiClient.get(url);
    },
    buyin: function (slug, data) {
        const url = `/cashgame/buyin/${slug}`;
        return ajaxClient.post(url, data);
    },
    report: function (slug, data) {
        const url = `/cashgame/report/${slug}`;
        return ajaxClient.post(url, data);
    },
    cashout: function (slug, data) {
        const url = `/cashgame/cashout/${slug}`;
        return ajaxClient.post(url, data);
    },
    getUser: function () {
        const url = `/user`;
        return apiClient.get(url);
    },
    getUsers: function () {
        const url = `/users`;
        return apiClient.get(url);
    }
};

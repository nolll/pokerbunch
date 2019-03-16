import apiClient from './api-client';
import apiUrls from './api-urls';
import ajaxClient from './ajax-client';
import ajaxUrls from './ajax-urls';

export default {
    getToken: (data) => ajaxClient.post(ajaxUrls.auth.token, data),
    getCashgame: (id) => apiClient.get(apiUrls.cashgame.get(id)),
    getCurrentGames: (slug) => apiClient.get(apiUrls.cashgame.current(slug)),
    getGame: (id) => apiClient.get(apiUrls.cashgame.get(id)),
    getBunch: (slug) => apiClient.get(apiUrls.bunch.get(slug)),
    getUserBunches: () => apiClient.get(apiUrls.bunch.user),
    getPlayers: (slug) => apiClient.get(apiUrls.player.list(slug)),
    getGames: (slug, year) => apiClient.get(apiUrls.cashgame.list(slug, year)),
    buyin: (id, data) => apiClient.post(apiUrls.cashgame.actions(id), data),
    report: (id, data) => apiClient.post(apiUrls.cashgame.actions(id), data),
    cashout: (id, data) => apiClient.post(apiUrls.cashgame.actions(id), data),
    getUser: () => apiClient.get(apiUrls.user.current),
    getUsers: () => apiClient.get(apiUrls.user.list)
};

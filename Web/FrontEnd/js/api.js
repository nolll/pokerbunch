import apiClient from './api-client';
import apiUrls from './api-urls';
import ajaxClient from './ajax-client';
import ajaxUrls from './ajax-urls';

export default {
    getToken: (data) => ajaxClient.post(ajaxUrls.auth.token, data),
    getCurrentGame: (slug) => ajaxClient.get(ajaxUrls.cashgame.current(slug)),
    getCurrentGames: (slug) => apiClient.get(apiUrls.cashgame.current(slug)),
    getGame: (id) => apiClient.get(apiUrls.cashgame.get(id)),
    getBunch: (slug) => apiClient.get(apiUrls.bunch.get(slug)),
    getUserBunches: () => apiClient.get(apiUrls.bunch.user),
    getPlayers: (slug) => apiClient.get(apiUrls.player.list(slug)),
    getGames: (slug, year) => apiClient.get(apiUrls.cashgame.list(slug, year)),
    buyin: (slug, data) => ajaxClient.post(ajaxUrls.cashgame.buyin(slug), data),
    report: (slug, data) => ajaxClient.post(ajaxUrls.cashgame.report(slug), data),
    cashout: (slug, data) => ajaxClient.post(ajaxUrls.cashgame.cashout(slug), data),
    getUser: () => apiClient.get(apiUrls.user.current),
    getUsers: () => apiClient.get(apiUrls.user.list)
};

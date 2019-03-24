import apiClient from './api-client';
import apiUrls from './api-urls';
import ajaxClient from './ajax-client';
import ajaxUrls from './ajax-urls';

export default {
    getToken: (data: object) => ajaxClient.post(ajaxUrls.auth.token, data),
    getCashgame: (id: string) => apiClient.get(apiUrls.cashgame.get(id)),
    getCurrentGames: (slug: string) => apiClient.get(apiUrls.cashgame.current(slug)),
    getGame: (id: string) => apiClient.get(apiUrls.cashgame.get(id)),
    getBunch: (slug: string) => apiClient.get(apiUrls.bunch.get(slug)),
    getUserBunches: () => apiClient.get(apiUrls.bunch.user),
    getPlayers: (slug: string) => apiClient.get(apiUrls.player.list(slug)),
    getGames: (slug: string, year: number) => apiClient.get(apiUrls.cashgame.list(slug, year)),
    buyin: (id: string, data: object) => apiClient.post(apiUrls.cashgame.actions(id), data),
    report: (id: string, data: object) => apiClient.post(apiUrls.cashgame.actions(id), data),
    cashout: (id: string, data: object) => apiClient.post(apiUrls.cashgame.actions(id), data),
    getUser: () => apiClient.get(apiUrls.user.current),
    getUsers: () => apiClient.get(apiUrls.user.list)
};

import apiClient from './api-client';
import apiUrls from './api-urls';
import ajaxClient from './ajax-client';
import ajaxUrls from '@/ajax-urls';
import { Player } from '@/models/Player';
import { User } from '@/models/User';
import { CurrentUser } from '@/models/CurrentUser';
import { CurrentGameResponse } from '@/response/CurrentGameResponse';
import { ArchiveCashgameResponse } from '@/response/ArchiveCashgameResponse';
import { DetailedCashgameResponse } from '@/response/DetailedCashgameResponse';
import { BunchResponse } from '@/response/BunchResponse';
import { ApiParamsGetToken } from '@/models/ApiParamsGetToken';
import { EventResponse } from './response/EventResponse';

export default {
    getToken: (data: ApiParamsGetToken) => ajaxClient.post(ajaxUrls.auth.token, data),
    signOut: () => ajaxClient.post(ajaxUrls.auth.signOut),
    getCashgame: (id: string) => apiClient.get<DetailedCashgameResponse>(apiUrls.cashgame.get(id)),
    getCurrentGames: (slug: string) => apiClient.get<CurrentGameResponse[]>(apiUrls.cashgame.current(slug)),
    getBunch: (slug: string) => apiClient.get<BunchResponse>(apiUrls.bunch.get(slug)),
    getUserBunches: () => apiClient.get<BunchResponse[]>(apiUrls.bunch.user),
    getPlayers: (slug: string) => apiClient.get<Player[]>(apiUrls.player.list(slug)),
    getGames: (slug: string, year?: number) => apiClient.get<ArchiveCashgameResponse[]>(apiUrls.cashgame.list(slug, year)),
    buyin: (id: string, data: object) => apiClient.post(apiUrls.cashgame.actions(id), data),
    report: (id: string, data: object) => apiClient.post(apiUrls.cashgame.actions(id), data),
    cashout: (id: string, data: object) => apiClient.post(apiUrls.cashgame.actions(id), data),
    getCurrentUser: () => apiClient.get<CurrentUser>(apiUrls.user.current),
    getUsers: () => apiClient.get<User[]>(apiUrls.user.list),
    deleteAction: (cashgameId: string, actionId: string) => apiClient.delete(apiUrls.cashgame.action(cashgameId, actionId)),
    updateAction: (cashgameId: string, actionId: string, data: object) => apiClient.put(apiUrls.cashgame.action(cashgameId, actionId), data),
    getEvents: (slug: string) => apiClient.get<EventResponse[]>(apiUrls.event.list(slug))
};

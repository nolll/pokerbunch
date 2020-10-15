import apiClient from './api-client';
import apiUrls from './api-urls';
import ajaxClient from './ajax-client';
import ajaxUrls from '@/ajax-urls';
import { Player } from '@/models/Player';
import { User } from '@/models/User';
import { CurrentGameResponse } from '@/response/CurrentGameResponse';
import { ArchiveCashgameResponse } from '@/response/ArchiveCashgameResponse';
import { DetailedCashgameResponse } from '@/response/DetailedCashgameResponse';
import { BunchResponse } from '@/response/BunchResponse';
import { ApiParamsGetToken } from '@/models/ApiParamsGetToken';
import { EventResponse } from './response/EventResponse';
import { LocationResponse } from './response/LocationResponse';
import { MessageResponse } from './response/MessageResponse';

export default {
    getToken: (data: ApiParamsGetToken) => ajaxClient.post(ajaxUrls.auth.token, data),
    signOut: () => ajaxClient.post(ajaxUrls.auth.signOut),
    getCashgame: (id: string) => apiClient.get<DetailedCashgameResponse>(apiUrls.cashgame.get(id)),
    getCurrentGames: (slug: string) => apiClient.get<CurrentGameResponse[]>(apiUrls.cashgame.current(slug)),
    getBunch: (slug: string) => apiClient.get<BunchResponse>(apiUrls.bunch.get(slug)),
    getUserBunches: () => apiClient.get<BunchResponse[]>(apiUrls.bunch.user),
    getBunches: () => apiClient.get<BunchResponse[]>(apiUrls.bunch.list),
    getPlayers: (slug: string) => apiClient.get<Player[]>(apiUrls.player.list(slug)),
    getGames: (slug: string, year?: number) => apiClient.get<ArchiveCashgameResponse[]>(apiUrls.cashgame.list(slug, year)),
    buyin: (id: string, data: object) => apiClient.post(apiUrls.cashgame.actions(id), data),
    report: (id: string, data: object) => apiClient.post(apiUrls.cashgame.actions(id), data),
    cashout: (id: string, data: object) => apiClient.post(apiUrls.cashgame.actions(id), data),
    getCurrentUser: () => apiClient.get<User>(apiUrls.user.current),
    getUser: (userName: string) => apiClient.get<User>(apiUrls.user.get(userName)),
    updateUser: (data: User) => apiClient.put(apiUrls.user.get(data.userName), data),
    getUsers: () => apiClient.get<User[]>(apiUrls.user.list),
    deleteAction: (cashgameId: string, actionId: string) => apiClient.delete(apiUrls.cashgame.action(cashgameId, actionId)),
    updateAction: (cashgameId: string, actionId: string, data: object) => apiClient.put(apiUrls.cashgame.action(cashgameId, actionId), data),
    getEvents: (slug: string) => apiClient.get<EventResponse[]>(apiUrls.event.list(slug)),
    getLocations: (slug: string) => apiClient.get<LocationResponse[]>(apiUrls.location.list(slug)),
    addLocation: (slug: string, data: object) => apiClient.post<LocationResponse>(apiUrls.location.list(slug), data),
    sendEmail: () => apiClient.post<MessageResponse>(apiUrls.admin.sendEmail),
    clearCache: () => apiClient.post<MessageResponse>(apiUrls.admin.clearCache)
};

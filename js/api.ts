import apiClient from './api-client';
import apiUrls from './api-urls';
import { User } from '@/models/User';
import { CurrentGameResponse } from '@/response/CurrentGameResponse';
import { ArchiveCashgameResponse } from '@/response/ArchiveCashgameResponse';
import { DetailedCashgameResponse } from '@/response/DetailedCashgameResponse';
import { PlayerResponse } from '@/response/PlayerResponse';
import { BunchResponse } from '@/response/BunchResponse';
import { ApiParamsLogin } from '@/models/ApiParamsLogin';
import { EventResponse } from './response/EventResponse';
import { LocationResponse } from './response/LocationResponse';
import { MessageResponse } from './response/MessageResponse';
import { ApiParamsChangePassword } from '@/models/ApiParamsChangePassword';
import { ApiParamsResetPassword } from './models/ApiParamsResetPassword';
import { ApiParamsAddUser } from './models/ApiParamsAddUser';
import { ApiParamsAddBunch } from './models/ApiParamsAddBunch';
import { ApiParamsInvitePlayer } from './models/ApiParamsInvitePlayer';
import { ApiParamsJoinBunch } from './models/ApiParamsJoinBunch copy';
import { ApiParamsUpdateBunch } from './models/ApiParamsUpdateBunch';
import { ApiParamsAddEvent } from './models/ApiParamsAddEvent';
import { ApiParamsAddLocation } from './models/ApiParamsAddLocation';
import { ApiParamsAddPlayer } from './models/ApiParamsAddPlayer';

export default {
  login: (data: ApiParamsLogin) => apiClient.post<string>(apiUrls.auth.login, data),
  getCashgame: (id: string) => apiClient.get<DetailedCashgameResponse>(apiUrls.cashgame.get(id)),
  addCashgame: (slug: string, data: object) => apiClient.post<DetailedCashgameResponse>(apiUrls.cashgame.list(slug), data),
  updateCashgame: (id: string, data: object) => apiClient.put(apiUrls.cashgame.get(id), data),
  deleteCashgame: (id: string) => apiClient.delete(apiUrls.cashgame.get(id)),
  getCurrentGames: (slug: string) => apiClient.get<CurrentGameResponse[]>(apiUrls.cashgame.current(slug)),
  getBunch: (slug: string) => apiClient.get<BunchResponse>(apiUrls.bunch.get(slug)),
  getUserBunches: () => apiClient.get<BunchResponse[]>(apiUrls.bunch.user),
  getBunches: () => apiClient.get<BunchResponse[]>(apiUrls.bunch.list),
  addBunch: (data: ApiParamsAddBunch) => apiClient.post<BunchResponse>(apiUrls.bunch.list, data),
  updateBunch: (id: string, data: ApiParamsUpdateBunch) => apiClient.put(apiUrls.bunch.get(id), data),
  joinBunch: (slug: string, data: ApiParamsJoinBunch) => apiClient.post(apiUrls.bunch.join(slug), data),
  getPlayers: (slug: string) => apiClient.get<PlayerResponse[]>(apiUrls.player.list(slug)),
  addPlayer: (slug: string, data: ApiParamsAddPlayer) => apiClient.post<PlayerResponse>(apiUrls.player.list(slug), data),
  deletePlayer: (id: string) => apiClient.delete(apiUrls.player.get(id)),
  invitePlayer: (id: string, data: ApiParamsInvitePlayer) => apiClient.post(apiUrls.player.invite(id), data),
  getGames: (slug: string, year?: number) => apiClient.get<ArchiveCashgameResponse[]>(apiUrls.cashgame.list(slug, year)),
  getEventGames: (slug: string, eventId: string) =>
    apiClient.get<ArchiveCashgameResponse[]>(apiUrls.cashgame.listByEvent(slug, eventId)),
  buyin: (id: string, data: object) => apiClient.post(apiUrls.cashgame.actions(id), data),
  report: (id: string, data: object) => apiClient.post(apiUrls.cashgame.actions(id), data),
  cashout: (id: string, data: object) => apiClient.post(apiUrls.cashgame.actions(id), data),
  getCurrentUser: () => apiClient.get<User>(apiUrls.user.current),
  changePassword: (data: ApiParamsChangePassword) => apiClient.put(apiUrls.user.password, data),
  resetPassword: (data: ApiParamsResetPassword) => apiClient.post<MessageResponse>(apiUrls.user.password, data),
  getUser: (userName: string) => apiClient.get<User>(apiUrls.user.get(userName)),
  addUser: (data: ApiParamsAddUser) => apiClient.post(apiUrls.user.list, data),
  updateUser: (data: User) => apiClient.put(apiUrls.user.get(data.userName), data),
  getUsers: () => apiClient.get<User[]>(apiUrls.user.list),
  deleteAction: (cashgameId: string, actionId: string) => apiClient.delete(apiUrls.cashgame.action(cashgameId, actionId)),
  updateAction: (cashgameId: string, actionId: string, data: object) =>
    apiClient.put(apiUrls.cashgame.action(cashgameId, actionId), data),
  getEvents: (slug: string) => apiClient.get<EventResponse[]>(apiUrls.event.list(slug)),
  addEvent: (slug: string, data: ApiParamsAddEvent) => apiClient.post<EventResponse>(apiUrls.event.list(slug), data),
  getLocations: (slug: string) => apiClient.get<LocationResponse[]>(apiUrls.location.list(slug)),
  addLocation: (slug: string, data: ApiParamsAddLocation) => apiClient.post<LocationResponse>(apiUrls.location.list(slug), data),
  sendEmail: () => apiClient.post<MessageResponse>(apiUrls.admin.sendEmail),
  clearCache: () => apiClient.post<MessageResponse>(apiUrls.admin.clearCache),
};

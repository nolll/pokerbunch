export default {
  admin: {
    clearCache: '/admin/clearcache',
    sendEmail: '/admin/sendemail',
  },
  auth: {
    login: '/login',
  },
  bunch: {
    get: (slug: string) => `/bunches/${slug}`,
    join: (slug: string) => `/bunches/${slug}/join`,
    list: '/bunches',
    user: '/user/bunches',
  },
  joinRequests: {
    add: (slug: string) => `/bunches/${slug}/joinrequests`,
    list: (slug: string) => `/bunches/${slug}/joinrequests`,
    accept: (id: string) => `/joinrequests/${id}/accept`,
    deny: (id: string) => `/joinrequests/${id}/deny`,
  },
  cashgame: {
    action: (cashgameId: string, actionId: string) => `/cashgames/${cashgameId}/actions/${actionId}`,
    actions: (id: string) => `/cashgames/${id}/actions`,
    current: (slug: string) => `/bunches/${slug}/cashgames/current`,
    get: (id: string) => `/cashgames/${id}`,
    list: (slug: string, year?: number) => (year ? `/bunches/${slug}/cashgames/${year}` : `/bunches/${slug}/cashgames`),
    listByEvent: (slug: string, eventId: string) => `/events/${eventId}/cashgames`,
  },
  event: {
    list: (slug: string) => `/bunches/${slug}/events`,
  },
  location: {
    list: (slug: string) => `/bunches/${slug}/locations`,
  },
  player: {
    list: (slug: string) => `/bunches/${slug}/players`,
    get: (id: string) => `/players/${id}`,
    invite: (id: string) => `/players/${id}/invite`,
  },
  user: {
    password: '/user/password',
    current: '/user',
    get: (userName: string) => `/users/${userName}`,
    update: (userName: string) => `/users/${userName}`,
    list: '/users',
  },
};

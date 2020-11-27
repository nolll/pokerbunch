export default {
    admin: {
        clearCache: '/admin/clearcache',
        sendEmail: '/admin/sendemail'
    },
    auth: {
        token: '/token'
    },
    bunch: {
        get: (slug: string) => `/bunches/${slug}`,
        join: (slug: string) => `/bunches/${slug}/join`,
        list: '/bunches',
        user: '/user/bunches'
    },
    cashgame: {
        action: (cashgameId: string, actionId: string) => `/cashgames/${cashgameId}/actions/${actionId}`,
        actions: (id: string) => `/cashgames/${id}/actions`,
        current: (slug: string) => `/bunches/${slug}/cashgames/current`,
        get: (id: string) => `/cashgames/${id}`,
        list: (slug: string, year?: number) => year ? `/bunches/${slug}/cashgames/${year}` : `/bunches/${slug}/cashgames`,
        listByEvent: (slug: string, eventId: string) => `/events/${eventId}/cashgames`
    },
    event: {
        list: (slug: string) => `/bunches/${slug}/events`
    },
    location: {
        list: (slug: string) => `/bunches/${slug}/locations`
    },
    misc: {
        timezones: {
            list: '/misc/timezones'
        }
    },
    player: {
        list: (slug: string) => `/bunches/${slug}/players`,
        get: (id: string) => `/players/${id}`,
        invite: (id: string) => `/players/${id}/invite`
    },
    user: {
        password: '/user/password',
        current: '/user',
        get: (userName: string) => `/users/${userName}`,
        update: (userName: string) => `/users/${userName}`,
        list: '/users'
    }
};

export default {
    admin: {
        clearCache: '/admin/clearcache',
        sendEmail: '/admin/sendemail'
    },
    bunch: {
        get: (slug: string) => `/bunches/${slug}`,
        list: '/bunches',
        user: '/user/bunches'
    },
    cashgame: {
        action: (cashgameId: string, actionId: string) => `/cashgames/${cashgameId}/actions/${actionId}`,
        actions: (id: string) => `/cashgames/${id}/actions`,

        current: (slug: string) => `/bunches/${slug}/cashgames/current`,
        get: (id: string) => `/cashgames/${id}`,
        list: (slug: string, year?: number) => year ? `/bunches/${slug}/cashgames/${year}` : `/bunches/${slug}/cashgames`
    },
    event: {
        list: (slug: string) => `/bunches/${slug}/events`
    },
    location: {
        list: (slug: string) => `/bunches/${slug}/locations`
    },
    player: {
        list: (slug: string) => `/bunches/${slug}/players`
    },
    user: {
        changePassword: '/user/password/change',
        current: '/user',
        get: (userName: string) => `/users/${userName}`,
        update: (userName: string) => `/users/${userName}`,
        list: '/users'
    }
};

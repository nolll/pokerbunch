export default {
    admin: {
        tools: '/admin/tools'
    },
    auth: {
        login: '/auth/login',
        logout: '/auth/logout'
    },
    api: {
        docs: '/apidocs'
    },
    bunch: {
        add: '/bunch/add',
        details: (slug: string) => `/bunch/details/${slug}`,
        edit: (slug: string) => `/bunch/edit/${slug}`,
        list: '/bunches'
    },
    cashgame: {
        add: (slug: string) => `/bunches/${slug}/add-cashgame`,
        details: (slug: string, id: string) => `/bunches/${slug}/cashgames/${id}`,
        edit: (id: string) => `/cashgame/edit/${id}`,
        index: (slug: string) => `/bunches/${slug}/cashgames`,
        actions: (cashgameId: string, playerId: string) => `/cashgame/action/${cashgameId}/${playerId}`
    },
    event: {
        add: (slug: string) => `/bunches/${slug}/events/add`,
        details: (id: string) => `/events/${id}`,
        list: (slug: string) => `/bunches/${slug}/events`
    },
    home: '/',
    location: {
        add: (slug: string) => `/bunches/${slug}/locations/add`,
        details: (slug: string, id: string) => `/bunches/${slug}/locations/${id}`,
        list: (slug: string) => `/bunches/${slug}/locations`
    },
    player: {
        add: (slug: string) => `/player/add/${slug}`,
        details: (id: string) => `/player/details/${id}`,
        list: (slug: string) => `/bunches/${slug}/players`
    },
    user: {
        add: '/user/add',
        details: (userName: string) => `/users/${userName}`,
        resetPassword: '/user/resetpassword',
        changePassword: '/user/changepassword',
        list: '/users'
    }
};

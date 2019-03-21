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
    app: {
        list: '/apps/all'
    },
    bunch: {
        add: '/bunch/add',
        details: (slug: string) => `/bunch/details/${slug}`,
        edit: (slug: string) => `/bunch/edit/${slug}`,
        list: '/bunch/all'
    },
    cashgame: {
        add: (slug: string) => `/bunches/${slug}/cashgames/add`,
        details: (slug: string, id: string) => `/bunches/${slug}/cashgames/${id}`,
        edit: (id: string) => `/cashgame/edit/${id}`,
        index: (slug: string) => `/bunches/${slug}/cashgames`,
        actions: (cashgameId: string, playerId: string) => `/cashgame/action/${cashgameId}/${playerId}`
    },
    event: {
        list: (slug: string) => `/event/list/${slug}`
    },
    home: '/',
    location: {
        details: (id: string) => `/location/details/${id}`,
        list: (slug: string) => `/location/list/${slug}`
    },
    player: {
        add: (slug: string) => `/player/add/${slug}`,
        details: (id: string) => `/player/details/${id}`,
        list: (slug: string) => `/player/list/${slug}`
    },
    user: {
        add: '/user/add',
        details: (userName: string) => `/users/${userName}`,
        forgotPassword: '/user/forgotpassword',
        list: '/users'
    }
};

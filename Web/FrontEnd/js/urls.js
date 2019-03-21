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
        details: (slug) => `/bunch/details/${slug}`,
        edit: (slug) => `/bunch/edit/${slug}`,
        list: '/bunch/all'
    },
    cashgame: {
        add: (slug) => `/bunches/${slug}/cashgames/add`,
        details: (slug, id) => `/bunches/${slug}/cashgames/${id}`,
        edit: (id) => `/cashgame/edit/${id}`,
        index: (slug) => `/bunches/${slug}/cashgames`,
        actions: (cashgameId, playerId) => `/cashgame/action/${cashgameId}/${playerId}`
    },
    event: {
        list: (slug) => `/event/list/${slug}`
    },
    home: '/',
    location: {
        details: (id) => `/location/details/${id}`,
        list: (slug) => `/location/list/${slug}`
    },
    player: {
        add: (slug) => `/player/add/${slug}`,
        details: (id) => `/player/details/${id}`,
        list: (slug) => `/player/list/${slug}`
    },
    user: {
        add: '/user/add',
        details: (userName) => `/users/${userName}`,
        forgotPassword: '/user/forgotpassword',
        list: '/users'
    }
};

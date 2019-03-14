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
        add: (slug) => `/cashgame/add/${slug}`,
        details: (slug, id) => `/cashgame/details/${slug}/${id}`,
        edit: (id) => `/cashgame/edit/${id}`,
        index: (slug) => `/cashgame/index/${slug}`
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
        details: (userName) => `/user/details/${userName}`,
        forgotPassword: '/user/forgotpassword',
        list: '/user/list'
    }
};

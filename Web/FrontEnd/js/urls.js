export default {
    auth: {
        login: '/auth/login',
        logout: '/auth/logout'
    },
    api: {
        docs: 'apidocs'
    },
    bunch: {
        add: '/bunch/add',
        details: (slug) => `/bunch/details/${slug}`,
        edit: (slug) => `/bunch/edit/${slug}`
    },
    cashgame: {
        details: (id) => `/cashgame/details/${id}`,
        index: (slug) => `/cashgame/index/${slug}`
    },
    event: {
        list: (slug) => `/event/list/${slug}`
    },
    home: '/',
    location: {
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
        forgotPassword: '/user/forgotpassword'
    }
};

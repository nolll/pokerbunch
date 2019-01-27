export default {
    auth: {
        login: '/auth/login',
        logout: '/auth/logout'
    },
    bunch: {
        details: (slug) => `/bunch/details/${slug}`
    },
    cashgame: {
        index: (slug) => `/cashgame/index/${slug}`,
        details: (id) => `/cashgame/details/${id}`
    },
    event: {
        list: (slug) => `/event/list/${slug}`
    },
    home: '/',
    location: {
        list: (slug) => `/location/list/${slug}`
    },
    player: {
        details: (id) => `/player/details/${id}`,
        add: (slug) => `/player/add/${slug}`,
        list: (slug) => `/player/list/${slug}`
    },
    user: {
        details: (userName) => `/user/details/${userName}`,
        add: '/user/add',
        forgotPassword: '/user/forgotpassword'
    }
};

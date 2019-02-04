export default {
    auth: {
        token: '/auth/token'
    },
    cashgame: {
        current: (slug) => `/cashgame/runninggamejson/${slug}`
    }
};

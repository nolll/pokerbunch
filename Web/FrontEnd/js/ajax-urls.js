export default {
    auth: {
        token: '/auth/token'
    },
    cashgame: {
        buyin: (slug) => `/cashgame/buyin/${slug}`,
        cashout: (slug) => `/cashgame/cashout/${slug}`,
        current: (slug) => `/cashgame/runninggamejson/${slug}`,
        report: (slug) => `/cashgame/report/${slug}`
    }
};

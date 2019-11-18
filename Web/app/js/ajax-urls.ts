export default {
    auth: {
        token: '/auth/token'
    },
    cashgame: {
        buyin: (slug: string) => `/cashgame/buyin/${slug}`,
        cashout: (slug: string) => `/cashgame/cashout/${slug}`,
        report: (slug: string) => `/cashgame/report/${slug}`
    }
};

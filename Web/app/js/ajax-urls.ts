export default {
    auth: {
        token: '/auth/token',
        signOut: '/auth/logout'
    },
    cashgame: {
        buyin: (slug: string) => `/cashgame/buyin/${slug}`,
        cashout: (slug: string) => `/cashgame/cashout/${slug}`,
        report: (slug: string) => `/cashgame/report/${slug}`
    }
};

export default {
    bunch: {
        get: (slug) => `/bunches/${slug}`,
        user: '/user/bunches'
    },
    cashgame: {
        actions: (id) => `/cashgames/${id}/actions`,
        current: (slug) => `/bunches/${slug}/cashgames/current`,
        get: (id) => `/cashgames/${id}`,
        list: (slug, year) => year ? `/bunches/${slug}/cashgames/${year}` : `/bunches/${slug}/cashgames`
    },
    player: {
        list: (slug) => `/bunches/${slug}/players`
    },
    user: {
        current: '/user',
        list: '/users'
    }
};

export default {
    bunch: {
        get: (slug: string) => `/bunches/${slug}`,
        user: '/user/bunches'
    },
    cashgame: {
        action: (cashgameId: string, actionId: string) => `/cashgames/${cashgameId}/actions/${actionId}`,
        actions: (id: string) => `/cashgames/${id}/actions`,

        current: (slug: string) => `/bunches/${slug}/cashgames/current`,
        get: (id: string) => `/cashgames/${id}`,
        list: (slug: string, year?: number) => year ? `/bunches/${slug}/cashgames/${year}` : `/bunches/${slug}/cashgames`
    },
    player: {
        list: (slug: string) => `/bunches/${slug}/players`
    },
    user: {
        current: '/user',
        list: '/users'
    }
};

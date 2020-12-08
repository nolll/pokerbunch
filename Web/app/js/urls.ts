import { CashgamePage } from './models/CashgamePage';

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
    bunch: {
        add: '/bunch/add',
        details: (slug: string) => `/bunch/details/${slug}`,
        edit: (slug: string) => `/bunch/edit/${slug}`,
        list: '/bunches'
    },
    cashgame: {
        add: (slug: string) => `/bunches/${slug}/cashgames/add`,
        details: (slug: string, id: string) => `/bunches/${slug}/cashgames/${id}`,
        edit: (id: string) => `/cashgame/edit/${id}`,
        index: (slug: string) => `/bunches/${slug}/cashgames`,
        actions: (cashgameId: string, playerId: string) => `/cashgame/action/${cashgameId}/${playerId}`,
        archive: (page: CashgamePage, slug: string, year?: number | null | undefined) => year
            ? `/cashgame/${page}/${slug}/${year}`
            : `/cashgame/${page}/${slug}`
    },
    event: {
        add: (slug: string) => `/bunches/${slug}/events/add`,
        details: (slug: string, id: string) => `/bunches/${slug}/events/${id}`,
        list: (slug: string) => `/bunches/${slug}/events`
    },
    home: '/',
    location: {
        add: (slug: string) => `/bunches/${slug}/locations/add`,
        details: (slug: string, id: string) => `/bunches/${slug}/locations/${id}`,
        list: (slug: string) => `/bunches/${slug}/locations`
    },
    player: {
        add: (slug: string) => `/bunches/${slug}/players/add`,
        details: (slug: string, id: string) => `/bunches/${slug}/players/${id}`,
        invite: (id: string) => `/player/invite/${id}`,
        list: (slug: string) => `/bunches/${slug}/players`
    },
    user: {
        add: '/user/add',
        details: (userName: string) => `/users/${userName}`,
        resetPassword: '/user/resetpassword',
        changePassword: '/user/changepassword',
        list: '/users'
    }
};

export default {
    login: function() {
        return '/auth/login';
    },
    cashgameIndex: function (slug) {
        return `/cashgame/index/${slug}`;
    },
    cashgameDetails: function (id) {
        return `/cashgame/details/${id}`;
    },
    playerDetails: function (id) {
        return `/player/details/${id}`;
    },
    addPlayer: function (slug) {
        return `/player/add/${slug}`;
    }
};

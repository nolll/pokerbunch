export default {
    login: function() {
        return '/auth/login';
    },
    cashgameIndex: function (slug) {
        return `/cashgame/index/${slug}`;
    },
    cashgameDetails: function (id) {
        return `/cashgame/details/${id}`;
    }
};

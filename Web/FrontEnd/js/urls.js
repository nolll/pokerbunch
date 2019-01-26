export default {
    auth: {
        login: function () {
            return '/auth/login';
        }
    },
    cashgame: {
        index: function (slug) {
            return `/cashgame/index/${slug}`;
        },
        details: function (id) {
            return `/cashgame/details/${id}`;
        }
    },
    player: {
        details: function (id) {
            return `/player/details/${id}`;
        },
        add: function (slug) {
            return `/player/add/${slug}`;
        }
    },
    user: {
        details: function (userName) {
            return `/user/details/${userName}`;
        }
    }
};

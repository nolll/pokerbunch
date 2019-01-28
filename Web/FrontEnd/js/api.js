import axios from 'axios';
import auth from './auth';

export default {
    getToken: function(data) {
        const url = `/auth/token`;
        return localPost(url, data);
    },
    getCurrentGame: function(slug) {
        const url = `/cashgame/runninggamejson/${slug}`;
        return localGet(url);
    },
    getBunch: function (slug) {
        const url = `/bunches/${slug}`;
        return apiGet(url);
    },
    getUserBunches: function () {
        const url = '/user/bunches';
        return apiGet(url);
    },
    getPlayers: function (slug) {
        const url = `/bunches/${slug}/players`;
        return apiGet(url);
    },
    getGames: function (slug, year) {
        const url = year
            ? `/bunches/${slug}/cashgames/${year}`
            : `/bunches/${slug}/cashgames`;
        return apiGet(url);
    },
    buyin: function (slug, data) {
        const url = `/cashgame/buyin/${slug}`;
        return localPost(url, data);
    },
    report: function (slug, data) {
        const url = `/cashgame/report/${slug}`;
        return localPost(url, data);
    },
    cashout: function (slug, data) {
        const url = `/cashgame/cashout/${slug}`;
        return localPost(url, data);
    },
    getUser: function () {
        const url = `/user`;
        return apiGet(url);
    },
    getUsers: function () {
        const url = `/users`;
        return apiGet(url);
    }
};

function apiGet(url) {
    return get(getApiUrl(url));
}

function apiPost(url, data) {
    return post(getApiUrl(url), data);
}

function localGet(url) {
    return get(url);
}

function localPost(url, data) {
    return post(url, data);
}

function get(url) {
    return axios({
        method: 'get',
        url: url,
        headers: getApiHeaders()
    });
}

function post(url, data) {
    return axios({
        method: 'post',
        url: url,
        headers: getApiHeaders(),
        data: data
    });
}

function getApiHeaders() {
    const token = auth.getToken();
    return {
        'Authorization': `bearer ${token}`
    };
}

function getApiUrl(url) {
    const protocol = getProtocol();
    const host = getHost();
    return protocol + '//' + host + url;
}

function getHost() {
    if (!window || !window.vueConfig || !window.vueConfig.apiHost)
        throw exception('No api host configured');
    return window.vueConfig.apiHost;
}

function getProtocol() {
    const url = window.location.href;
    return url.split('/')[0];
}

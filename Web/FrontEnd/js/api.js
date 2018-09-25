import axios from 'axios';

export default {
    getToken: function(data) {
        const url = `/auth/login`;
        return localPost(url, data);
    },
    getCurrentGame: function(slug) {
        const url = `/cashgame/runninggamejson/${slug}`;
        return localGet(url);
    },
    refreshCurrentGame: function (slug) {
        const url = `/cashgame/runningplayersjson/${slug}`;
        return localGet(url);
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
        url: url
    });
}

function post(url, data) {
    return axios({
        method: 'post',
        url: url,
        data: data
    });
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

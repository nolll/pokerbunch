import axios from 'axios';

export default {
    getCurrentGame: function(slug) {
        const url = `/cashgame/runninggamejson/${slug}`;
        return get(url);
    },
    refreshCurrentGame: function (slug) {
        const url = `/cashgame/runningplayersjson/${slug}`;
        return get(url);
    },
    buyin: function (slug, data) {
        const url = `/cashgame/buyin/${slug}`;
        return post(url, data);
    },
    report: function (slug, data) {
        const url = `/cashgame/report/${slug}`;
        return post(url, data);
    },
    cashout: function (slug, data) {
        const url = `/cashgame/cashout/${slug}`;
        return post(url, data);
    }
};

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

function getHost() {
    if (!window || !window.apiHost)
        throw exception('No api host configured');
    return window.apiHost;
}

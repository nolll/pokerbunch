import auth from './auth';
import httpClient from './http-client';

export default {
    get(url) {
        return httpClient.get(getApiUrl(url), getApiHeaders());
    },
    post(url, data) {
        return httpClient.post(getApiUrl(url), data, getApiHeaders());
    }
};

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
        throw new Error('No api host configured');
    return window.vueConfig.apiHost;
}

function getProtocol() {
    const url = window.location.href;
    return url.split('/')[0];
}

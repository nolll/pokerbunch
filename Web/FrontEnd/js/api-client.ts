import auth from './auth';
import httpClient from './http-client';
import settings from './settings';

export default {
    get: (url: string) => httpClient.get(getApiUrl(url), getApiHeaders()),
    post: (url: string, data: object) => httpClient.post(getApiUrl(url), data, getApiHeaders()),
    put: (url: string, data: object) => httpClient.put(getApiUrl(url), data, getApiHeaders()),
    delete: (url: string) => httpClient.delete(getApiUrl(url), getApiHeaders())
};

function getApiHeaders() {
    const token = auth.getToken();
    return {
        'Authorization': `bearer ${token}`
    };
}

function getApiUrl(url: string) {
    const protocol = getProtocol();
    return protocol + '//' + settings.apiHost + url;
}

function getProtocol() {
    const url = window.location.href;
    return url.split('/')[0];
}

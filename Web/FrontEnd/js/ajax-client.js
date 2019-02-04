import httpClient from './http-client';

export default {
    get(url) {
        return httpClient.get(url);
    },
    post(url, data) {
        return httpClient.post(url, data);
    }
};

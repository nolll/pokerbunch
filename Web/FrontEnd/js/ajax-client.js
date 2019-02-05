import httpClient from './http-client';

export default {
    get: (url) => httpClient.get(url),
    post: (url, data) => httpClient.post(url, data)
};

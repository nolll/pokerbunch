import httpClient from './http-client';

export default {
    get: (url: string) => httpClient.get(url),
    post: (url: string, data: object) => httpClient.post(url, data)
};

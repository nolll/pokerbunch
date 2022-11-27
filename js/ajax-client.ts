import httpClient from './http-client';

export default {
  get<T = any>(url: string) {
    return httpClient.get<T>(url);
  },
  post<T = any>(url: string, data?: object) {
    return httpClient.post<T>(url, data || {});
  },
};

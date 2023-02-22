import auth from '@/auth';
import { AxiosResponseHeaders } from 'axios';
import httpClient from './http-client';

export default {
  get<T = any>(url: string) {
    return httpClient.get<T>(getApiUrl(url), getApiHeaders());
  },
  post<T = any>(url: string, data?: object) {
    return httpClient.post<T>(getApiUrl(url), data, getApiHeaders());
  },
  put<T = any>(url: string, data: object) {
    return httpClient.put<T>(getApiUrl(url), data, getApiHeaders());
  },
  delete<T = any>(url: string) {
    return httpClient.delete<T>(getApiUrl(url), getApiHeaders());
  },
};

function getApiHeaders() {
  const token = auth.getToken();
  return {
    Authorization: `bearer ${token}`,
  };
}

function getApiUrl(url: string) {
  return url.startsWith('/') ? `/api${url}` : `/api/${url}`;
}

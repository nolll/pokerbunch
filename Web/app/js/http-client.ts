import axios from 'axios';

export default {
    get<T>(url: string, headers?: object): Promise<IHttpResponse<T>> {
        return axios.get<T>(url, { headers });
    },
    post<T>(url: string, data: object, headers?: object): Promise<IHttpResponse<T>> {
        return axios.post<T>(url, data, { headers });
    },
    put<T>(url: string, data: object, headers?: object): Promise<IHttpResponse<T>> {
        return axios.put<T>(url, data, { headers });
    },
    delete<T>(url: string, headers?: object): Promise<IHttpResponse<T>> {
        return axios.delete<T>(url, { headers });
    }
};

export interface IHttpResponse<T> {
    data: T;
    status: number;
}
import axios from 'axios';

export default {
    get: (url: string, headers?: object) => axios.get(url, { headers: headers }),
    post: (url: string, data: object, headers?: object) => axios.post(url, data, { headers: headers, }),
    put: (url: string, data: object, headers?: object) => axios.put(url, data, { headers: headers, }),
    delete: (url: string, headers?: object) => axios.delete(url, { headers: headers, })
};

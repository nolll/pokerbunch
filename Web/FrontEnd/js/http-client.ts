import axios from 'axios';

export default {
    get: (url: string, headers?: object) => axios({
        method: 'get',
        url: url,
        headers: headers
    }),
    post: (url: string, data: object, headers?: object) => axios({
        method: 'post',
        url: url,
        headers: headers,
        data: data
    })
};

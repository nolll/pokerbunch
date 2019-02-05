import axios from 'axios';

export default {
    get: (url, headers) => axios({
        method: 'get',
        url: url,
        headers: headers
    }),
    post: (url, data, headers) => axios({
        method: 'post',
        url: url,
        headers: headers,
        data: data
    })
};

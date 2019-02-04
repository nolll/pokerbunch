import axios from 'axios';

export default {
    get(url, headers) {
        return axios({
            method: 'get',
            url: url,
            headers: headers
        });
    },
    post(url, data, headers) {
        return axios({
            method: 'post',
            url: url,
            headers: headers,
            data: data
        });
    }
};

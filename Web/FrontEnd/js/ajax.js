import axios from 'axios';

function get(url, successHandler, errorHandler) {
    axios(getOptionsForGet(url))
        .then(function(response) {
            successHandler(response.data);
        })
        .catch(function(error) {
            errorHandler(error);
        });
}

function post(url, data, successHandler, errorHandler) {
    axios(getOptionsForPost(url, data))
        .then(function() {
            successHandler();
        })
        .catch(function(error) {
            errorHandler(error);
        });
}

function getOptionsForGet(url) {
    return {
        method: 'get',
        url: url
    }
}

function getOptionsForPost(url, data) {
    return {
        method: 'post',
        url: url,
        data: data
    }
}

export default {
    get: get,
    post: post
};
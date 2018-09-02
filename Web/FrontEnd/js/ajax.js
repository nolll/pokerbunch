import axios from 'axios';

function get(url, successHandler, errorHandler) {
    axios(url, getOptionsForGet())
        .then(function(response) {
            successHandler(response.data);
        })
        .catch(function(error) {
            errorHandler(error);
        });
}

function post(url, data, successHandler, errorHandler) {
    axios(url, getOptionsForPost(data))
        .then(function() {
            successHandler();
        })
        .catch(function(error) {
            errorHandler(error);
        });
}

function getOptionsForGet() {
    return {
        credentials: 'same-origin'
    }
}

function getOptionsForPost(data) {
    return {
        credentials: 'same-origin',
        method: 'post',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    }
}

export default {
    get: get,
    post: post
};
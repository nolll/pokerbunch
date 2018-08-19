define(["axios"],
    function(axios) {
        "use strict";

        function get(url, successHandler, errorHandler) {
            axios(url,
                    {
                        credentials: 'same-origin'
                    })
                .then(function(response) {
                    successHandler(response.data);
                })
                .catch(function(error) {
                    errorHandler(error);
                });
        }

        function post(url, data, successHandler, errorHandler) {
            axios(url,
                    {
                        credentials: 'same-origin',
                        method: 'post',
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(data)
                    })
                .then(function() {
                    successHandler();
                })
                .catch(function(error) {
                    errorHandler(error);
                });
        }

        return {
            get: get,
            post: post
        }
    }
);
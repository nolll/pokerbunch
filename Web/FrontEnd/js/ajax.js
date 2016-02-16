define(["fetch"],
    function(fetch) {
        "use strict";

        function load(url, success, error) {
            fetch('/users')
                .then(checkStatus)
                .then(parseJSON)
                .then(success)
                .catch(error);
        }

        function checkStatus(response) {
            if (response.status >= 200 && response.status < 300) {
                return response;
            } else {
                return new Error(response.statusText);
            }
        }

        function parseJson(response) {
            return response.json();
        }

        return {
            fetch: load
        }
    }
);
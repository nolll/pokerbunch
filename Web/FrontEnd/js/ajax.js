define(["jquery", "fetch"],
    function($, fetch) {
        "use strict";

        function loadWithJquery(url, success, error) {
            $.ajax({
                dataType: 'json',
                url: url,
                success: success,
                error: error,
                cache: false
            });
        }
        
        function postWithJquery(url, data, success, error) {
            $.ajax({
                dataType: 'json',
                url: url,
                type: "POST",
                data: data,
                success: success,
                error: error
            });
        }

        function loadWithFetch(url, success, error) {
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
            fetch: loadWithFetch,
            load: loadWithJquery,
            post: postWithJquery
        }
    }
);
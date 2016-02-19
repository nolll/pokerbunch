define(["vue", "standings"],
    function(vue, standings) {
        "use strict";

        var vm;

        function init(data) {
            vue.config.debug = true;
            vm = new standings({
                el: this,
                data: {
                    url: data.url
                }
            });
        }

        return {
            init: init
        };
    }
);
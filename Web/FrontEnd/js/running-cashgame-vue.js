define(["standings", "jquery"],
    function(standings, $) {
        "use strict";

        var el, vm;

        function init(data) {
            el = this;
            var url = data.url;
            loadData(url, initVue);
        }

        function initVue(data) {
            vm = new standings({
                el: el,
                data: data
            });
        }

        function loadData(url, callback) {
            $.ajax({
                dataType: 'json',
                url: url,
                success: callback,
                cache: false
            });
        }
        
        return {
            init: init
        };
    }
);
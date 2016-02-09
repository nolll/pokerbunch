define(["vue"],
    function(vue) {
        "use strict";

        function init() {
            var data = {
                message: 'Hello Vue.js!'
            };
            var vm = new vue({
                el: this,
                data: data
            });
            //var $el = $(this);
            //var url = $el.data('url');
            //loadData(url, function (data) {
            //    var standings = new StandingsViewModel(data);
            //    ko.applyBindings(standings);
            //    standings.initSockets(data.slug);
            //});
        }

        return {
            init: init
        };
    }
);
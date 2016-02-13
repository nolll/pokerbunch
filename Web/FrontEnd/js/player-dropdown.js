define(["vue", "text!player-dropdown.html"],
    function(vue, html) {
        "use strict";

        return vue.extend({
            template: html,
            props: ['players'],
            ready: function() {
                var x = 0;
            }
        });
    }
);
define(["vue", "text!game-button.html"],
    function(vue, html) {
        "use strict";

        return vue.extend({
            template: html,
            props: ['text', 'icon'],
            ready: function() {
                
            }
        });
    }
);
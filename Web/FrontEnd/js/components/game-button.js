define(["vue", "text!components/game-button.html"],
    function(vue, html) {
        "use strict";

        return vue.extend({
            template: html,
            props: ['text', 'icon']
        });
    }
);
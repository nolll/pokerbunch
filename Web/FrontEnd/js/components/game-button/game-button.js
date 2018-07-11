define(["vue", "text!components/game-button/game-button.html"],
    function(vue, html) {
        "use strict";

        return vue.component("game-button", {
            template: html,
            props: ['text', 'icon'],
            computed: {
                iconCssClass: function() {
                    return 'icon-' + this.icon;
                }
            }
        });
    }
);